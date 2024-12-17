using Anonymous_Chat.Models;
using static Anonymous_Chat.Controllers.Chatting;
using static Anonymous_Chat.SysMessageTypes;
namespace Anonymous_Chat.Helpers
{
    public class Actions
    {
        public async static Task Transfer_Message(string connectionString, string message, List<Models.File> files)
        {
            var chat = activeChats.FirstOrDefault(c => c.Key == connectionString || c.Value == connectionString);
            if (chat.Key == null)
                return;

            await WebSocketHandler.SendMessage(
                chat.Key == connectionString ? chat.Value : chat.Key,
                new
                {
                    type = Transfer,
                    content = message,
                    files = files
                });
        }
        public async static Task Start_Talk(string connectionString, Profile profile)
        {
            profiles[connectionString] = profile;

            if (waitingClients.TryDequeue(out var otherConnectionString))
            {
                activeChats[connectionString] = otherConnectionString;
                Console.WriteLine($"Paired: {connectionString} - {otherConnectionString}");
                await WebSocketHandler.SendMessage(connectionString, new
                {
                    type = Start,
                    response = "Pair found!",
                    profile = profiles[otherConnectionString]
                });
                await WebSocketHandler.SendMessage(otherConnectionString, new
                {
                    type = Start,
                    response = "Pair found!",
                    profile,

                });
                return;
            }
            else
            {
                Console.WriteLine($"Waiting: {connectionString}");
                waitingClients.Enqueue(connectionString);
                await WebSocketHandler.SendMessage(connectionString, new
                {
                    type = Waiting,
                    response = "waiting for pair"
                });
            }
        }

        public async static Task Stop_Talking(string connectionString)
        {
            var chat = activeChats.FirstOrDefault(c => c.Key == connectionString || c.Value == connectionString);
            if (chat.Key == null)
                return;

            Console.WriteLine($"Ended: {chat.Key} - {chat.Value}");
            await WebSocketHandler.SendMessage(chat.Key, new
            {
                type = Stop,
                content = "Talk ended"
            });
            await WebSocketHandler.SendMessage(chat.Value, new
            {
                type = Stop,
                content = "Talk ended"
            });
            activeChats.Remove(chat.Key, out _);
        }
    }
}
