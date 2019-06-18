namespace SnakeClient
{
    partial class MainGameWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.GameLoop = new System.Windows.Forms.Timer(this.components);
            this.Canvas = new System.Windows.Forms.PictureBox();
            this.onlinePlayersLabel = new System.Windows.Forms.Label();
            this.miniMap = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.miniMap)).BeginInit();
            this.SuspendLayout();
            // 
            // GameLoop
            // 
            this.GameLoop.Interval = 500;
            this.GameLoop.Tick += new System.EventHandler(this.GameLoop_Tick);
            // 
            // Canvas
            // 
            this.Canvas.BackColor = System.Drawing.Color.Black;
            this.Canvas.Dock = System.Windows.Forms.DockStyle.Left;
            this.Canvas.Location = new System.Drawing.Point(0, 0);
            this.Canvas.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(930, 905);
            this.Canvas.TabIndex = 0;
            this.Canvas.TabStop = false;
            this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.UpdateCanvas);
            // 
            // onlinePlayersLabel
            // 
            this.onlinePlayersLabel.AutoSize = true;
            this.onlinePlayersLabel.BackColor = System.Drawing.Color.Black;
            this.onlinePlayersLabel.ForeColor = System.Drawing.Color.White;
            this.onlinePlayersLabel.Location = new System.Drawing.Point(1024, 31);
            this.onlinePlayersLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.onlinePlayersLabel.Name = "onlinePlayersLabel";
            this.onlinePlayersLabel.Size = new System.Drawing.Size(117, 20);
            this.onlinePlayersLabel.TabIndex = 1;
            this.onlinePlayersLabel.Text = "Online Players: ";
            // 
            // miniMap
            // 
            this.miniMap.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.miniMap.Location = new System.Drawing.Point(930, 233);
            this.miniMap.Name = "miniMap";
            this.miniMap.Size = new System.Drawing.Size(672, 672);
            this.miniMap.TabIndex = 2;
            this.miniMap.TabStop = false;
            this.miniMap.Paint += new System.Windows.Forms.PaintEventHandler(this.UpdateMinimap);
            // 
            // MainGameWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1602, 905);
            this.Controls.Add(this.miniMap);
            this.Controls.Add(this.onlinePlayersLabel);
            this.Controls.Add(this.Canvas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "MainGameWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.miniMap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label onlinePlayersLabel;
        public System.Windows.Forms.Timer GameLoop;
        public System.Windows.Forms.PictureBox Canvas;
        private System.Windows.Forms.PictureBox miniMap;
    }
}

