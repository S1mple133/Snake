using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Util
{
    public class ServerUtil
    {
        public static Socket Server;
        public static string Ip;
        private static Thread ListenSnakes;
        private static bool FirstStart = true;
        private static IPEndPoint LocalEndPoint;

        /// <summary>
        /// Listens for data from snakes
        /// </summary>
        /// <param name="snake"></param>
        public static void ListenForData(Snake snake)
        {
            byte[] buffer = snake.GetBuffer();
            snake.GetClient().ReceiveTimeout = Util.TIMEOUT_TIME;
            bool isConnected = true;
            do
            {
                try
                {
                    snake.GetClient().Receive(buffer);
                    snake.Update(buffer[0], buffer[1], buffer[2]);
                }
                catch (SocketException)
                {
                    isConnected = false;
                }
            } while (isConnected);

            snake.Remove(KickCode.NONE);
        }

        /// <summary>
        /// Listens 24/7 for incoming connenctions
        /// Registers new snakes and starts task
        /// that listens to Data from snakes
        /// </summary>
        public static void RegisterNewSnakes(Socket server)
        {
            Socket sock;
            while (true)
            {
                try
                {
                    sock = server.Accept();
                }
                catch (SocketException)
                {
                    break;
                }

                // Check if banned
                if (Util.BannedIpList.Contains((sock.RemoteEndPoint as IPEndPoint).Address.ToString()))
                {
                    sock.Disconnect(true);
                    continue;
                }
                sock.SendTimeout = Util.TIMEOUT_TIME;

                new Snake(sock);

                Thread.Sleep(300);

                // Don't accept new connections if max players online
                while (Snake.GetSnakes().Count >= Util.MAX_PLAYERS)
                {
                    Thread.Sleep(500);
                }
            }
        }

        /// <summary>
        /// Handles commands
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string DoCommand(string[] args)
        {
            string cmd = args[0].ToLower();

            if (cmd.Equals("kick"))
            {
                foreach (Snake snake in Snake.GetSnakes())
                {
                    if (snake.GetIp().Equals(args[1]))
                    {
                        snake.Kick(KickCode.KICK);
                        return "Snake successfully kicked!";
                    }
                }
                return "Snake was not found!";
            }
            else if (cmd.Equals("list"))
            {
                foreach (Snake snake in Snake.GetSnakes())
                {
                    Util.log("Online snakes: ");
                    Console.WriteLine();
                    Util.log(snake.GetIp());
                    return "";
                }

                return "No online snakes!";
            }
            else if (cmd.Equals("ban"))
            {
                try
                {
                    Snake.GetSnake(args[1]).Ban();
                    return "Snake was successfully banned!";
                }
                catch (ArgumentNullException ex)
                {
                    return "Snake was not found!";
                }
                catch (NullReferenceException)
                {
                    return "Snake was not found!";
                }
            }
            else if (cmd.Equals("help"))
            {
                return String.Format(" * list : Show the online snakes and their IPs\r\n" +
                                "{0,22} * kick <snake's ip> : Kick a snake\r\n" +
                                "{0,22} * ban <snake's ip> : ban a snake\r\n", "");
            }

            return "Unknown command!";

        }

        /// <summary>
        /// Starts the server
        /// </summary>
        /// <returns></returns>
        internal static string StartServer()
        {
            int[] positionList = new int[2];

            LocalEndPoint = new IPEndPoint(IPAddress.Any, Util.PORT);
            Server = new Socket(IPAddress.Any.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                Server.Bind(LocalEndPoint);
            }
            catch (SocketException)
            {
                return "Server is already running!";
            }

            Ip = Dns.GetHostEntry("").AddressList[Dns.GetHostEntry("").AddressList.Length-1].ToString();

            Server.Listen(Util.MAX_PLAYERS);

            Util.InitBanList();

            /// Start listening to snakes
            ListenSnakes = new Thread(delegate () { ServerUtil.RegisterNewSnakes(Server); });
            ListenSnakes.IsBackground = true;
            ListenSnakes.Start();

            FirstStart = false;

            return "Server started!";
        }

        /// <summary>
        /// Stops the server
        /// </summary>
        /// <returns></returns>
        internal static string StopServer(bool resetCmd)
        {
            try
            {
                Server.Close();
            }
            catch (Exception)
            {
                return "Server is already stopped!";
            }

            Snake.DisconnectFromAllSnakes();

            // Reset cmd
            if (resetCmd)
                Util.Form.resetCmd();

            // Abort listening for snakes
            ListenSnakes.Abort();

            Server = null;

            return "Server stopped!";
        }
    }
}
