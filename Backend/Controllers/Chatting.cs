using Anonymous_Chat.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text.Json;

using static Anonymous_Chat.SysMessageTypes;
using static Anonymous_Chat.Helpers.Actions;

namespace Anonymous_Chat.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Chatting : ControllerBase
    {
        static long connections = 0;
        public readonly static ConcurrentQueue<string> waitingClients = [];
        public readonly static ConcurrentDictionary<string, Profile> profiles = [];
        public readonly static ConcurrentDictionary<string, string> activeChats = [];

        [Route("connect")]
        public async Task HandleConnection()
        {
            var connectionId = $"user-{Interlocked.Increment(ref connections)}";
            string longJSON = string.Empty;
            
            using var webSocket = await HttpContext.WebSockets.AcceptWebSocketAsync();

            try
            {
                Console.WriteLine($"Connection established: {connectionId}");

                await foreach (var message in WebSocketHandler.HandleAsync(connectionId, webSocket))
                {
                    try
                    {
                        using var document = JsonDocument.Parse(message);
                        var jsonNode = document.RootElement;
                         
                        var result = InitialParse(jsonNode);
                        if (result.result == ParsingResult.Last)
                        {
                            longJSON += result.data.GetString();
                            
                            var data = JsonDocument.Parse(longJSON).RootElement;
                            longJSON = "";
             
                            await ParseAndRun(data, connectionId);
                        }
                        else if (result.result == ParsingResult.Partial)
                        {
                            longJSON += result.data.ToString();
                        }
                    }
                    catch (JsonException jsonEx)
                    {
                        Console.WriteLine($"JSON parsing error: {jsonEx.Message}");
                    }
                }
            }
            catch (WebSocketException ex)
            {
                Console.WriteLine($"WebSocket error: {ex.Message}");
            }
            finally
            {
                profiles.Remove(connectionId, out _);
                Console.WriteLine($"Connection closed: {connectionId}");
                await Stop_Talking(connectionId);
                waitingClients.TryPeek(out var conn);
                if (conn == connectionId)
                    waitingClients.TryDequeue(out _);
            }
        }
        private static (JsonElement data, ParsingResult result) InitialParse(JsonElement root)
        {
            if (!root.TryGetProperty("part", out var partId) ||
                !root.TryGetProperty("totalParts", out var totalParts) ||
                !root.TryGetProperty("data", out var data))
                return (root, ParsingResult.Error);
            if (partId.GetInt32() == totalParts.GetInt32())
                return (data, ParsingResult.Last);

            return (data, ParsingResult.Partial);
        }
        private async static Task ParseAndRun(JsonElement root, string connectionString)
        {
            if (root.TryGetProperty("code", out var element))
            {
                var code = (SysMessageTypes)element.GetInt32();
                switch (code)
                {
                    case Transfer:
                        if (!root.TryGetProperty("content", out var msg))
                            return;
                        if (!root.TryGetProperty("files", out var files))
                            return;

                        await Transfer_Message(connectionString, msg.ToString(),
                                               files.Deserialize<List<Models.File>>());
                        break;

                    case Start:
                        if (!root.TryGetProperty("profile", out var profile))
                            return;
                        await Start_Talk(connectionString, profile.Deserialize<Profile>()); break;

                    case Next: await Stop_Talking(connectionString);
                               await Start_Talk(connectionString, profiles[connectionString]); break;

                    case Stop: await Stop_Talking(connectionString); break;
                    default: Console.WriteLine("Wrong data sent"); break;
                }
            }
        }
        enum ParsingResult
        {
            Last,
            Error,
            Partial,
        }
    }
}

