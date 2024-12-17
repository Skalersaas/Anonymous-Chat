using Anonymous_Chat.Helpers;
using System.Net.WebSockets;
using System.Text.Json;
namespace Anonymous_Chat.Handlers
{
    public class ConnectionHandler
    {
        private static long connections = 0;
        public static async Task HandleConnectionAsync(WebSocket webSocket)
        {
            var connectionId = $"user-{Interlocked.Increment(ref connections)}";
            string partialJson = string.Empty;
            try
            {
                Console.WriteLine($"Connection established: {connectionId}");

                await foreach (var message in WebSocketHandler.HandleAsync(connectionId, webSocket))
                {
                    try
                    {
                        var result = MessageHandler.HandlePartialMessage(partialJson, message);
                        partialJson = result.RemainingJson;
                        if (result.IsComplete)
                            await MessageHandler.ProcessMessageAsync(result.CompleteJson, connectionId);
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
                UserManager.RemoveUser(connectionId);
            }
        }
    }
}
