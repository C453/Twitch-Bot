using System;
using System.IO;
using System.Net.Sockets;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace TwitchBot
{
    class Program
    {
        private static readonly int port = 6667;
        private static readonly string creator = "This bot was made by Donran, ossimc82 and C453!";
        private static readonly string _ASCII_fun1 = (@" _____          _ _       _     ____        _   ");
        private static readonly string _ASCII_fun2 = (@"|_   _|_      _(_) |_ ___| |__ | __ )  ___ | |_ ");
        private static readonly string _ASCII_fun3 = (@"  | | \ \ /\ / / | __/ __| '_ \|  _ \ / _ \| __|");
        private static readonly string _ASCII_fun4 = (@"  | |  \ V  V /| | || (__| | | | |_) | (_) | |_ ");
        private static readonly string _ASCII_fun5 = (@"  |_|   \_/\_/ |_|\__\___|_| |_|____/ \___/ \__|");
        private static readonly string server = "irc.twitch.tv";

        private static string[] args;
        private static bool userSet;

        private static TcpClient chatSkt;
        private static Thread consoleThread;
        private static Thread listenThread;
        private static Thread formThread;
        private static User currentUser;
        private static StreamReader input;
        private static StreamWriter output;
        public static Main MainForm { get; private set; }
        public static string Channel { get { return currentUser.Channel; } }
        public static bool IsRestarting { get; set; }

        public static string askchannel;
        public static string botowner;

        public static void Main(string[] args)
        {
            userSet = false;
            Program.args = args;
            chatSkt = new System.Net.Sockets.TcpClient();
            if(!Application.AllowQuit)
                Application.SetCompatibleTextRenderingDefault(true);
            Application.EnableVisualStyles();
            new Database("Server=213.66.248.78;Database=twitch_bot;uid=TwitchBotProject;password=ui25j1!7j;Pooling=true;Connection Timeout=30;MinimumPoolSize=10;maximumpoolsize=1200;");

            centerText(_ASCII_fun1);
            centerText(_ASCII_fun2);
            centerText(_ASCII_fun3);
            centerText(_ASCII_fun4);
            centerText(_ASCII_fun5);
            centerText("\n \n");
            centerText(creator);
            centerText("\n \n");
            centerText("-------------------------------------------------------------------------------");

            Console.Write("who are you? (c453, donran, ossimc82)\n");
            while (!userSet)
            {
                if (!ConsoleManager.IsRunning)
                    UserInit(null);
            }
            IsRestarting = false;
            askchannel = currentUser.Channel;

            chatSkt.Connect(server, port);
            if (!chatSkt.Connected)
            {
                Writer.WriteSystemln("Failed to connect!");
                Thread.Sleep(2000);
                return;
            }
            input = new System.IO.StreamReader(chatSkt.GetStream());
            output = new System.IO.StreamWriter(chatSkt.GetStream());
            consoleThread = new Thread(() => new ConsoleManager(output));
            listenThread = new Thread(() => new Bot(input, output));
            formThread = new Thread(() => Application.Run(MainForm = new Main(currentUser.Name.Replace("bot", String.Empty))));
            
            consoleThread.Start();
            listenThread.Start();
            formThread.Start();

            //Starting USER and NICK login commands 
            output.Write(
                "PASS " + currentUser.AuthKey + "\r\n" +
                "USER " + currentUser.Name + "\r\n" +
                "NICK " + currentUser.Name + "\r\n"
            );
            output.Flush();
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Red does NOT = errors.");
            Console.ResetColor();
            Console.WriteLine("");

            //Thread.Sleep(2000); //this is required otherwise the instance will be null
            //ConsoleManager.Instance.SendChatMessage("test {0}", "param1");

            while (chatSkt.Connected) { }

            Writer.WriteSystemln("Socket Disconnected! Closing...");
            Thread.Sleep(3000);
            //Environment.Exit(0);
        }

        static bool AllCapitals(string inputString)
        {
            return Regex.IsMatch(inputString, @"^[A-Z\s]+$");
        }

        private static void centerText(String text)
        {
            Console.Write(new string(' ', (Console.WindowWidth - text.Length) / 2));
            Console.WriteLine(text);
        }

        public static void UserInit(string name)
        {
            while (true)
            {
                if(String.IsNullOrEmpty(name))
                    name = Console.ReadLine().ToLower();
                if (User.GetUserInformation(name, ref currentUser))
                {
                    userSet = true;
                    break;
                }
                Console.Write("\nPlease enter a valid name. \n \n");
                name = null;
            }
        }

        public static void Disconnect()
        {
            IsRestarting = true;
            consoleThread = null;
            listenThread = null;
            formThread = null;

            if (MainForm.IsHandleCreated)
                MainForm.Invoke(new Action((MethodInvoker)delegate()
                {
                    MainForm.Close();
                }));

            chatSkt.Close();
            Database.Instance.Dispose();
            Thread.Sleep(2000);
            Main(args);
        }

        public static void Close()
        {
            IsRestarting = true;
            consoleThread = null;
            listenThread = null;
            formThread = null;

            if (MainForm.IsHandleCreated)
                MainForm.Invoke(new Action((MethodInvoker)delegate()
                {
                    MainForm.Close();
                }));

            chatSkt.Close();
            Database.Instance.Dispose();
            Console.WriteLine("I'm out, good bye!");
            Thread.Sleep(2000);
            Environment.Exit(0);
        }

        public void textInvoker(string value)
        {
            if(MainForm.IsHandleCreated)
                MainForm.Invoke(new Action((MethodInvoker)delegate()
                {
                    MainForm.richTextBox1.AppendText(value + "\r\n");
                }));
        }

        public static void ReshowForm()
        {
            formThread = new Thread(() => Application.Run(MainForm = new Main(currentUser.Name.Replace("bot", String.Empty))));
            formThread.Start();
        }
    }
}
