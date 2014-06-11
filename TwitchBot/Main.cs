using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwitchBot
{
    public partial class Main : Form
    {
        public string buf = Bot.Instance.buf;
        string ChannelMessageRecived = Bot.Instance.ChannelMessageRecived;
        public Main(string botOwner)
        {
            InitializeComponent();
            this.steamEngineTheme1.Text = "Logged in as " + botOwner;
        }

        private void steamCloseButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void steamButton1_Click(object sender, EventArgs e)
        {
            string textinbox = textBox1.Text;
            string dropdownbox = comboBox1.Text;
            if (textinbox.Length > 0)
            {
                if (textinbox.Contains("BROADCAST") || textinbox.Contains("Broadcast") || textinbox.Contains("broadcast"))
                {
                    if (Convert.ToInt32(comboBox1.SelectedIndex) != -1)
                    {
                        Bot.Instance.SendChatMessage(textinbox.Remove(0, 10), dropdownbox.Remove(0, 1));
                        Program.MainForm.richTextBox1.AppendText(dropdownbox + ": " + textinbox + "\r\n");
                    }
                    else 
                    { 
                        foreach (var item in Bot.Instance.StreamList) 
                        {
                            Bot.Instance.SendChatMessage("Developer Broadcast: "+textinbox.Remove(0, 10), item.Remove(0, 1));
                        }
                        Program.MainForm.richTextBox1.AppendText("All Streams("+Bot.Instance.StreamList.Count()+"): " + textinbox + "\r\n");
                    }
                }
                else if (textinbox.Contains("JOIN") || textinbox.Contains("Join") || textinbox.Contains("join"))
                {
                    Bot.Instance.StreamList.Add(textinbox.Remove(0, 5));
                    Bot.Instance.Write(textinbox + "\r\n");
                    Program.MainForm.richTextBox1.AppendText(textinbox + "\r\n");
                }
                else if (textinbox.Contains("PART") || textinbox.Contains("Part") || textinbox.Contains("part"))
                {
                    Bot.Instance.StreamList.Remove(textinbox.Remove(0, 5));
                    Bot.Instance.Write(textinbox + "\r\n");
                    Program.MainForm.richTextBox1.AppendText(textinbox + "\r\n");
                }
                else { 
                switch (textinbox)
                {
                    case "streamlist":
                        Console.WriteLine("Active streams:");
                        richTextBox1.AppendText("Active streams: \r\n");
                        foreach (var item in Bot.Instance.StreamList)
                        {
                            Console.WriteLine(item);
                            richTextBox1.AppendText(item + "\r\n");
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
                            richTextBox1.AppendText("Debuf Disabled");
                        }
                        else if (Bot.debug == false)
                        {
                            Bot.debug = true;
                            Console.WriteLine("Debug Enabled.");
                            richTextBox1.AppendText("Debug Enabled.");
                        }
                        break;
                    default:
                        break;

                }
                }
                textBox1.Clear();
                richTextBox1.ScrollToCaret();
            }
        }

        private void steamMinimizeButton1_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
        private void Write(string value, params object[] objectParams0)
        {
            Bot.Instance.output.Write(String.Format(value, objectParams0));
            Bot.Instance.output.Flush();
        }
        private void viewerListBox_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void steamButton2_Click(object sender, EventArgs e)
        {
            this.Close();
            Program.Disconnect();
        }

        private void Main_Load(object sender, EventArgs e)
        {
            if (this.IsHandleCreated)
                this.Invoke(new Action((MethodInvoker)delegate()
                {
                    this.richTextBox1.AppendText("\r\n");
                    this.richTextBox1.SelectionColor = Color.Red;
                    this.richTextBox1.AppendText("Red does NOT mean error." + "\r\n");
                    this.richTextBox1.AppendText("\r\n");
                    this.richTextBox1.SelectionColor = Color.White;
                }));
        }
    }
}
