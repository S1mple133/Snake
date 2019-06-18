// Pavelescu Darius

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace Util
{
    public class Snake
    {
        private static List<Snake> snakeList = new List<Snake>();
        private static int accumulatedData = 0;
        private string ip;
        static int X = 0;
        private Thread listenForData;
        static int Y = 1;
        private int[] position;
        private Socket client;
        private int length;
        private List<int[]> positionList;
        private byte[] buffer;
        private int id;

        public Snake(Socket client)
        {
            this.position = Util.GenerateStartPositions();
            this.length = Util.SIZE;
            this.positionList = new List<int[]>();
            this.client = client;
            this.buffer = new byte[3];
            this.ip = (client.LocalEndPoint as IPEndPoint).Address.ToString();
            this.id = Util.GetId();

            // Send game settings
            client.Send(new byte[] { (byte)Util.MAX_PLAYERS, (byte)Util.SIZE, (byte)id, (byte)Util.SNAKE_LENGTH, (byte)position[X], (byte)position[Y] });

            snakeList.Add(this);

            // Start listening for data
            this.listenForData = new Thread(delegate () { ServerUtil.ListenForData(this); });
            listenForData.Start();

            Util.AddNewSnake(ip);
        }

        internal void Message(byte[] buffer)
        {

        }

        public void Disconnect()
        {
            GetClient().Shutdown(SocketShutdown.Both);
            GetClient().Disconnect(true);
        }

        public static void DisconnectFromAllSnakes()
        {
            foreach (Snake snake in GetSnakes())
                snake.Disconnect();

            snakeList.Clear();
        }

        public int Id => id;

        /// <summary>
        /// Returns the data array
        /// </summary>
        /// <returns></returns>
        public byte[] GetBuffer()
        {
            return buffer;
        }

        /// <summary>
        /// Get snake by ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static Snake GetSnake(string ip)
        {
            foreach (Snake snake in snakeList)
            {
                if (snake.GetIp().Equals(ip))
                {
                    return snake;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the ip of the client
        /// </summary>
        /// <returns></returns>
        public string GetIp()
        {
            return ip;
        }

        /// <summary>
        /// Updates the position of a snake
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="len"></param>
        public void Update(int x, int y, int len)
        {
            byte[] allData;

            if (position.Length < 2)
            {
                return;
            }

            // Calculate positions
            this.length = len;

            if (length > positionList.Count)
            {
                this.positionList.Add(position);
            }
            else
            {
                for (int i = 0; i < positionList.Count - 1; i++)
                {
                    positionList.ToArray()[i] = positionList.ToArray()[i + 1];
                }

                if (positionList.Count > 0)
                    positionList.ToArray()[positionList.Count - 1] = position;
            }

            this.position = new int[] { x, y };

            CheckSnakeCollision();
            accumulatedData++;

            // Output debug
            if (Util.DEBUG)
            {
                Util.log("Became data from " + (client.RemoteEndPoint as IPEndPoint).Address);
                Util.LogData(buffer);
            }


            if (accumulatedData >= snakeList.Count)
            {
                allData = GetData();

                // Saved snakeList into new Array, so it wouldn't throw
                // Error when a snake connects (is being added to the List)
                // while the foreach loop is running
                int snakes = snakeList.Count;
                Snake[] snakeArray = snakeList.ToArray();

                // Send data to other snakes
                foreach (Snake snake in snakeArray)
                {
                    snake.GetClient().Send(allData);

                    if (Util.DEBUG)
                    {
                        Util.log("Sent data to " + (snake.GetClient().RemoteEndPoint as IPEndPoint).Address);
                        Util.LogData(allData);
                    }
                }


                accumulatedData = 0;
            }
        }

        /// <summary>
        /// Stop listening for data from this snake
        /// </summary>
        private void StopListeningForData()
        {
            listenForData = null;
        }

        /// <summary>
        /// Removes a snake
        /// </summary>
        public void Remove(KickCode kickCode)
        {
            StopListeningForData();

            length = 0;
            snakeList.Remove(this);

            Util.Form.onlinePlayers.Invoke((MethodInvoker)delegate { Util.Form.onlinePlayers.Text = Convert.ToString(Convert.ToInt32(Util.Form.onlinePlayers.Text) - 1); });

            // Remove snake from online list
            string ipAddress = "";

            for (int i = 0; i < snakeList.Count; i++)
            {
                ipAddress = ipAddress + ip + "\r\n";
            }

            Util.Form.setOnlineSnakes(ipAddress);

            Util.RemoveId(id);

            // Send disconnect cause
            if (kickCode != KickCode.NONE)
            {
                try
                {
                    GetClient().Send(new byte[] { 0, (byte)kickCode });
                }
                catch (SocketException)
                {
                    return;
                }
            }

            try
            {
                GetClient().Shutdown(SocketShutdown.Both);
                GetClient().Disconnect(true);
            } catch(SocketException)
            {

            }

            Util.log("Snake disconnected from " + ip);
        }

        /// <summary>
        /// Kills a snake
        /// </summary>
        public void Kill()
        {
            Remove(KickCode.DEAD);
            GetClient().Disconnect(true);
        }

        /// <summary>
        /// Ban IP
        /// </summary>
        /// <param name="ip"></param>
        public void Ban()
        {
            Util.BannedIpList.Add(ip);
            File.AppendAllLines(Util.BanFileName, new string[] { ip + ";" });
            Util.Form.addBannedSnake(ip + "\r\n");
            Kick(KickCode.BAN);
        }

        /// <summary>
        /// Kick a snake
        /// </summary>
        internal void Kick(KickCode reason)
        {
            Remove(reason);
        }

        /// <summary>
        /// Checks if the snake collided with an other snake
        /// </summary>
        private void CheckSnakeCollision()
        {
            foreach (Snake snake in snakeList)
            {
                if (snake.PositionList.Contains(position))
                {
                    this.Kill();
                }
            }
        }

        /// <summary>
        /// Returns the position of the snake
        /// </summary>
        /// <returns></returns>
        public int[] Position => position;

        /// <summary>
        /// Returns the length of the snake
        /// </summary>
        /// <returns></returns>
        public int Length => length;

        /// <summary>
        /// Gets the Position as String
        /// </summary>
        /// <returns></returns>
        private byte[] GetData()
        {
            byte[] data = new byte[Util.MAX_PLAYERS * 4 + 1];
            data[0] = (byte)snakeList.Count;
            Snake snake;
            int cnt = 1;

            for (int i = 1; i <= snakeList.Count; i++)
            {
                snake = snakeList.ToArray()[i - 1];

                data[cnt] = (byte)snake.Id;
                data[cnt + 1] = (byte)snake.Position[X];
                data[cnt + 2] = (byte)snake.Position[Y];
                data[cnt + 3] = (byte)snake.Length;

                cnt += 4;
            }

            return data;
        }

        /// <summary>
        /// Returns the Socket of the User
        /// </summary>
        /// <returns></returns>
        public Socket GetClient()
        {
            return client;
        }

        /// <summary>
        /// Returns the Snake with the specified ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Snake GetSnake(Socket client)
        {
            foreach (Snake snake in snakeList)
            {
                if (snake.GetClient() == client)
                {
                    return snake;
                }
            }
            return null;
        }

        /// <summary>
        /// Returns the position list
        /// </summary>
        /// <returns></returns>
        public List<int[]> PositionList => positionList;

        /// <summary>
        /// Returns the online snakes
        /// </summary>
        /// <returns></returns>
        public static Snake[] GetSnakes() => snakeList.ToArray();
    }
}
