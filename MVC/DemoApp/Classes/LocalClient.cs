using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ChatTutorial.ConsoleServer;

namespace DemoApp
{
    internal class LocalClient
    {
        private readonly TcpClient _client;
        public TcpClient Client { get { return _client; } }

        private readonly Action<string> _closedCallback;
        private readonly Action<string, string> _recvCallback;

        private readonly StreamReader _reader;
        private readonly StreamWriter _writer;

        private readonly string _id;
        public string Id { get { return _id; } }

        public LocalClient(TcpClient client, Action<string, string> recvCallback, Action<string> closedCallback, string id)
        {
            _closedCallback = closedCallback;
            try
            {
                _client = client;
                _recvCallback = recvCallback;
                _id = id;

                _reader = new StreamReader(_client.GetStream());
                _writer = new StreamWriter(_client.GetStream()) { AutoFlush = true };

                StartReceive();

                Console.WriteLine("Local client {0} receiving...", id);
            }
            catch (Exception ex)
            {
                Logger.PrintException(ex, "LocalClient");
            }
        }

        public async void StartReceive()
        {
            try
            {
                while (true)
                {
                    var message = await _reader.ReadLineAsync();

                    if (String.IsNullOrEmpty(message))
                    {
                        await Task.Run(() => _closedCallback(_id));
                        return;
                    }

                    _recvCallback(message, _id);
                    Console.WriteLine("{0} > {1}", _id, message);
                }
            }
            catch (Exception ex)
            {
                Logger.PrintException(ex, "StartReceive");
            }
        }

        public void Close()
        {
            if (_reader != null)
                _reader.Dispose();
        }

        public async void Send(string message)
        {
            try
            {
                if (String.IsNullOrEmpty(message)) return;

                await _writer.WriteLineAsync(message);
            }
            catch (Exception ex)
            {
                Logger.PrintException(ex, "Send");
            }
        }
    }
}
