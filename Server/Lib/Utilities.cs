using System;
using System.Net;
using System.Net.Sockets;

namespace TouchGrass
{
    class Utilities
    {
        public static string GetLocalIpAddress()
        {
            string localIp = "127.0.0.1";
            using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, 0))
            {
                socket.Connect("8.8.8.8", 80);
                localIp = (socket.LocalEndPoint as IPEndPoint)?.Address.ToString();
            }
            return localIp;
        }
    }
}
