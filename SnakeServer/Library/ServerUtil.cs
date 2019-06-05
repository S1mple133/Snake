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
        public static Socket server;
        public static string ip;
        private static Thread listenSnakes;

        /// <summary>
        /// Listens for data from snakes
        /// </summary>
        /// <param name="snake"></param>
        public static void listenForData(Snake snake)
        {
            byte[] buffer = snake.getBuffer();
            snake.getClient().ReceiveTimeout = Util._TIMEOUT_TIME;

            do
            {
                try
                {
                    snake.getClient().Receive(buffer);
                    snake.Update(buffer[0], buffer[1], buffer[2]);
                }
                catch (SocketException)
                {
                    break;
                }
            } while (true);

            snake.remove();
        }

        /// <summary>
        /// Listens 24/7 for incoming connenctions
        /// Registers new snakes and starts task
        /// that listens to Data from snakes
        /// </summary>
        public static void registerNewSnakes(Socket server)
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
                if (Util.getBannedIpList().Contains((sock.RemoteEndPoint as IPEndPoint).Address.ToString()))
                {
                    sock.Disconnect(true);
                    continue;
                }

                Thread.Sleep(200);
                sock.SendTimeout = Util._TIMEOUT_TIME;

                new Snake(sock);

                // Don't accept new connections if max players online
                while (Snake.getSnakes().Count >= Util._MAX_PLAYERS)
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
        public static string doCommand(string[] args)
        {
            string cmd = args[0].ToLower();

            if (cmd.Equals("kick"))
            {
                foreach (Snake snake in Snake.getSnakes())
                {
                    if (snake.getIp().Equals(args[1]))
                    {
                        snake.kick();
                        return "Snake successfully kicked!";
                    }
                }
                return "Snake was not found!";
            }
            else if (cmd.Equals("list"))
            {
                foreach (Snake snake in Snake.getSnakes())
                {
                    Util.log("Online snakes: ");
                    Console.WriteLine();
                    Util.log(snake.getIp());
                    return "";
                }

                return "No online snakes!";
            }
            else if (cmd.Equals("ban"))
            {
                try
                {
                    Snake.getSnake(args[1]).ban();
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
        internal static string startServer()
        {
            int[] positionList = new int[2];

            IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Any, Util._PORT);
            server = new Socket(IPAddress.Any.AddressFamily, SocketType.Stream, ProtocolType.Tcp);


            try
            {
                server.Bind(localEndPoint);
            }
            catch (SocketException)
            {
                return "Server is already running!";
            }

            ip = Dns.GetHostEntry("").AddressList[1].ToString();

            server.Listen(Util._MAX_PLAYERS);

            Util.getBannedIpList().AddRange(File.ReadAllLines(Util.banFileName));
            Util.Init();

            /// Start listening to snakes
            listenSnakes = new Thread(delegate () { ServerUtil.registerNewSnakes(server); });
            listenSnakes.IsBackground = true;
            listenSnakes.Start();
            return "Server started!";
        }

        /// <summary>
        /// Stops the server
        /// </summary>
        /// <returns></returns>
        internal static string stopServer(bool resetCmd)
        {
            server.Close();

            Snake.DisconnectFromAllSnakes();

            Snake.getSnakes().Clear();

            // Reset cmd
            if (resetCmd)
                Util.getForm().resetCmd();

            // Abort listening for snakes
            listenSnakes.Abort();

            return "Server stopped!";
        }
    }
}
