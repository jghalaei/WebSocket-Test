using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace speed.Handlers
{
    public interface IWebSocketHandler
    {
        Task HandleWebSocketAsync(HttpContext context);
        Task<bool> SendWebSocketMessageAsync(string id, string message);
    }
    public class WebSocketHandler : IWebSocketHandler
    {
        const string WellcomeMessage = "Wellcome";
        private static Dictionary<string, WebSocket> activeWebSockets = new Dictionary<string, WebSocket>();


        private WebSocket? FindActiveWebSockets(string id)
        {
            if (activeWebSockets.ContainsKey(id))
                return activeWebSockets[id];
            return null;
        }

        public async Task HandleWebSocketAsync(HttpContext context)
        {
            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket? webSocket = FindActiveWebSockets(context.Session.Id);
                if (webSocket == null)
                    webSocket = await context.WebSockets.AcceptWebSocketAsync();
                await SendWebSocketMessageAsync(webSocket, WellcomeMessage);
            }
            else
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
        }

        public async Task<bool> SendWebSocketMessageAsync(string id, string message)
        {
            WebSocket? webSocket = FindActiveWebSockets(id);
            if (webSocket != null)
            {
                await SendWebSocketMessageAsync(webSocket, message);
                return true;
            }
            return false;
        }
        private async Task SendWebSocketMessageAsync(WebSocket webSocket, string message)
        {
            Byte[] bytes = Encoding.UTF8.GetBytes(message);

            await webSocket.SendAsync(
                new ArraySegment<byte>(bytes, 0, bytes.Length),
                WebSocketMessageType.Text,
                true,
                CancellationToken.None
            );

        }

    }
}