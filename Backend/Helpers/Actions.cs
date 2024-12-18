using Anonymous_Chat.Handlers;
using Anonymous_Chat.Models;
using static Anonymous_Chat.Helpers.SysMessageTypes;
namespace Anonymous_Chat.Helpers
{
    public class Actions
    {
        public async static Task Transfer_Message(string connectionString, string message, List<Models.File> files)
        {
            var chat = UserManager.GetChat(connectionString); 
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
            UserManager.SetProfile(connectionString, profile);

            if (UserManager.DequeueWaitingUser(out var otherConnectionString))
            {
                UserManager.AddChat(connectionString, otherConnectionString);
                Console.WriteLine($"Paired: {connectionString} - {otherConnectionString}");
                await WebSocketHandler.SendMessage(connectionString, new
                {
                    type = Start,
                    response = "Pair found!",
                    profile = UserManager.GetProfile(otherConnectionString)
                });
                await WebSocketHandler.SendMessage(otherConnectionString, new
                {
                    type = Start,
                    response = "Pair found!",
                    profile

                });
                return;
            }
            else
            {
                Console.WriteLine($"Waiting: {connectionString}");
                UserManager.AddUser(connectionString);
                await WebSocketHandler.SendMessage(connectionString, new
                {
                    type = Waiting,
                    response = "waiting for pair"
                });
            }
        }

        public async static Task Stop_Talking(string connectionString)
        {
            var chat = UserManager.GetChat(connectionString);
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
            UserManager.RemoveChat(chat.Key);
        }
    }
}
