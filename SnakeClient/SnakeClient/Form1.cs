using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SnakeClient
{

    public partial class Form1 : Form
    {
        Client client;
        Player player;

        private static Random random;

        public Form1()
        {
            InitializeComponent();

            //this.Canvas.Size = new Size(Screen.PrimaryScreen.Bounds.Height, 996);
            //GoFullscreen(true);

            //TODO: read from file OR read information throw textBox
            //string ipAddress = "192.168.0.165";

            random = new Random();

            string ipAddress = "127.0.0.1";
            int port = 4396;
            int tickInterval = 250;

            GameLoop.Interval = tickInterval;

            client = new Client(ipAddress, port);
            byte[] buffer = client.Receive(6);

            Util.MAX_PLAYERS = buffer[0];
            Util.GRID_SIZE = buffer[1];

            player = new Player(buffer[2], buffer[3], buffer[4], buffer[5]);

            client.Send(player);

            GameLoop.Start();
        }


        private void GoFullscreen(bool fullscreen)
        {
            if (fullscreen)
            {
                this.WindowState = FormWindowState.Normal;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                this.Bounds = Screen.PrimaryScreen.Bounds;
            }
            else
            {
                this.WindowState = FormWindowState.Maximized;
                this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                this.Canvas.Size = new Size(500, Canvas.Height);
            }
        }

        private void GameLoop_Tick(object sender, EventArgs e)
        {

            byte[] buffer = client.Receive(Util.MAX_PLAYERS * 4 + 1);

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
                SetOnlinePlayersLabel(Util.ONLINE_PLAYERS);
            }

            //Make screen a sqare
            this.Size = new Size(Canvas.Height + 200, Canvas.Height);
            this.Canvas.Size = new Size(Canvas.Height, Canvas.Height);
            this.onlinePlayersLabel.Location = new Point(Canvas.Height+5,10);


            Canvas.Invalidate();
            
            player.UpdatePosition();

            client.Send(player);

        }

        private void UpdateCanvas(object sender, PaintEventArgs e)
        {
            Snake[] snakes = Snake.GetSnakes();

            int scale = Canvas.Height / Util.GRID_SIZE;
            float offset = (Canvas.Height - scale * Util.GRID_SIZE) / 2;

            Graphics frame = e.Graphics;

            //Draw Boarder
            frame.FillRectangle(new SolidBrush(Color.White), 0, 0, Canvas.Width, offset);
            frame.FillRectangle(new SolidBrush(Color.White), 0, 0, offset, Canvas.Height);
            frame.FillRectangle(new SolidBrush(Color.White), 0, Canvas.Height - offset, Canvas.Width, offset);
            frame.FillRectangle(new SolidBrush(Color.White), Canvas.Width - offset, 0, offset, Canvas.Height);

            //Draw snakes
            foreach (Snake s in snakes)
            {
                Position[] tail = s.GetTail();
                for (int i = 0; i < tail.Length; i++)
                    frame.FillRectangle(new SolidBrush(s.TailColor), tail[i].X * scale + offset, tail[i].Y * scale + offset, scale, scale);

                frame.FillRectangle(new SolidBrush(s.HeadColor), s.Head.X * scale + offset, s.Head.Y * scale + offset, scale, scale);
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.W || e.KeyCode == Keys.Up)
                player.Dir = Direction.UP;
            else if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
                player.Dir = Direction.LEFT;
            else if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
                player.Dir = Direction.DOWN;
            else if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
                player.Dir = Direction.RIGHT;
            else if (e.KeyCode == Keys.Escape)
            {
                client.Disconnect();
                Application.Exit();
            }
        }

        private void SetOnlinePlayersLabel(int players)
        {
            onlinePlayersLabel.Text = "Online Players: " + players;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {

        }
    }
}
