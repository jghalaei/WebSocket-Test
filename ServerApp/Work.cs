using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using speed.Handlers;

namespace speed
{
    public class Work
    {
        const string StartMessage = "WorkStarted";
        const string FinishMessage = "WorkFinished";
        private IWebSocketHandler _webSocketHandler;
        public string ConnectionId { get; set; }
        public string WorkId { get; set; }
        public Work(string connectionId, IWebSocketHandler websocketHandler)
        {
            _webSocketHandler = websocketHandler;
            ConnectionId = connectionId;
            WorkId = Guid.NewGuid().ToString();
        }


        public async Task StartAsync()
        {
            if (await _webSocketHandler.SendWebSocketMessageAsync(ConnectionId, $"{StartMessage}, {WorkId}"))
            {
                await DoAsync();
                await FinishAsync();
            }
            else
                throw new ArgumentNullException("Connection not found");

        }
        private async Task DoAsync()
        {
            await Task.Delay(TimeSpan.FromSeconds(2));
        }
        private async Task FinishAsync()
        {
            await _webSocketHandler.SendWebSocketMessageAsync(ConnectionId, $"{FinishMessage}, {WorkId}");
        }
    }
}
