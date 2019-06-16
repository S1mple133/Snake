namespace SnakeClient
{
    partial class ReadSettings
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
            this.ContinueButton = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.Button();
            this.readIpAddressTextBox = new System.Windows.Forms.TextBox();
            this.readPortTextBox = new System.Windows.Forms.TextBox();
            this.readTickIntervalTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ContinueButton
            // 
            this.ContinueButton.Location = new System.Drawing.Point(12, 129);
            this.ContinueButton.Name = "ContinueButton";
            this.ContinueButton.Size = new System.Drawing.Size(75, 23);
            this.ContinueButton.TabIndex = 0;
            this.ContinueButton.Text = "Continue";
            this.ContinueButton.UseVisualStyleBackColor = true;
            this.ContinueButton.Click += new System.EventHandler(this.ContinueButton_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.Location = new System.Drawing.Point(93, 129);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(75, 23);
            this.ExitButton.TabIndex = 1;
            this.ExitButton.Text = "Exit";
            this.ExitButton.UseVisualStyleBackColor = true;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // readIpAddressTextBox
            // 
            this.readIpAddressTextBox.Location = new System.Drawing.Point(12, 25);
            this.readIpAddressTextBox.MaxLength = 15;
            this.readIpAddressTextBox.Name = "readIpAddressTextBox";
            this.readIpAddressTextBox.Size = new System.Drawing.Size(156, 20);
            this.readIpAddressTextBox.TabIndex = 2;
            this.readIpAddressTextBox.Text = "127.0.0.1";
            // 
            // readPortTextBox
            // 
            this.readPortTextBox.Location = new System.Drawing.Point(12, 64);
            this.readPortTextBox.MaxLength = 6;
            this.readPortTextBox.Name = "readPortTextBox";
            this.readPortTextBox.Size = new System.Drawing.Size(156, 20);
            this.readPortTextBox.TabIndex = 3;
            this.readPortTextBox.Text = "4396";
            // 
            // readTickIntervalTextBox
            // 
            this.readTickIntervalTextBox.Location = new System.Drawing.Point(12, 103);
            this.readTickIntervalTextBox.MaxLength = 5;
            this.readTickIntervalTextBox.Name = "readTickIntervalTextBox";
            this.readTickIntervalTextBox.Size = new System.Drawing.Size(156, 20);
            this.readTickIntervalTextBox.TabIndex = 4;
            this.readTickIntervalTextBox.Text = "250";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "IP-Address:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Port:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 87);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Tick-Interval:";
            // 
            // ReadSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(182, 170);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.readTickIntervalTextBox);
            this.Controls.Add(this.readPortTextBox);
            this.Controls.Add(this.readIpAddressTextBox);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.ContinueButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ReadSettings";
            this.Text = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ContinueButton;
        private System.Windows.Forms.Button ExitButton;
        private System.Windows.Forms.TextBox readIpAddressTextBox;
        private System.Windows.Forms.TextBox readPortTextBox;
        private System.Windows.Forms.TextBox readTickIntervalTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}