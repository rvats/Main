/******************************************************************************************
 * Author: Rahul Vats
 * Date Created: 03/08/2017
 * Purpose: To Understand the Asynchronous Distributed Programming
******************************************************************************************/

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace ServerSocket1
{
    public static class ServerSocket
    {
        public static void Main()
        {
            var status = true;
            var serverMessage = "";
            var clientMessage = "";

            try
            {
               
                var tcpListener = new TcpListener(IPAddress.Parse("0.0.0.0"), 8100);
                tcpListener.Start();
                Console.WriteLine("Server Started");

                var socketForClient = tcpListener.AcceptSocket();
                Console.WriteLine("Client Connected");
                var networkStream = new NetworkStream(socketForClient);
                var streamwriter = new StreamWriter(networkStream);
                var streamreader = new StreamReader(networkStream);

                while (status)
                {
                    if (socketForClient.Connected)
                    {
                        serverMessage = streamreader.ReadLine();
                        Console.WriteLine("Client:" + serverMessage);
                        if (serverMessage == "bye")
                        {
                            status = false;
                            streamreader.Close();
                            networkStream.Close();
                            streamwriter.Close();
                            return;
                        }
                        Console.Write("Server:");
                        clientMessage = Console.ReadLine();

                        streamwriter.WriteLine(clientMessage);
                        streamwriter.Flush();
                    }
                }
/*
                streamreader.Close();
                networkStream.Close();
                streamwriter.Close();
                socketForClient.Close();
                Console.WriteLine("Exiting");
*/
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}