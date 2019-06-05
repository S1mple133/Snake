using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Snake_Server
{
    public class Util
    {
        public const int _SIZE = 25;
        public const string banFileName = "banned.csv";
        public const int _SNAKE_LENGTH = 4;
        public const int _MAX_PLAYERS = 10;
        public const bool _DEBUG = true;
        public const int _TIMEOUT_TIME = 5000;
        private static List<string> bannedList = new List<string>();
        internal static bool lastLineCmdChar = false;
        private static bool[] idList = new bool[_MAX_PLAYERS];

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
        /// Log to console
        /// </summary>
        /// <param name="v"></param>
        public static void log(string v)
        {
            if(v == "")
            {
                return;
            }

            if(lastLineCmdChar)
            {
                Console.CursorTop = Console.CursorTop - 1;
            }

            Console.CursorLeft = 0;
            Console.WriteLine("[" + DateTime.Now.Date + "] " + v);
            Console.Write("$ ");
            lastLineCmdChar = true;
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
                Console.WriteLine(i + ": " + buffer[i]);
            }

            lastLineCmdChar = false;
        }

        /// <summary>
        /// Removes an ID of a snake
        /// </summary>
        public static void removeId(int id) => idList[id] = false;
    }
}
