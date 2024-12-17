using Anonymous_Chat.Helpers;
using Anonymous_Chat.Models;
using System.Text.Json;
using static Anonymous_Chat.Helpers.Actions;
namespace Anonymous_Chat.Handlers
{
    public class MessageHandler
    {
        public static (string RemainingJson, bool IsComplete, string CompleteJson) HandlePartialMessage(string currentPartial, string newMessage)
        {
            using var document = JsonDocument.Parse(newMessage);
            var jsonNode = document.RootElement;

            var (Data, Result) = ParsePartial(jsonNode);
            if (Result == ParsingResult.Last)
            {
                return ("", true, currentPartial + Data.GetString());
            }

            if (Result == ParsingResult.Partial)
            {
                return (currentPartial + Data.ToString(), false, "");
            }

            return (currentPartial, false, "");
        }
        private static (JsonElement Data, ParsingResult Result) ParsePartial(JsonElement root)
        {
            if (!root.TryGetProperty("part", out var partId) ||
                !root.TryGetProperty("totalParts", out var totalParts) ||
                !root.TryGetProperty("data", out var data))
                return (root, ParsingResult.Error);

            if (partId.GetInt32() == totalParts.GetInt32())
                return (data, ParsingResult.Last);

            return (data, ParsingResult.Partial);
        }
        public static async Task ProcessMessageAsync(string completeJson, string connectionId)
        {
            var root = JsonDocument.Parse(completeJson).RootElement;
            if (root.TryGetProperty("code", out var element))
            {
                var code = (SysMessageTypes)element.GetInt32();
                switch (code)
                {
                    case SysMessageTypes.Transfer:
                        if (root.TryGetProperty("content", out var msg) && root.TryGetProperty("files", out var files))
                        {
                            var fileList = files.Deserialize<List<Models.File>>();
                            await Transfer_Message(connectionId, msg.ToString(), fileList);
                        }
                        break;

                    case SysMessageTypes.Start:
                        if (root.TryGetProperty("profile", out var profile))
                        {
                            var userProfile = profile.Deserialize<Profile>();
                            await Start_Talk(connectionId, userProfile);
                        }
                        break;

                    case SysMessageTypes.Next:
                        await Stop_Talking(connectionId);
                        await Start_Talk(connectionId, UserManager.GetProfile(connectionId));
                        break;

                    case SysMessageTypes.Stop:
                        await Stop_Talking(connectionId);
                        break;

                    default:
                        Console.WriteLine("Invalid message code.");
                        break;
                }
            }
        }
    }
}
