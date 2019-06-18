namespace SnakeClient
{
    class Util
    {
        public static int MAX_PLAYERS;
        public static int GRID_SIZE;
        public const int ZOOM = 25;
        public static int ONLINE_PLAYERS;
        public static int TICK_INTERVAL = 250;
        public static string IP_ADDRESS;
        public static int PORT = 4396;
        public const string SETTINGS_FILE_NAME = "config.conf";

        public static void LoadConfig(string path)
        {
            if (!System.IO.File.Exists(path))
                System.Windows.Forms.Application.Run(new ReadSettings());

            if (!System.IO.File.Exists(path))
                System.Environment.Exit(0);

            string[] lines = System.IO.File.ReadAllLines(path, System.Text.Encoding.UTF8);

            TICK_INTERVAL = System.Convert.ToInt32(RemoveSpaces(lines[0].Substring(lines[0].IndexOf(':') + 1)));
            IP_ADDRESS = RemoveSpaces(lines[1].Substring(lines[1].IndexOf(':') + 1));
            PORT = System.Convert.ToInt32(RemoveSpaces(lines[2].Substring(lines[2].IndexOf(':') + 1)));
        }

        private static string RemoveSpaces(string input)
        {
            int indexOfSpace = input.IndexOf(' ');
            if (indexOfSpace != -1)
                for (int i = 0; i < input.Length; i++)
                    if (input[i] == ' ')
                        input = input.Substring(0, input.IndexOf(' ')) + input.Substring(input.IndexOf(' ') + 1);
            return input;
        }
    }
}
