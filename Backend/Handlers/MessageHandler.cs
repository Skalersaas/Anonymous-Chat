using Anonymous_Chat.Helpers;
using Anonymous_Chat.Models;
using System.Text.Json;
using static Anonymous_Chat.Helpers.Actions;
namespace Anonymous_Chat.Handlers
{
    public class MessageHandler
    {
        public static async Task ProcessMessageAsync(string json, string connectionId)
        {
            var root = JsonDocument.Parse(json).RootElement;
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
