using System;
using System.Net;
using System.Net.Sockets;

namespace SnakeClient
{
    class Client
    {
        IPEndPoint localEndPoint;
        Socket server;

        public Client(string ipAddress, int port)
        {
            Connect(ipAddress, port);
        }

        public void Connect(string ipAddress, int port)
        {
            localEndPoint = new IPEndPoint(IPAddress.Parse(ipAddress), port);
            server = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            server.Connect(localEndPoint);
        }

        public void Disconnect()
        {
            server.Disconnect(true);

        }

        public byte[] Receive(int bufferSize)
        {
            byte[] buffer = new byte[bufferSize];
            
            try
            {
                server.Receive(buffer);
            }
            catch (System.Exception)
            {
                Environment.Exit(0);
            }
            return buffer;
        }



        public void Send(Player player)
        {
            try
            {
                server.Send(new byte[] { (byte)player.Position.X, (byte)player.Position.Y, (byte)player.Length });
            }
            catch (System.Exception)
            {
                Environment.Exit(0);
            }
        }
    }
}
