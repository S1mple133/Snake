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
            server.Shutdown(SocketShutdown.Both);
            server.Disconnect(true);

        }

        public byte[] Receive(int bufferSize)
        {
            byte[] buffer = new byte[bufferSize];
            if (IsActive())
                server.Receive(buffer);
            return buffer;
        }

        public bool IsActive()
        {
            return server.Connected;
        }

        public void Send(Player player)
        {
            if (IsActive())
                server.Send(new byte[] { (byte)player.Position.X, (byte)player.Position.Y, (byte)player.Length });
            else
                Console.WriteLine("LOST CONNECTION");
        }
    }
}
