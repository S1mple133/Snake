using System;
using System.Windows.Forms;

namespace Util
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

        /// <summary>
        /// Outputs line to console
        /// </summary>
        /// <param name="v"></param>
        public void printLine(string v)
        {
            this.commandLine.Invoke((MethodInvoker)delegate { this.commandLine.Text += v; });
            this.commandLine.Invoke((MethodInvoker)delegate { commandLine.ScrollToCaret(); });
        }


        /// <summary>
        /// Sets the online snake list
        /// </summary>
        /// <param name="snakes"></param>
        public void setOnlineSnakes(string snakes)
        { 
            this.onlineSnakes.Invoke((MethodInvoker)delegate { this.onlineSnakes.Text = snakes; });
        }

        /// <summary>
        /// Gets the online snake list
        /// </summary>
        /// <returns></returns>
        public string[] getOnlineSnakes()
        {
            string[] snakes = null;
            this.onlineSnakes.Invoke((MethodInvoker)delegate { snakes = this.onlineSnakes.Lines; });

            return snakes;
        }

        public void resetCmd()
        {
                this.onlineSnakes.Invoke((MethodInvoker)delegate { this.onlineSnakes.Text = ""; });
                this.commandLine.Invoke((MethodInvoker)delegate { this.commandLine.Text = ""; });
                this.bannedIps.Invoke((MethodInvoker)delegate { this.bannedIps.Text = ""; });
        }

        /// <summary>
        /// Adds a banned snake to the list
        /// </summary>
        /// <param name="ip"></param>
        public void addBannedSnake(string ip)
        {
            this.bannedIps.Invoke((MethodInvoker)delegate { this.bannedIps.Text = this.onlineSnakes.Text + ip; });
        }

        /// <summary>
        /// Sets the banned snake list
        /// </summary>
        /// <param name="ip"></param>
        public void setBannedSnakes(string ip)
        {
            this.bannedIps.Invoke((MethodInvoker)delegate { this.bannedIps.Text = ip; });
        }

        /// <summary>
        /// Adds online snake to the list
        /// </summary>
        /// <param name="ip"></param>
        public void addOnlineSnake(string ip)
        {
            this.onlineSnakes.Invoke((MethodInvoker)delegate { onlineSnakes.AppendText(ip); });
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.inputLine = new System.Windows.Forms.TextBox();
            this.commandLine = new System.Windows.Forms.TextBox();
            this.onlineSnakes = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bannedIps = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.stopButton = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.onLabel = new System.Windows.Forms.Label();
            this.offLabel = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.ipLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.ipLocal = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.portNumb = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.onlinePlayers = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // inputLine
            // 
            this.inputLine.Font = new System.Drawing.Font("Courier New", 10F);
            this.inputLine.Location = new System.Drawing.Point(446, 723);
            this.inputLine.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.inputLine.Multiline = true;
            this.inputLine.Name = "inputLine";
            this.inputLine.Size = new System.Drawing.Size(669, 29);
            this.inputLine.TabIndex = 1;
            this.inputLine.TextChanged += new System.EventHandler(this.inputLine_TextChanged);
            // 
            // commandLine
            // 
            this.commandLine.Font = new System.Drawing.Font("Courier New", 10F);
            this.commandLine.Location = new System.Drawing.Point(446, 64);
            this.commandLine.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.commandLine.Multiline = true;
            this.commandLine.Name = "commandLine";
            this.commandLine.ReadOnly = true;
            this.commandLine.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.commandLine.Size = new System.Drawing.Size(669, 660);
            this.commandLine.TabIndex = 1;
            // 
            // onlineSnakes
            // 
            this.onlineSnakes.Font = new System.Drawing.Font("Courier New", 10F);
            this.onlineSnakes.Location = new System.Drawing.Point(30, 64);
            this.onlineSnakes.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.onlineSnakes.Multiline = true;
            this.onlineSnakes.Name = "onlineSnakes";
            this.onlineSnakes.ReadOnly = true;
            this.onlineSnakes.Size = new System.Drawing.Size(372, 307);
            this.onlineSnakes.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Candara", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(30, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 23);
            this.label1.TabIndex = 3;
            this.label1.Text = "Online Snakes";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Candara", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(30, 383);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "Banned Ips";
            // 
            // bannedIps
            // 
            this.bannedIps.Font = new System.Drawing.Font("Courier New", 10F);
            this.bannedIps.Location = new System.Drawing.Point(30, 423);
            this.bannedIps.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.bannedIps.Multiline = true;
            this.bannedIps.Name = "bannedIps";
            this.bannedIps.ReadOnly = true;
            this.bannedIps.Size = new System.Drawing.Size(372, 329);
            this.bannedIps.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Candara", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(443, 23);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "Console";
            // 
            // stopButton
            // 
            this.stopButton.Location = new System.Drawing.Point(1345, 715);
            this.stopButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(89, 36);
            this.stopButton.TabIndex = 7;
            this.stopButton.Text = "STOP";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.button1_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1159, 715);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(89, 36);
            this.button1.TabIndex = 8;
            this.button1.Text = "START";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Courier New", 10F);
            this.label4.Location = new System.Drawing.Point(1189, 659);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 17);
            this.label4.TabIndex = 9;
            this.label4.Text = "Status:";
            // 
            // onLabel
            // 
            this.onLabel.AutoSize = true;
            this.onLabel.BackColor = System.Drawing.Color.LimeGreen;
            this.onLabel.Font = new System.Drawing.Font("Courier New", 10F);
            this.onLabel.Location = new System.Drawing.Point(1342, 659);
            this.onLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.onLabel.Name = "onLabel";
            this.onLabel.Size = new System.Drawing.Size(24, 17);
            this.onLabel.TabIndex = 10;
            this.onLabel.Text = "ON";
            // 
            // offLabel
            // 
            this.offLabel.AutoSize = true;
            this.offLabel.BackColor = System.Drawing.Color.Red;
            this.offLabel.Font = new System.Drawing.Font("Courier New", 10F);
            this.offLabel.Location = new System.Drawing.Point(1379, 659);
            this.offLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.offLabel.Name = "offLabel";
            this.offLabel.Size = new System.Drawing.Size(32, 17);
            this.offLabel.TabIndex = 11;
            this.offLabel.Text = "OFF";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Courier New", 10F);
            this.label5.Location = new System.Drawing.Point(1152, 614);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(104, 17);
            this.label5.TabIndex = 12;
            this.label5.Text = "External IP:";
            // 
            // ipLabel
            // 
            this.ipLabel.AutoSize = true;
            this.ipLabel.Font = new System.Drawing.Font("Courier New", 10F);
            this.ipLabel.Location = new System.Drawing.Point(1342, 614);
            this.ipLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ipLabel.Name = "ipLabel";
            this.ipLabel.Size = new System.Drawing.Size(64, 17);
            this.ipLabel.TabIndex = 13;
            this.ipLabel.Text = "ipLabel";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Courier New", 10F);
            this.label6.Location = new System.Drawing.Point(1175, 576);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(80, 17);
            this.label6.TabIndex = 14;
            this.label6.Text = "Local IP:";
            // 
            // ipLocal
            // 
            this.ipLocal.AutoSize = true;
            this.ipLocal.Font = new System.Drawing.Font("Courier New", 10F);
            this.ipLocal.Location = new System.Drawing.Point(1342, 576);
            this.ipLocal.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ipLocal.Name = "ipLocal";
            this.ipLocal.Size = new System.Drawing.Size(64, 17);
            this.ipLocal.TabIndex = 15;
            this.ipLocal.Text = "ipLocal";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Courier New", 10F);
            this.label7.Location = new System.Drawing.Point(1205, 540);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 17);
            this.label7.TabIndex = 16;
            this.label7.Text = "Port:";
            // 
            // portNumb
            // 
            this.portNumb.AutoSize = true;
            this.portNumb.Font = new System.Drawing.Font("Courier New", 10F);
            this.portNumb.Location = new System.Drawing.Point(1342, 540);
            this.portNumb.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.portNumb.Name = "portNumb";
            this.portNumb.Size = new System.Drawing.Size(72, 17);
            this.portNumb.TabIndex = 17;
            this.portNumb.Text = "portNumb";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Courier New", 10F);
            this.label8.Location = new System.Drawing.Point(1129, 506);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(128, 17);
            this.label8.TabIndex = 18;
            this.label8.Text = "Players online:";
            // 
            // onlinePlayers
            // 
            this.onlinePlayers.AutoSize = true;
            this.onlinePlayers.Font = new System.Drawing.Font("Courier New", 10F);
            this.onlinePlayers.Location = new System.Drawing.Point(1342, 506);
            this.onlinePlayers.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.onlinePlayers.Name = "onlinePlayers";
            this.onlinePlayers.Size = new System.Drawing.Size(16, 17);
            this.onlinePlayers.TabIndex = 19;
            this.onlinePlayers.Text = "0";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1432, 883);
            this.Controls.Add(this.onlinePlayers);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.portNumb);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.ipLocal);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.ipLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.offLabel);
            this.Controls.Add(this.onLabel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.stopButton);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.bannedIps);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.onlineSnakes);
            this.Controls.Add(this.commandLine);
            this.Controls.Add(this.inputLine);
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void Form1_FormClosing(object sender, EventArgs e)
        {
            ServerUtil.stopServer(false);


        }

        #endregion

        private System.Windows.Forms.TextBox inputLine;
        private System.Windows.Forms.TextBox commandLine;
        private System.Windows.Forms.TextBox onlineSnakes;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox bannedIps;
        private Label label3;
        private Button stopButton;
        private Button button1;
        private Label label4;
        private Label onLabel;
        private Label offLabel;
        private Label label5;
        private Label ipLabel;
        private Label label6;
        private Label ipLocal;
        private Label label7;
        private Label portNumb;
        private Label label8;
        public Label onlinePlayers;
    }
}

