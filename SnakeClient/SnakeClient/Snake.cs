﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace SnakeClient
{
    class Snake
    {
        public int ID { get; set; }
        public Position Head { get; set; }

        public Color HeadColor;
        public Color TailColor;

        private int Length;
        private List<Position> Tail = new List<Position>();

        private static List<Snake> Snakes = new List<Snake>();
        Snake(int id, int x, int y, int length, int colorA, int colorR, int colorG, int colorB)
        {
            ID = id;
            Head = new Position(x, y);
            Length = length;
            HeadColor = Color.FromArgb(colorA, colorR, colorG, colorB);
            TailColor = Color.FromArgb(colorA, colorR-50, colorG-50, colorB-50);
        }

        public static int GetSnakeAmount()
        {
            return Snakes.Count();
        }

        public static Snake GetSnake(int id)
        {
            foreach (Snake s in Snakes)
                if (s.ID == id)
                    return s;
            return null;
        }

        public static void RemoveOfflineSnakes(int[] usedIDs)
        {
            int[] snakeListIDs = GetAllIDs();

            for (int i = 0; i < snakeListIDs.Length; i++)
            {
                bool isInArr = false;
                for (int j = 0; j < usedIDs.Length; j++)
                {
                    if (snakeListIDs[i] == usedIDs[j])
                    {
                        isInArr = true;
                        break;
                    }
                }
                if (!isInArr)
                {
                    GetSnake(snakeListIDs[i]).Remove();
                }
            }
        }

        private void Remove()
        {
            Snakes.Remove(this);
        }

        public static void AddSnake(int id, int x, int y, int length, int colorA, int colorR, int colorG, int colorB)
        {
            Snakes.Add(new Snake(id, x, y, length, colorA, colorR, colorG, colorB));
        }

        private static int[] GetAllIDs()
        {
            int[] IDs = new int[Snakes.Count()];
            int idx = 0;
            foreach (Snake s in Snakes)
            {
                IDs[idx] = s.ID;
                idx++;
            }
            return IDs;
        }

        public static Snake[] GetSnakes()
        {
            return Snakes.ToArray();
        }

        public Position[] GetTail()
        {
            return Tail.ToArray();
        }

        public void Update(int x, int y, int length)
        {
            Position tempPos = new Position(Head.X, Head.Y);

            Length = length;
            Head.X = x;
            Head.Y = y;

            Tail.Add(tempPos);
            if (Tail.Count() >= Length)
                Tail.RemoveAt(0);
        }
    }
}