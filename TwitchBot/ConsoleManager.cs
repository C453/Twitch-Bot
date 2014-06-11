using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace TwitchBot
{
    public class ConsoleManager
    {
        private StreamWriter output;
        private static ConsoleManager _instance;

        public static bool IsRunning { get; set; }

        public ConsoleManager(StreamWriter output)
        {
            IsRunning = true;
            this.output = output;
            _instance = this;
            ListenToConsole();
        }

        //This will only be called once
        private void ListenToConsole()
        {
            while (true)
            {
                string input = Console.ReadLine();

                if (Program.IsRestarting)
                {
                    Program.UserInit(input);
                    return;
                }

                if (input.Contains("broadcast") || input.Contains("Broadcast"))
                {
                    foreach (var item in Bot.Instance.StreamList)
                    {
                        SendChatMessage(input.Remove(0, 10), item);
                        textInvoker(item);
                    }
                }
                if (input.Contains("JOIN") || input.Contains("Join") || input.Contains("join"))
                {
                    Bot.Instance.StreamList.Add(input.Remove(0, 5));
                    textInvoker(input);
                }
                if (input.Contains("PART") || input.Contains("Part") || input.Contains("part"))
                {
                    Bot.Instance.StreamList.Remove(input.Remove(0, 5));
                    textInvoker(input);
                }
                if (input.Contains("say"))
                {
                    SendChatMessage(input.Remove(0, 4), Program.Channel);
                    textInvoker(input);
                }
                switch (input)
                {
                    case "streamlist":
                        Console.WriteLine("Active streams:");
                        textInvoker("Active Streams:");
                        foreach (var item in Bot.Instance.StreamList)
                        {
                            Console.WriteLine(item);
                            textInvoker(item);
                        }
                        break;
                    case "/dc":
                        Program.Disconnect();
                        break;
                    case "/visuals":
                        Program.ReshowForm();
                        break;
                    case "/exit":
                        Program.Close();
                        break;
                    case "/debug":
                        if (Bot.debug)
                        {
                            Bot.debug = false;
                            Console.WriteLine("Debug Disabled.");
                            textInvoker("Debug Disabled.");
                        }
                        else if (Bot.debug == false)
                        {
                            Bot.debug = true;
                            Console.WriteLine("Debug Enabled.");
                            textInvoker("Debug Enabled.");
                        }
                        break;
                    default:
                        Write(input + "\r\n");
                        break;

                }
            }
        }

        public void SendChatMessage(string value, string channel, params object[] objectParams0)
        {
            string baseText = String.Format("PRIVMSG {0} :", channel);
            output.Write(baseText + String.Format(value, objectParams0) + "\r\n");
            output.Flush();
        }

        private void Write(string value, params object[] objectParams0)
        {
            output.Write(String.Format(value, objectParams0));
            output.Flush();
        }

        public void textInvoker(string value)
        {
            Program.MainForm.Invoke(new Action(() => Program.MainForm.richTextBox1.AppendText(value + "\r\n")));
        }

        public static ConsoleManager Instance
        {
            get
            {
                if (_instance == null)
                    throw new NullReferenceException();

                return _instance;
            }
        }
    }
}
