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
    public class Util
    {
        public const int _SIZE = 25;
        public const string banFileName = "banned.csv";
        public const int _SNAKE_LENGTH = 4;
        public const int _MAX_PLAYERS = 10;
        public const bool _DEBUG = false;
        public const int _PORT = 4396;
        public const int _TIMEOUT_TIME = 5000;
        private static List<string> bannedList = new List<string>();
        private static bool[] idList = new bool[_MAX_PLAYERS];
        private static Form1 form = new Form1();
        internal static byte _GRIDSIZE= 25;

        /// <summary>
        /// Generate a random start position
        /// </summary>
        /// <returns></returns>
        public static int[] generateStartPositions()
        {
            int[] startPos = new int[2];
            Random rand = new Random();
            int cnt = 0;
            bool isValidPos;

            do
            {
                isValidPos = true;
                startPos[0] = rand.Next(0, _SIZE);
                startPos[1] = rand.Next(0, _SIZE);

                // Check x
                foreach (Snake snake in Snake.getSnakes())
                {
                    if (snake.getPositionList().Contains(startPos))
                    {
                        isValidPos = false;
                    }
                }
            } while (!isValidPos);

            return startPos;
        }

        internal static void addNewSnake(string ip)
        {
            form.onlinePlayers.Invoke((MethodInvoker)delegate { form.onlinePlayers.Text = Convert.ToString(Convert.ToInt32(Util.getForm().onlinePlayers.Text) + 1); });
            form.addOnlineSnake(ip + "\r\n");
            log("Snake connected from " + ip);
        }

        /// <summary>
        /// Initiates the GUI
        /// </summary>
        public static void Init()
        {
            string bannedSnakes = "";

            // Add bans to ban list
            foreach (string bannedIp in bannedList)
            {
                bannedSnakes = bannedSnakes + bannedIp + "\r\n";
            }

            getForm().setBannedSnakes(bannedSnakes);

            bannedList.Clear();
        }

        /// <summary>
        /// Generates ID for a snake
        /// </summary>
        /// <returns></returns>
        public static int getId()
        {
            for (int i = 0; i < idList.Length; i++)
            {
                if(!idList[i])
                {
                    return i;
                }
            }

            return -1;
        }

        /// <summary>
        /// Returns the form
        /// </summary>
        /// <returns></returns>
        public static Form1 getForm()
        {
            return form;
        }

        /// <summary>
        /// Log to console
        /// </summary>
        /// <param name="v"></param>
        public static void log(string v)
        {
            if(v == "")
            {
                return;
            }

            form.printLine(String.Format("[{0}] " + v + "\r\n", DateTime.Now));
        }

        /// <summary>
        /// Returns the list of the banned ips
        /// </summary>
        /// <returns></returns>
        public static List<string> getBannedIpList()
        {
            return bannedList;
        }

        /// <summary>
        /// Log data to console
        /// </summary>
        /// <param name="buffer"></param>
        public static void logData(byte[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                log(i + ": " + buffer[i]);
            }
        }

        /// <summary>
        /// Removes an ID of a snake
        /// </summary>
        public static void removeId(int id) => idList[id] = false;
    }
}
