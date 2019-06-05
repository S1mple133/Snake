// Pavelescu Darius

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Util;

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
            this.position = Util.generateStartPositions();
            this.length = Util._SIZE;
            this.positionList = new List<int[]>();
            this.client = client;
            this.buffer = new byte[3];
            this.ip = (client.LocalEndPoint as IPEndPoint).Address.ToString();
            this.id = Util.getId();

            // Send game settings
            client.Send(new byte[] { (byte)Util._MAX_PLAYERS, Util._SIZE, (byte)id, Util._SNAKE_LENGTH, (byte)position[X], (byte)position[Y] });

            snakeList.Add(this);

            // Start listening for data
            this.listenForData = new Thread(delegate () { ServerUtil.listenForData(this); });
            listenForData.Start();

            Util.addNewSnake(ip);
        }

        public int getId()
        {
            return id;
        }

        /// <summary>
        /// Returns the data array
        /// </summary>
        /// <returns></returns>
        public byte[] getBuffer()
        {
            return buffer;
        }

        /// <summary>
        /// Get snake by ip
        /// </summary>
        /// <param name="ip"></param>
        /// <returns></returns>
        public static Snake getSnake(string ip)
        {
            foreach (Snake snake in snakeList)
            {
                if (snake.getIp().Equals(ip))
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
        public string getIp()
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

            checkSnakeCollision();
            accumulatedData++;

            // Output debug
            if (Util._DEBUG)
            {
                Util.log("Became data from " + (client.RemoteEndPoint as IPEndPoint).Address);
                Util.logData(buffer);
            }


            if (accumulatedData == snakeList.Count)
            {
                allData = getData();

                // Saved snakeList into new Array, so it wouldn't throw
                // Error when a snake connects (is being added to the List)
                // while the foreach loop is running
                int snakes = snakeList.Count;
                Snake[] snakeArray = snakeList.ToArray();

                // Send data to other snakes
                foreach (Snake snake in snakeArray)
                {
                    snake.getClient().Send(allData);

                    if (Util._DEBUG)
                    {
                        Util.log("Sent data to " + (snake.getClient().RemoteEndPoint as IPEndPoint).Address);
                        Util.logData(allData);
                    }
                }
                

                accumulatedData = 0;
            }
        }

        /// <summary>
        /// Stop listening for data from this snake
        /// </summary>
        private void stopListeningForData()
        {
            listenForData.Abort();
        }

        /// <summary>
        /// Removes a snake
        /// </summary>
        public void remove()
        {
            Util.log("Snake disconnected from " + ip);
            this.length = 0;
            snakeList.Remove(this);

            Util.getForm().onlinePlayers.Invoke((MethodInvoker)delegate { Util.getForm().onlinePlayers.Text = Convert.ToString(Convert.ToInt32(Util.getForm().onlinePlayers.Text) - 1); });

            // Remove snake from online list
            string ipAddress = "";

            for (int i = 0; i < snakeList.Count; i++)
            {
                ipAddress = ipAddress + ip + "\r\n";
            }

            Util.getForm().setOnlineSnakes(ipAddress);

            Util.removeId(id);

            try
            {
                getClient().Disconnect(true);
            }
            catch (SocketException)
            {
                return;
            }

            stopListeningForData();
        }

        /// <summary>
        /// Kills a snake
        /// </summary>
        public void kill()
        {
            remove();
            getClient().Disconnect(true);
        }

        /// <summary>
        /// Ban IP
        /// </summary>
        /// <param name="ip"></param>
        public void ban()
        {
            Util.getBannedIpList().Add(ip);
            File.AppendText(ip + ";");
            Util.getForm().addBannedSnake(ip + "\r\n");
            kick();
        }

        /// <summary>
        /// Kick a snake
        /// </summary>
        internal void kick()
        {
            remove();
            getClient().Disconnect(true);
        }

        /// <summary>
        /// Checks if the snake collided with an other snake
        /// </summary>
        private void checkSnakeCollision()
        {
            foreach (Snake snake in snakeList)
            {
                if (snake.getPositionList().Contains(position))
                {
                    this.kill();
                }
            }
        }

        /// <summary>
        /// Returns the position of the snake
        /// </summary>
        /// <returns></returns>
        public int[] getPosition()
        {
            return position;
        }

        /// <summary>
        /// Returns the length of the snake
        /// </summary>
        /// <returns></returns>
        public int getLength()
        {
            return length;
        }

        /// <summary>
        /// Gets the Position as String
        /// </summary>
        /// <returns></returns>
        private byte[] getData()
        {
            byte[] data = new byte[Util._MAX_PLAYERS * 4 + 1];
            data[0] = (byte)snakeList.Count;
            Snake snake;
            int cnt = 1;

            for (int i = 1; i <= snakeList.Count; i++)
            {
                snake = snakeList.ToArray()[i - 1];

                data[cnt] = (byte)snake.getId();
                data[cnt + 1] = (byte)snake.getPosition()[X];
                data[cnt + 2] = (byte)snake.getPosition()[Y];
                data[cnt + 3] = (byte)snake.getLength();

                cnt += 4;
            }

            return data;
        }

        /// <summary>
        /// Returns the Socket of the User
        /// </summary>
        /// <returns></returns>
        public Socket getClient()
        {
            return client;
        }

        /// <summary>
        /// Returns the Snake with the specified ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Snake getSnake(Socket client)
        {
            foreach (Snake snake in snakeList)
            {
                if (snake.getClient() == client)
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
        public List<int[]> getPositionList()
        {
            return positionList;
        }

        /// <summary>
        /// Returns the online snakes
        /// </summary>
        /// <returns></returns>
        public static List<Snake> getSnakes()
        {
            return snakeList;
        }
    }
}
