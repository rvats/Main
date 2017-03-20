using DemoApp;
using System;

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
}