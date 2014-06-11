using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TwitchBot
{
    public class CommandManager
    {
        public static string GetReturnText(string command)
        {
            string[] getUsernameSplit = Bot.Instance.buf.Split(new Char[] { ':', '!' });
            string username = getUsernameSplit[1];
            MySqlCommand cmd = Database.Instance.CreateQuery();
            cmd.CommandText = "SELECT returnText FROM commands WHERE command=@command;";
            cmd.Parameters.AddWithValue("@command", command);

            using (MySqlDataReader rdr = cmd.ExecuteReader()) {
                while (rdr.Read()) { 
                    Console.WriteLine(rdr.GetString("returnText"));
                    Bot.Instance.textInvoker(rdr.GetString("returnText") + "     |Request by " + username,Color.Yellow);
                    return rdr.GetString("returnText");
                }
            }
            return null;
        }
    }
}
