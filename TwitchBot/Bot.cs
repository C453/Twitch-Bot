using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Drawing;

namespace TwitchBot
{
    public class Bot
    {
        public List<string> StreamList { get; set; }
        public Dictionary<string, int> ViewerList { get; set; }
        //string = username, int = rank in the chat (
        //viewer = 0,
        //mod = 1,
        //streamer = 2)

        public TextReader input;
        public TextWriter output;
        public string buf{get; set;}
        public string ChannelMessageRecived { get; set; }
        public string Command { get; set; }
        
        public static bool debug = false;

        private static Random rnd;
        private static Bot _instance;
        
        public Bot(StreamReader input, StreamWriter output)
        {
            this.input = input as TextReader;
            this.output = output as TextWriter;
            StreamList = new List<string>();
            ViewerList = new Dictionary<string, int>();
            rnd = new Random();
            _instance = this;
            StartListening();
        }

        private void StartListening()
        {
            var warnings = new List<string>();//TEST WARNINGS
            //Process each line received from irc server
            while (true)
            {
                try { buf = input.ReadLine(); }
                catch { Console.WriteLine("Listening stopped! Socket Closed!"); break; } //It will only catch if the socket is closing.
            
                //Display received irc message
                //Console.WriteLine(buf);
                //Program.MainForm.Invoke(new Action(() => Program.MainForm.richTextBox1.AppendText(buf + "\r\n")));
                //^^Disabled for now to not cause to much console spam
            
            
                //Send pong reply to any ping messages
                if (buf.StartsWith("PING "))
                    Write(buf.Replace("PING", "PONG") + "\r\n");
            
                if (buf[0] != ':') continue;
            
            
                //GETS NAME OF COMMETER
                string[] getUsernameSplit = buf.Split(new Char[] { ':', '!' });
                string username = getUsernameSplit[1];
                //Get channel sent from
                ChannelMessageRecived = buf.Substring(buf.IndexOf('#') + 1).Split(new Char[] { ':', '!', ' ' })[0];
                //GET CONTENT OF COMMENT
                string chatTrim1 = buf.Substring(buf.IndexOf(':') + 1);
                string chatTrim2 = chatTrim1.Substring(chatTrim1.IndexOf(':') + 1);
                //Sends comments to console
                if (buf.Contains(":jtv MODE #" + ChannelMessageRecived))
                {
                    string modTrim1 = username.Substring(username.IndexOf('+') + 3).Split(new Char[] { '+'})[0];
                    string moderatorJoinTrim = ChannelMessageRecived.Split(new char[] { '+' })[0];
                    if (modTrim1 != moderatorJoinTrim) 
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;//Means someone joined as a moderator
                        Console.WriteLine(modTrim1 + " is now a moderator in {0}!", moderatorJoinTrim);
                        textInvoker(modTrim1 + " is now a moderator in #" + moderatorJoinTrim + "!",Color.Blue);
                    }
                }
                else if (username.Contains("jtv") || username.Contains("tmi.twitch.tv"))
                {
                    Console.ForegroundColor = ConsoleColor.Red;//System messages (IRC ETC)
                    Console.WriteLine(buf);
                    textInvoker(buf,Color.Red);
                }
                else if (buf.Contains("tmi.twitch.tv JOIN #" + ChannelMessageRecived))
                {
                    if ("#" + username != Program.Channel || Program.Channel != ChannelMessageRecived)
                    {
                        if ("#" + username == ChannelMessageRecived)
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;//Means someone joined
                            Console.WriteLine("The broadcaster(" + username + ") has joined #{0}!", ChannelMessageRecived);
                            textInvoker("The broadcaster(" + username + ") has joined #" + ChannelMessageRecived + "!",Color.Magenta);
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Cyan;//Means someone joined
                            Console.WriteLine(username + " has joined #{0}!", ChannelMessageRecived);
                            textInvoker(username + " has joined #"+ChannelMessageRecived+"!",Color.Cyan);
                        }
                        if(!ViewerList.Keys.Contains(username))
                            ViewerList.Add(username, 0); //TODO: add real rank here
                    }
                }
                else if (buf.Contains("tmi.twitch.tv PART #" + ChannelMessageRecived))
                {
                    if ("#" + username == ChannelMessageRecived)
                    {
                        ViewerList.Remove(username);
                        Console.ForegroundColor = ConsoleColor.Magenta;//Means someone left
                        Console.WriteLine("The broadcaster(" + username + ") has left #{0}!", ChannelMessageRecived);
                        textInvoker("The broadcaster(" + username + ") has left #" + ChannelMessageRecived + "!",Color.Magenta);
                    }
                    else
                    {
                        ViewerList.Remove(username);
                        Console.ForegroundColor = ConsoleColor.Red;//Means someone left
                        Console.WriteLine(username + " has left #{0}!", ChannelMessageRecived);
                        textInvoker(username + " has left #" + ChannelMessageRecived + "!",Color.Red);
                    }
                }
                else
                {
            
                    Console.ForegroundColor = ConsoleColor.Green;//Chat messages
                    Console.WriteLine("#" + ChannelMessageRecived + " " + username + " : " + chatTrim2);
                    textInvoker("#" + ChannelMessageRecived + " " + username + " : " + chatTrim2,Color.Green);
                }

