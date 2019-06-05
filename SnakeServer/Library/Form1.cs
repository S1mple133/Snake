using System;
using System.Net;
using System.Windows.Forms;

namespace Util
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.onLabel.Visible = true;
            this.offLabel.Visible = false;

            //resize 
            this.WindowState = FormWindowState.Maximized;
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;

            Util.log(ServerUtil.startServer());

            updateIP();
        }

        private void updateIP()
        {
            ipLabel.Text = new WebClient().DownloadString("http://icanhazip.com");
            ipLocal.Text = ServerUtil.ip;
            portNumb.Text = Convert.ToString(Util._PORT);
        }

        private void inputLine_TextChanged(object sender, EventArgs e)
        {
            if (inputLine.Text.Substring(Math.Max(0,inputLine.TextLength - 2), Math.Min(inputLine.TextLength, 2)).Equals("\r\n"))
            {
                Util.log(ServerUtil.doCommand(inputLine.Text.Substring(0, inputLine.TextLength - 2).Split(' ')));
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Util.log(ServerUtil.stopServer(false));
            this.onLabel.Visible = false;
            this.offLabel.Visible = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Util.log(ServerUtil.startServer());
            
            this.onLabel.Visible = true;
            this.offLabel.Visible = false;

            updateIP();
        }
    }
}
