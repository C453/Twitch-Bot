using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwitchBot
{
    public class User
    {
        public string Name { get; private set; }
        public string AuthKey { get; private set; }
        public string Channel { get; private set; }

        private User() { }

        public User(string name, string authKey, string channel)
        {
            this.Name = name;
            this.AuthKey = authKey;
            this.Channel = channel;
        }

        public void SetChannel(string newChannel)
        {
            if (newChannel.StartsWith("#"))
                Channel = newChannel;
            else
                Channel = "#" + newChannel;
        }

        public static bool GetUserInformation(string name, ref User user)
        {
            MySqlCommand cmd = Database.Instance.CreateQuery();
            cmd.CommandText = "SELECT name, authkey FROM users WHERE name=@name;";
            cmd.Parameters.AddWithValue("@name", name + "bot");

            using (MySqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    user = new User
                    {
                        Name = rdr.GetString("name"),
                        AuthKey = rdr.GetString("authkey"),
                        Channel = "#" + rdr.GetString("name")
                    };
                    return true;
                }
            }
            return false;
        }
    }
}
