using System;
using System.Windows.Forms;

namespace SnakeClient
{
    public partial class ReadSettings : Form
    {
        public ReadSettings()
        {
            InitializeComponent();
        }



        private void ContinueButton_Click(object sender, EventArgs e)
        {
            string errorMessage = "";

            string ipAddress = readIpAddressTextBox.Text;

            if (!Int32.TryParse(readPortTextBox.Text, out int port))
                errorMessage += "Can't read Port!" + Environment.NewLine;

            if (!Int32.TryParse(readTickIntervalTextBox.Text, out int tickInterval))
                errorMessage += "Can't read Tick Interval!" + Environment.NewLine;

            if (errorMessage == "")
            {
                System.IO.File.WriteAllLines(Util.SETTINGS_FILE_NAME, new string[] {
                    "Tick-Interval: " + tickInterval,
                    "IP Address: " + ipAddress,
                    "Port: " + port});
                this.Close();

            }
            else
                MessageBox.Show(errorMessage);

        }

        private void ExitButton_Click(object sender, EventArgs e) => Environment.Exit(0);
    }
}
