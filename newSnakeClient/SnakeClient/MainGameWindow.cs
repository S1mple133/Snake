﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace SnakeClient
{

    public partial class MainGameWindow : Form
    {
        bool isFullscreen = false;

        public MainGameWindow()
        {
            InitializeComponent();

            Util.LoadConfig(Util.SETTINGS_FILE_NAME);

            SetWindowSize();

            GameLoop.Interval = Util.TICK_INTERVAL;
            GameLoop.Start();

            Game.Setup();
        }


        private void SetFullscreen(bool fullscreen)
        {
            if (fullscreen)
            {
                this.Bounds = Screen.PrimaryScreen.Bounds;
                this.Canvas.Size = new Size(Canvas.Height, Canvas.Height);
                this.onlinePlayersLabel.Location = new Point(Canvas.Height + 5, 10);
            }
            else
            {
                this.Size = new Size(Screen.PrimaryScreen.Bounds.Height / 2 + 200, Screen.PrimaryScreen.Bounds.Height / 2);
                this.Canvas.Size = new Size(Canvas.Height, Canvas.Height);
                this.onlinePlayersLabel.Location = new Point(Canvas.Height + 5, 10);
                this.CenterToScreen();
            }
        }

        public void SetWindowSize()
        {
            //Make screen a sqare

            this.Canvas.Size = new Size(Canvas.Height, Canvas.Height);
            if (!isFullscreen)
                this.Size = new Size(Canvas.Height + 200, Canvas.Height);
            this.onlinePlayersLabel.Location = new Point(Canvas.Height + 5, 10);
        }

        private void GameLoop_Tick(object sender, EventArgs e)
        {
            Game.Loop();
            if (Snake.GetSnakeAmount() != Util.ONLINE_PLAYERS)
            {
                Util.ONLINE_PLAYERS = Snake.GetSnakeAmount();
                SetOnlinePlayersLabel(Util.ONLINE_PLAYERS);
            }

            Canvas.Invalidate();
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
                Game.Player.Dir = Direction.UP;
            else if (e.KeyCode == Keys.A || e.KeyCode == Keys.Left)
                Game.Player.Dir = Direction.LEFT;
            else if (e.KeyCode == Keys.S || e.KeyCode == Keys.Down)
                Game.Player.Dir = Direction.DOWN;
            else if (e.KeyCode == Keys.D || e.KeyCode == Keys.Right)
                Game.Player.Dir = Direction.RIGHT;
            else if (e.KeyCode == Keys.Escape)
            {
                Game.DisconnectFromServer();
                Environment.Exit(0);
            }
            else if (e.KeyCode == Keys.F)
            {
                SetFullscreen(!isFullscreen);
                isFullscreen = !isFullscreen;
            }
        }

        public void SetOnlinePlayersLabel(int players)
        {
            onlinePlayersLabel.Text = "Online Players: " + players;
        }
    }
}