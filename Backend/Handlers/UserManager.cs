using System.Collections.Concurrent;
using Anonymous_Chat.Models;
using static Anonymous_Chat.Helpers.Actions;

namespace Anonymous_Chat.Handlers
{
    public class UserManager
    {
        private static readonly ConcurrentQueue<string> waitingClients = [];
        private static readonly ConcurrentDictionary<string, Profile> profiles = [];
        private static readonly ConcurrentDictionary<string, string> activeChats = [];
        public static async void RemoveUser(string connectionId)
        {
            profiles.Remove(connectionId, out _);
            waitingClients.TryPeek(out var conn);
            if (conn == connectionId)
                waitingClients.TryDequeue(out _);

            await Stop_Talking(connectionId);
            Console.WriteLine($"Connection closed: {connectionId}");
        }
        public static void AddUser(string connectionId, Profile profile = null)
        {
            waitingClients.Enqueue(connectionId);
            if (profile != null)
                profiles[connectionId] = profile;
        }
        public static bool DequeueWaitingUser(out string connectionString)
        {
            return waitingClients.TryDequeue(out connectionString);
        }
        public static Profile GetProfile(string connectionId)
        {
            return profiles[connectionId];
        }
        public static void SetProfile(string connectionId, Profile profile)
        {
            profiles[connectionId] = profile;
        }
        public static KeyValuePair<string, string> GetChat(string connectionString)
        {
            return activeChats.FirstOrDefault(c => c.Key == connectionString || c.Value == connectionString);
        }
        public static void AddChat(string connectionString1, string connectionString2)
        {
            activeChats[connectionString1] = connectionString2;
        }
        public static void RemoveChat(string connectionString)
        {
            activeChats.TryRemove(connectionString, out _);
        }
    }
}
