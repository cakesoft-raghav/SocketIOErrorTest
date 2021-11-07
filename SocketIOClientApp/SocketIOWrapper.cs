using SocketIOClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocketIOClientApp
{
    class SocketIOWrapper
    {
        public SocketIO SocketIO { get; private set; }

        public bool Socket_Setup(string connectionUrl)
        {
            if (SocketIO == null || SocketIO.ServerUri == null || SocketIO.Disconnected)
            {
                try
                {
                    SocketIO = new SocketIO(connectionUrl);
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
            {
                Console.WriteLine("Already connected. Please close existing connection");
            }
            return false;
        }

        public async Task Socket_Connect()
        {
            if (SocketIO == null)
            {
                Console.WriteLine("Please run Socket_Setup before connect");
            }
            else if (SocketIO.Connected)
            {
                Console.WriteLine("Already connected. Please close existing connection");
            }
            else if (SocketIO.ServerUri == null)
            {
                Console.WriteLine("No server uri set. Cannot connect. Please run Socket_Setup");
            }
            else
            {
                SocketIO.OnDisconnected += Socket_OnDisconnected;
                SocketIO.OnReconnectAttempt += Socket_OnReconnecting;
                SocketIO.OnError += Socket_OnError;
                Console.WriteLine("Connecting to SocketIO server");
                await SocketIO.ConnectAsync();
            }
        }

        private void Socket_OnReconnecting(object sender, int e)
        {
            Console.WriteLine($"{DateTime.Now} Reconnecting: attempt = {e}");
        }

        private void Socket_OnDisconnected(object sender, string e)
        {
            Console.WriteLine("disconnect: " + e);
        }

        private void Socket_OnError(object sender, string error)
        {
            Console.WriteLine("{SocketIOError}", error);
        }

        public void Socket_OnMessage(SocketIOResponse response)
        {
            string text = response.GetValue<string>();
            Console.WriteLine($"message from server {text}");
        }
    }
}
