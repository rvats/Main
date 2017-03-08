using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using DemoApp;

namespace ChatTutorial.ConsoleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var server = new ChatServer();
                server.Start();
                Console.WriteLine("Listening....");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Logger.PrintException(ex, "Main");
            }
        }
    }

    internal class ChatServer
    {
        private readonly TcpListener _listener = new TcpListener(IPAddress.Parse("0.0.0.0"), 7776);
        private readonly Dictionary<string, LocalClient> _clients = new Dictionary<string, LocalClient>();

        public async void Start()
        {
            try
            {
                _listener.Start();
                while (true)
                {
                    try
                    {
                        Console.WriteLine("Listening...");
                        var client = await _listener.AcceptTcpClientAsync();

                        Console.WriteLine("Client connected");
                        var id = Guid.NewGuid().ToString();
                        _clients.Add(id, new LocalClient(client, ReceivedMessage, ClientClosed, id));
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Start() While() '{0}', '{1}'", ex.Message, ex.StackTrace);
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.PrintException(ex, "Start");
            }
        }

        public void ClientClosed(string id)
        {
            Console.WriteLine("Client {0} disconnected", id);
            var client = _clients[id];
            _clients.Remove(id);
            client.Close();
            Console.WriteLine("Client {0} Closed", id);
            ReceivedMessage("Client disconnected", id);
        }

        public void ReceivedMessage(string message, string id)
        {
            try
            {
                var clientsToRemove = new List<string>();
                foreach (var client in _clients)
                {
                    if (client.Value != null && client.Value.Client.Connected)
                    {
                        if (client.Key != id)
                            client.Value.Send(id + "> " + message);
                    }
                    else
                        clientsToRemove.Add(client.Key);
                }

                foreach (var client in clientsToRemove)
                    _clients.Remove(client);
            }
            catch (Exception ex)
            {
                Logger.PrintException(ex, "ReceivedMessage");
            }
        }
    }

    public static class Logger
    {
        public static void PrintException(Exception ex, string methodName)
        {
            Console.WriteLine("{0}: \n '{1}' \n '{2}'", methodName, ex.Message, ex.StackTrace);
        }
    }
}