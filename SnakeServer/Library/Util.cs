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
        private const int sIZE = 25; // Size of board
        private const string banFileName = "banned.csv"; // Name of ban file
        private const int sNAKE_LENGTH = 4; // Default snake length
        private const int mAX_PLAYERS = 10; // Max players
        private const bool dEBUG = false; // Debug mode
        private const int pORT = 4396; // Port to listen for
        private const int tIMEOUT_TIME = 5000; // Time to listen for for every connection
        private static bool[] idList = new bool[MAX_PLAYERS]; // id list


        public static List<string> BannedIpList { get; } = new List<string>();

        /// <summary>
        /// Form for the console
        /// </summary>
        public static Form1 Form { get; } = new Form1();

        /// <summary>
        /// Time to listen for every client to send data
        /// </summary>
        public static int TIMEOUT_TIME => tIMEOUT_TIME;

        /// <summary>
        /// Port to listen for
        /// </summary>
        public static int PORT => pORT;

        /// <summary>
        /// Debug mode
        /// </summary>
        public static bool DEBUG => dEBUG;

        /// <summary>
        /// Max players
        /// </summary>
        public static int MAX_PLAYERS => mAX_PLAYERS;

        /// <summary>
        /// Default length of snake
        /// </summary>
        public static int SNAKE_LENGTH => sNAKE_LENGTH;

        /// <summary>
        /// Name of the ban file
        /// </summary>
        public static string BanFileName => banFileName;

        /// <summary>
        /// Size of board
        /// </summary>
        public static int SIZE => sIZE;

        /// <summary>
        /// Generate a random start position
        /// </summary>
        /// <returns></returns>
        public static int[] GenerateStartPositions()
        {
            int[] startPos = new int[2];
            Random rand = new Random();
            int cnt = 0;
            bool isValidPos;

            do
            {
                isValidPos = true;
                startPos[0] = rand.Next(0, SIZE);
                startPos[1] = rand.Next(0, SIZE);

                // Check x
                foreach (Snake snake in Snake.GetSnakes())
                {
                    if (snake.PositionList.Contains(startPos))
                    {
                        isValidPos = false;
                    }
                }
            } while (!isValidPos);

            return startPos;
        }

        internal static void AddNewSnake(string ip)
        {
            Form.onlinePlayers.Invoke((MethodInvoker)delegate { Form.onlinePlayers.Text = Convert.ToString(Convert.ToInt32(Util.Form.onlinePlayers.Text) + 1); });
            Form.addOnlineSnake(ip + "\r\n");
            log("Snake connected from " + ip);
        }

        /// <summary>
        /// Initiates the Ban list
        /// </summary>
        public static void InitBanList()
        {
            BannedIpList.AddRange(File.ReadAllLines(BanFileName));
            string bannedSnakes = "";

            // Add bans to ban list
            foreach (string bannedIp in BannedIpList)
            {
                bannedSnakes = bannedSnakes + bannedIp + "\r\n";
            }

            Form.setBannedSnakes(bannedSnakes);

            BannedIpList.Clear();
        }

        /// <summary>
        /// Generates ID for a snake
        /// </summary>
        /// <returns></returns>
        public static int GetId()
        {
            for (int i = 0; i < idList.Length; i++)
            {
                if(!idList[i])
                {
                    idList[i] = true;
                    return i;
                }
            }

            return -1;
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

            Form.printLine(String.Format("[{0}] " + v + "\r\n", DateTime.Now));
        }

        /// <summary>
        /// Log data to console
        /// </summary>
        /// <param name="buffer"></param>
        public static void LogData(byte[] buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                log(i + ": " + buffer[i]);
            }
        }

        /// <summary>
        /// Removes an ID of a snake
        /// </summary>
        public static void RemoveId(int id) => idList[id] = false;
    }

    public enum KickCode
    {
        KICK,
        BAN,
        DEAD,
        NONE
    }
}
