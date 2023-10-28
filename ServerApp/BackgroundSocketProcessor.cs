using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading.Tasks;
using ServerApp.Handlers;

namespace ServerApp
{
    public class BackgroundSocketProcessor
    {
        public static async void AddSession(WsSession wsSession, TaskCompletionSource<object> socketFinishedTcs)
        {
            await wsSession.Start();
        }
    }
}