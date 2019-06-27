using System;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeClient
{
    class Game
    {

        static public Player Player;

        static public Client Client;
        static private Random random;

        static public void Setup()
        {
            random = new Random();

            Client = new Client(Util.IP_ADDRESS, Util.PORT);
            byte[] buffer = Client.Receive(7);

            Util.MAX_PLAYERS = buffer[0];
            Util.GRID_SIZE = buffer[1];
            Util.ZOOM = buffer[2];

            Player = new Player(buffer[3], buffer[4], buffer[5], buffer[6]);
            //Player = new Player(buffer[3], buffer[4], 5, 250);

            Client.Send(Player);
        }

        static public void Loop(int onlinePlayers)
        {
            byte[] buffer = Client.Receive(Util.MAX_PLAYERS * 4);

            int[] usedIDs = new int[onlinePlayers];
            int idx = 0;

            for (int i = 0; i < onlinePlayers * 4 ; i += 4)
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
                    if (s.Head.IsInBounds(Player.Position.X - (float)Util.ZOOM / 2, Player.Position.Y - (float)Util.ZOOM / 2, Player.Position.X + (float)Util.ZOOM / 2, Player.Position.Y + (float)Util.ZOOM / 2))
                        frame.FillRectangle(new SolidBrush(s.TailColor),
                            (tail[i].X - (Player.Position.X - (float)Util.ZOOM / 2)) * scale + offset - (float)scale / 2,
                            (tail[i].Y - (Player.Position.Y - (float)Util.ZOOM / 2)) * scale + offset - (float)scale / 2,
                            scale, scale);

                if (s.Head.IsInBounds(Player.Position.X - (float)Util.ZOOM / 2, Player.Position.Y - (float)Util.ZOOM / 2, Player.Position.X + (float)Util.ZOOM / 2, Player.Position.Y + (float)Util.ZOOM / 2))
                    frame.FillRectangle(new SolidBrush(s.HeadColor),
                        (s.Head.X - (Player.Position.X - (float)Util.ZOOM / 2)) * scale + offset - (float)scale / 2,
                        (s.Head.Y - (Player.Position.Y - (float)Util.ZOOM / 2)) * scale + offset - (float)scale / 2,
                        scale, scale);
            }
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
