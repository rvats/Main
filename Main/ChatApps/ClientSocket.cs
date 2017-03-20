/******************************************************************************************
 * Author: Rahul Vats
 * Date Created: 03/08/2017
 * Purpose: To Understand the Asynchronous Distributed Programming
******************************************************************************************/

using System;
using System.IO;
using System.Net.Sockets;

namespace ClientSocket
{
    public static class ClientSocket
    {
        private static void Main(string[] args)
        {
            TcpClient socketForServer;

            var status = true;
            var serverMessage = "";
            var clientMessage = "";

            try
            {
                socketForServer = new TcpClient("localhost", 8100);
                Console.WriteLine("Connected to Server");
            }
            catch
            {
                Console.WriteLine("Failed to Connect to server{0}:999", "localhost");
                return;
            }

            var networkStream = socketForServer.GetStream();
            var streamreader = new StreamReader(networkStream);
            var streamwriter = new StreamWriter(networkStream);

            try
            {
                while (status)
                {
                    Console.Write("Client:");
                    clientMessage = Console.ReadLine();
                    if ((clientMessage == "bye") || (clientMessage == "BYE"))
                    {
                        status = false;
                        streamwriter.WriteLine("bye");
                        streamwriter.Flush();
                    }
                    if ((clientMessage != "bye") && (clientMessage != "BYE"))
                    {
                        streamwriter.WriteLine(clientMessage);
                        streamwriter.Flush();
                        serverMessage = streamreader.ReadLine();
                        Console.WriteLine("Server:" + serverMessage);
                    }
                }
            }
            catch
            {
                Console.WriteLine("Exception reading from the server");
            }
            streamreader.Close();
            networkStream.Close();
            streamwriter.Close();
        }
    }
}