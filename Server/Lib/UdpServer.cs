using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;

/*
 * INSTALLATION GUIDE:
 * - Get the ViGem driver for Windows
 * - Make sure to give firewall permissions when prompted the first time you open the app
 * 
 * 
 * 
 * 
 */

namespace TouchGrass
{
    class UdpServer
    {
        private InputHandlerInterface inputHandler;

        public UdpServer()
        {
        }

        public void StartServer(bool stopServer)
        {
            UdpClient udpClient = new UdpClient(12345);
            IPEndPoint remoteEP = new IPEndPoint(IPAddress.Any, 12345);

            Console.WriteLine("UDP server listening on port 12345...");
            Console.WriteLine(Utilities.GetLocalIpAddress());
            inputHandler = InputHandlerFactory.getInstance();

            while (true)
            {
                try
                {
                    byte[] data = udpClient.Receive(ref remoteEP);
                    string message = Encoding.UTF8.GetString(data);
                    Console.WriteLine($"Received: {message}");
                    inputHandler.HandleCommand(message);
                    // Send a response
                    byte[] response = Encoding.UTF8.GetBytes("hi");
                    udpClient.Send(response, response.Length, remoteEP);
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Exception occurred: {e.Message}");
                }
            }   

            udpClient.Close();
            inputHandler.Disconnect();
            Console.WriteLine("Server closed.");
        }
    }
}