                Console.ResetColor();
                //Test timeout command for ALL CAPS (add limit later?) Need to work on this
                /*if (AllCapitals(chatTrim2))
                {
                    output.Write("PRIVMSG #donran :User {0} has been typing all caps\r\n ", getUsernameSplit[1]);
                    output.Flush();
                }
                */
            
                //Warning system?
                //Need to work on this too
                //COMMANDS
                switch (chatTrim2)
                {            
                    case "!giveaway":
                        if ("#" + username == ChannelMessageRecived)
                        {
                            int r = rnd.Next(ViewerList.Keys.Count);
                            SendChatMessage("Viewer " + ViewerList.Keys.ToList()[r] + " has won the giveaway!",ChannelMessageRecived);
                            Console.WriteLine("Viewer " + ViewerList.Keys.ToList()[r] + " has won the giveaway!");
                            textInvoker("Viewer " + ViewerList.Keys.ToList()[r] + " has won the giveaway!",Color.Yellow);
                        }
                        break;
                    case "!warn":
                        break;
                    case "!debug":
                        if (debug)
                            Command = "Hello World!";
                        break;
                    case "!joinmy":
                        if (!StreamList.Contains(username)) 
                        {
                            StreamList.Add(username);
                        }
                        Write("JOIN #" + username + "\r\n");
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Joined " + username + "'s channel.");
                        textInvoker("Joined " + username + "'s channel.",Color.Yellow);
                        Console.ResetColor();
                        break;
                    case "!leavemy":
                        if (username == ChannelMessageRecived)
                        {
                            Write("PART #" + username + "\r\n");
                            StreamList.Remove(username);
                            Console.WriteLine("Left " + username + "'s channel.");
                            textInvoker("Left " + username + "'s channel.",Color.Yellow);
                            Console.ResetColor();
                        }
                        break;
                    default:
                        SendChatMessage(CommandManager.GetReturnText(chatTrim2), ChannelMessageRecived);
                        break;
                }
                if (Command != null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(Command + "     |Request by " + username);
                    textInvoker(Command + "     |Request by " + username,Color.Yellow);
                    Console.ResetColor();
                    Write("PRIVMSG #{0} : {1}\r\n", ChannelMessageRecived, Command);
                    Command = null;
                }
            
            
                /* IRC commands come in one of these formats:
                 * :NICK!USER@HOST COMMAND ARGS ... :DATA\r\n
                 * :SERVER COMAND ARGS ... :DATA\r\n
                 */
                //After server sends 001 command, we can set mode to bot and join a channel
                if (buf.Split(' ')[1] == "001")
                {
                    Write("JOIN {0}\r\n", Program.Channel);
                }
                Program.MainForm.Invoke(new Action((MethodInvoker)delegate()
                {
                    Program.MainForm.richTextBox1.ScrollToCaret();
                }));
                if (Program.MainForm.IsHandleCreated)
                {
                    Program.MainForm.Invoke(new Action((MethodInvoker)delegate()
                    {
                        Program.MainForm.viewerListBox.Items.Clear();
                        Program.MainForm.viewerListBox.Items.AddRange(ViewerList.Keys.ToArray());
                        Program.MainForm.viewerListBox.Update();
                    }));
                    Program.MainForm.Invoke(new Action((MethodInvoker)delegate()
                    {
                        Program.MainForm.comboBox1.Items.Clear();
                        Program.MainForm.comboBox1.Items.AddRange(StreamList.ToArray());
                        Program.MainForm.comboBox1.Update();
                    }));
                }
            }
        }

        public void Write(string value, params object[] objectParams0)
        {
            output.Write(String.Format(value, objectParams0));
            output.Flush();
        }
        public void textInvoker(string value, Color color) 
        {
            if (Program.MainForm.IsHandleCreated)
                Program.MainForm.Invoke(new Action((MethodInvoker)delegate() 
                    {

                        Program.MainForm.richTextBox1.SelectionColor = color;
                        Program.MainForm.richTextBox1.AppendText(value + "\r\n");
                        Program.MainForm.richTextBox1.SelectionColor = Color.White;
                    }));
        }
        public void SendChatMessage(string value, string channel, params object[] objectParams0) 
        {
            string baseText = String.Format("PRIVMSG #{0} :", channel);

            if (value == null) return;

            output.Write(baseText + String.Format(value, objectParams0) + "\r\n");
            output.Flush();
        }

        private bool IsUpperText(string value)
        {
            //TODO: Fix it
            int half = value.Length / 2;
            int dest = 0;

            for (int i = 0; i < value.Length; i++)
            {
                if (Char.IsLetter(value[i]))
                {
                    if (!Char.IsUpper(value[i]))
                        return false;

                    if (dest > half)
                        break;

                    dest++;
                }
            }
            return true;
        }
        
        public static Bot Instance
        {
            get
            {
                if(_instance == null)
                    throw new NullReferenceException("Make an instance first.");

                return _instance;
            }
        }
    }
}
