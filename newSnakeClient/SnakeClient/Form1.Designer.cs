namespace SnakeClient
{
    partial class Form1
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
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).BeginInit();
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
            this.Canvas.Margin = new System.Windows.Forms.Padding(1);
            this.Canvas.Name = "Canvas";
            this.Canvas.Size = new System.Drawing.Size(620, 588);
            this.Canvas.TabIndex = 0;
            this.Canvas.TabStop = false;
            this.Canvas.Paint += new System.Windows.Forms.PaintEventHandler(this.UpdateCanvas);
            // 
            // onlinePlayersLabel
            // 
            this.onlinePlayersLabel.AutoSize = true;
            this.onlinePlayersLabel.BackColor = System.Drawing.Color.Black;
            this.onlinePlayersLabel.ForeColor = System.Drawing.Color.White;
            this.onlinePlayersLabel.Location = new System.Drawing.Point(683, 20);
            this.onlinePlayersLabel.Margin = new System.Windows.Forms.Padding(1, 0, 1, 0);
            this.onlinePlayersLabel.Name = "onlinePlayersLabel";
            this.onlinePlayersLabel.Size = new System.Drawing.Size(80, 13);
            this.onlinePlayersLabel.TabIndex = 1;
            this.onlinePlayersLabel.Text = "Online Players: ";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1068, 588);
            this.Controls.Add(this.onlinePlayersLabel);
            this.Controls.Add(this.Canvas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.Canvas)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label onlinePlayersLabel;
        public System.Windows.Forms.Timer GameLoop;
        public System.Windows.Forms.PictureBox Canvas;
    }
}

