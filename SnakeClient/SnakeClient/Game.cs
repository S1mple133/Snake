using System;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeClient
{
    class Game
    {

        static public Player Player;

        static private Client Client;
        static private Random random;

        static public void Setup()
        {
            random = new Random();

            Client = new Client(Util.IP_ADDRESS, Util.PORT);
            byte[] buffer = Client.Receive(6);

            Util.MAX_PLAYERS = buffer[0];
            Util.GRID_SIZE = buffer[1];

            Player = new Player(buffer[2], buffer[3], buffer[4], buffer[5]);

            Client.Send(Player);
        }

        static public void Loop()
        {
            byte[] buffer = Client.Receive(Util.MAX_PLAYERS * 4 + 1);

            int[] usedIDs = new int[buffer[0]];
            int idx = 0;

            for (int i = 1; i < buffer[0] * 4 + 1; i += 4)
            {
                Snake actSnake = Snake.GetSnake(buffer[i]);

                if (actSnake == null)
                    Snake.AddSnake(buffer[i], buffer[i + 1], buffer[i + 2], buffer[i + 3], 255, random.Next(77, 177), random.Next(77, 177), random.Next(77, 177));
                else
                    actSnake.Update(buffer[i + 1], buffer[i + 2], buffer[i + 3]);

                usedIDs[idx] = buffer[i];
                idx++;
            }
            Snake.RemoveOfflineSnakes(usedIDs);

            Player.UpdatePosition();

            Client.Send(Player);
        }

        public static void DrawSnakes(PaintEventArgs e, ref Graphics frame, int scale, float offset)
        {
            Snake[] snakes = Snake.GetSnakes();

            foreach (Snake s in snakes)
            {
                Position[] tail = s.GetTail();
                for (int i = 0; i < tail.Length; i++)
                    if (s.Head.IsInBounds(Player.Position.X - Util.ZOOM / 2, Player.Position.Y - Util.ZOOM / 2, Player.Position.X + Util.ZOOM / 2, Player.Position.Y + Util.ZOOM / 2))
                        frame.FillRectangle(new SolidBrush(s.TailColor),
                            (tail[i].X - (Player.Position.X - Util.ZOOM / 2)) * scale + offset,
                            (tail[i].Y - (Player.Position.Y - Util.ZOOM / 2)) * scale + offset,
                            scale, scale);
                if (s.Head.IsInBounds(Player.Position.X - Util.ZOOM / 2, Player.Position.Y - Util.ZOOM / 2, Player.Position.X + Util.ZOOM / 2, Player.Position.Y + Util.ZOOM / 2))
                {
                    frame.FillRectangle(new SolidBrush(s.HeadColor),
                        (s.Head.X - (Player.Position.X - Util.ZOOM / 2)) * scale + offset,
                        (s.Head.Y - (Player.Position.Y - Util.ZOOM / 2)) * scale + offset,
                        scale, scale);
                }
                else Console.WriteLine("OUTSIDE: " + new Random().Next());
            }
        }

        public static void DrawGameBoarder(PaintEventArgs e, ref Graphics frame, int scale, float offset)
        {

        }

        public static void DrawWindowBoarder(PaintEventArgs e, ref Graphics frame, int canvasWidth, int canvasHeight, float offset)
        {
            frame.FillRectangle(new SolidBrush(Color.White), 0, 0, canvasWidth, offset);
            frame.FillRectangle(new SolidBrush(Color.White), 0, 0, offset, canvasHeight);
            frame.FillRectangle(new SolidBrush(Color.White), 0, canvasHeight - offset, canvasWidth, offset);
            frame.FillRectangle(new SolidBrush(Color.White), canvasWidth - offset, 0, offset, canvasHeight);
        }

        static public void DisconnectFromServer()
        {
            Client.Disconnect();
        }
    }
}
