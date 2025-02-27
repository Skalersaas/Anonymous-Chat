﻿using System.Collections.Concurrent;
using System.Net.WebSockets;
using System.Text.Json;
using System.Text;

namespace Anonymous_Chat.Helpers
{
    public class WebSocketHandler
    {
        private static readonly ConcurrentDictionary<string, WebSocket> _connections = new();
    
        public static bool AddConnection(string connectionId, WebSocket webSocket)
            => _connections.TryAdd(connectionId, webSocket);
        public static bool RemoveConnection(string connectionId)
            => _connections.TryRemove(connectionId, out _);

        public static async Task SendMessage<T>(string connectionId, T message)
        {
            if (_connections.TryGetValue(connectionId, out var webSocket) && webSocket.State == WebSocketState.Open)
            {
                var messageBytes = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));
                await webSocket.SendAsync(new ArraySegment<byte>(messageBytes),
                        WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
        public static async IAsyncEnumerable<string> HandleAsync(string connectionId, WebSocket webSocket)
        {
            AddConnection(connectionId, webSocket);
            var buffer = new byte[1024 * 4];
            var messageBuilder = new StringBuilder();

            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closed by server", CancellationToken.None);
                    break;
                }
                messageBuilder.Append(Encoding.UTF8.GetString(buffer, 0, result.Count));

                if (result.EndOfMessage)
                {
                    yield return messageBuilder.ToString();
                    messageBuilder.Clear();
                }
            }

            RemoveConnection(connectionId);
        }
    }
}
