using System;

namespace SnakeClient
{
    class Game
    {

        public Client Client;
        public Player Player;

        private Random random;

        public void Setup(Form1 form)
        {
            //this.Canvas.Size = new Size(Screen.PrimaryScreen.Bounds.Height, 996);
            //GoFullscreen(true);
            form.SetWindowSize();

            //TODO: read from file OR read information throw textBox
            //string ipAddress = "192.168.0.165";

            random = new Random();

            string ipAddress = "127.0.0.1";
            int port = 4396;
            int tickInterval = 250;

            form.GameLoop.Interval = tickInterval;

            Client = new Client(ipAddress, port);
            byte[] buffer = Client.Receive(6);

            Util.MAX_PLAYERS = buffer[0];
            Util.GRID_SIZE = buffer[1];

            Player = new Player(buffer[2], buffer[3], buffer[4], buffer[5]);

            Client.Send(Player);

            form.GameLoop.Start();
        }

        public void Loop(Form1 form)
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
                {
                    actSnake.Update(buffer[i + 1], buffer[i + 2], buffer[i + 3]);
                    usedIDs[idx] = buffer[i];
                    idx++;
                }
            }
            Snake.RemoveOfflineSnakes(usedIDs);

            if (Snake.GetSnakeAmount() != Util.ONLINE_PLAYERS)
            {
                Util.ONLINE_PLAYERS = Snake.GetSnakeAmount();
                form.SetOnlinePlayersLabel(Util.ONLINE_PLAYERS);
            }
            form.Canvas.Invalidate();

            Player.UpdatePosition();

            Client.Send(Player);
        }
    }
}
