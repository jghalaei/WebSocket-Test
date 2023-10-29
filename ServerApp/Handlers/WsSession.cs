using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace ServerApp.Handlers
{
    public class WsSession
    {
        private static Dictionary<string, WsSession> activeSessions = new Dictionary<string, WsSession>();
        public string ClientId { get; set; }
        public WebSocket WsSocket { get; set; }
        public WsSession(string clientId, WebSocket wsSocket)
        {
            ClientId = clientId;
            WsSocket = wsSocket;

        }
        public static WsSession? GetSession(string clientId)
        {
            if (activeSessions.ContainsKey(clientId))
            {
                return activeSessions[clientId];
            }
            return null;
        }

        public async Task Start()
        {
            activeSessions.Add(ClientId, this);
            await SendMessageAsync(ClientId);
            await SendMessageAsync("Wellcome");
        }


        public async Task SendMessageAsync(string message)
        {
            try
            {
                Byte[] buffer = Encoding.UTF8.GetBytes(message);
                await WsSocket.SendAsync(
                    new ArraySegment<byte>(buffer, 0, buffer.Length),
                    WebSocketMessageType.Text,
                    false,
                    CancellationToken.None
                );
            }
            catch(WebSocketException)
            {
                activeSessions.Remove(ClientId);
                Console.WriteLine($"connection to {ClientId} removed");
            }
        }
    
    }
}