using SocketIOClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace SocketIOClientApp
{
    class Program
    {
        private static SocketIO socket;
        private static DummyTask dummyTask;
        private static string connectionUrl;
        static void Main(string[] args)
        {
            Console.WriteLine("Enter socketio server address (in http(s)://ipAddress:port form)");
            connectionUrl = Console.ReadLine();
            Console.WriteLine("Hello World!");
            dummyTask = new DummyTask();
            dummyTask.FileReadEvent += File_OnRead;

            SocketIOWrapper socketIOWrapper = new SocketIOWrapper();
            socketIOWrapper.Socket_Setup(connectionUrl);
            socket = socketIOWrapper.SocketIO;
            socket.OnConnected += Socket_OnConnected;
            socket.On("/getData", (response) =>
            {
                dummyTask.doTask();
            });
            socket.ConnectAsync();

            Console.ReadLine();
        }

        private static void Socket_OnConnected(object sender, EventArgs e)
        {
            Console.WriteLine("Socket_OnConnected");
            var socket = sender as SocketIO;
            Console.WriteLine("Socket.Id:" + socket.Id);
        }

        public static async void File_OnRead(DummyData data)
        {
            Console.WriteLine($"Sending data {data}");
            await socket.EmitAsync("/sendStatus", data);
        }
    }
}
