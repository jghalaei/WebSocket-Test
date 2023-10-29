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
        public static WsSession? GetSession(string clientId)
        {
            if (activeSessions.ContainsKey(clientId))
            {
                return activeSessions[clientId];
            }
            return null;
        }

        private string _clientId;
        private WebSocket _wsSocket;
        private TaskCompletionSource<int> _tcsCompletionSource;
        private ILogger<WsSession> _logger;
        public WsSession(string clientId, WebSocket wsSocket, ILogger<WsSession> logger)
        {
            _clientId = clientId;
            _wsSocket = wsSocket;
            _logger = logger;
        }
        public async Task StartAsync()
        {
            activeSessions.Add(_clientId, this);
            await SendMessageAsync(_clientId);
            await SendMessageAsync("Wellcome");
            _tcsCompletionSource = new TaskCompletionSource<int>();
      
            await _tcsCompletionSource.Task;
        }

        public async Task SendMessageAsync(string message)
        {
            try
            {
                if (_wsSocket.State == WebSocketState.Open)
                {
                    Byte[] buffer = Encoding.UTF8.GetBytes(message);
                    await _wsSocket.SendAsync(
                        new ArraySegment<byte>(buffer, 0, buffer.Length),
                        WebSocketMessageType.Text,
                        false,
                        CancellationToken.None
                    );
                    _logger.LogInformation("WebSocket Message sent: " + message);
                }
            }
            catch (WebSocketException ex)
            {
                _logger.LogError(ex.Message);
                if (activeSessions.ContainsKey(_clientId))
                    activeSessions.Remove(_clientId);
                _tcsCompletionSource.SetResult(1);
                _logger.LogInformation($"connection to {_clientId} removed");
            }
        }
        public async Task DisconnectAsync()
        {
            try
            {
                if (_wsSocket.State == WebSocketState.Open)
                {
                    await _wsSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Disconnect Requested", CancellationToken.None);
                    _logger.LogInformation("Close Handshake Sent");
                }
            }
            finally
            {
                if (activeSessions.ContainsKey(_clientId))
                    activeSessions.Remove(_clientId);
                _tcsCompletionSource.SetResult(1);
            }

        }

    }
}