using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using MySql.Data.MySqlClient;
using MySql.Data.Types;

namespace TwitchBot
{
    public class Database
    {
        private static Database _db;
        private MySqlConnection con;

        public Database(string connectionString)
        {
            Console.WriteLine("Trying to connect to {0}!", connectionString.Substring((connectionString.IndexOf("Server=") + 7), (connectionString.IndexOf(';') - (connectionString.IndexOf("Server=") + 7))));
            con = new MySqlConnection(connectionString);
            con.Open();
            _db = this;
            Console.Clear();
        }

        public MySqlCommand CreateQuery()
        {
            return con.CreateCommand();
        }

        public void Dispose()
        {
            con.Close();
        }

        /// <summary>
        /// You should only use this to read single values.
        /// </summary>
        public bool GetBoolean(string commandText, string column)
        {
            object value = null;
            MySqlCommand cmd = CreateQuery();
            cmd.CommandText = commandText;

            using (MySqlDataReader rdr = cmd.ExecuteReader())
                while (rdr.Read())
                    value = rdr.GetBoolean(column);

            return (bool)value;
        }

        /// <summary>
        /// You should only use this to read single values.
        /// </summary>
        public byte GetByte(string commandText, string column)
        {
            object value = null;
            MySqlCommand cmd = CreateQuery();
            cmd.CommandText = commandText;

            using (MySqlDataReader rdr = cmd.ExecuteReader())
                while (rdr.Read())
                    value = rdr.GetByte(column);

            return (byte)value;
        }

        /// <summary>
        /// You should only use this to read single values.
        /// </summary>
        public long GetBytes(string commandText, int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            object value = null;
            MySqlCommand cmd = CreateQuery();
            cmd.CommandText = commandText;

            using (MySqlDataReader rdr = cmd.ExecuteReader())
                while (rdr.Read())
                    value = rdr.GetBytes(i, fieldOffset, buffer, bufferoffset, length);

            return (long)value;
        }

        /// <summary>
        /// You should only use this to read single values.
        /// </summary>
        public char GetChar(string commandText, string name)
        {
            object value = null;
            MySqlCommand cmd = CreateQuery();
            cmd.CommandText = commandText;

            using (MySqlDataReader rdr = cmd.ExecuteReader())
                while (rdr.Read())
                    value = rdr.GetChar(name);

            return (char)value;
        }

        /// <summary>
        /// You should only use this to read single values.
        /// </summary>
        public long GetChars(string commandText, int i, long fieldOffset, char[] buffer, int bufferoffset, int length)
        {
            object value = null;
            MySqlCommand cmd = CreateQuery();
            cmd.CommandText = commandText;

            using (MySqlDataReader rdr = cmd.ExecuteReader())
                while (rdr.Read())
                    value = rdr.GetChars(i, fieldOffset, buffer, bufferoffset, length);

            return (long)value;
        }

        /// <summary>
        /// You should only use this to read single values.
        /// </summary>
        public string GetDataTypeName(string commandText, int i)
        {
            object value = null;
            MySqlCommand cmd = CreateQuery();
            cmd.CommandText = commandText;

            using (MySqlDataReader rdr = cmd.ExecuteReader())
                while (rdr.Read())
                    value = rdr.GetDataTypeName(i);

            return (string)value;
        }

        /// <summary>
        /// You should only use this to read single values.
        /// </summary>
        public DateTime GetDateTime(string commandText, string column)
        {
            object value = null;
            MySqlCommand cmd = CreateQuery();
            cmd.CommandText = commandText;

            using (MySqlDataReader rdr = cmd.ExecuteReader())
                while (rdr.Read())
                    value = rdr.GetDateTime(column);

            return (DateTime)value;
        }

        /// <summary>
        /// You should only use this to read single values.
        /// </summary>
        public decimal GetDecimal(string commandText, string column)
        {
            object value = null;
            MySqlCommand cmd = CreateQuery();
            cmd.CommandText = commandText;

            using (MySqlDataReader rdr = cmd.ExecuteReader())
                while (rdr.Read())
                    value = rdr.GetDecimal(column);

            return (decimal)value;
        }

        /// <summary>
        /// You should only use this to read single values.
        /// </summary>
        public double GetDouble(string commandText, string column)
        {
            object value = null;
            MySqlCommand cmd = CreateQuery();
            cmd.CommandText = commandText;

            using (MySqlDataReader rdr = cmd.ExecuteReader())
                while (rdr.Read())
                    value = rdr.GetDouble(column);

            return (double)value;
        }

        /// <summary>
        /// You should only use this to read single values.
        /// </summary>
        public IEnumerator GetEnumerator(string commandText)
        {
            object value = null;
            MySqlCommand cmd = CreateQuery();
            cmd.CommandText = commandText;

            using (MySqlDataReader rdr = cmd.ExecuteReader())
                while (rdr.Read())
                    value = rdr.GetEnumerator();

            return (IEnumerator)value;
        }

        /// <summary>
        /// You should only use this to read single values.
        /// </summary>
        public Type GetFieldType(string commandText, string column) 
        {
            object value = null;
            MySqlCommand cmd = CreateQuery();
            cmd.CommandText = commandText;

            using (MySqlDataReader rdr = cmd.ExecuteReader())
                while (rdr.Read())
                    value = rdr.GetFieldType(column);

            return (Type)value;
        }

        /// <summary>
        /// You should only use this to read single values.
        /// </summary>
        public float GetFloat(string commandText, string column)
        {
            object value = null;
            MySqlCommand cmd = CreateQuery();
            cmd.CommandText = commandText;

            using (MySqlDataReader rdr = cmd.ExecuteReader())
                while (rdr.Read())
                    value = rdr.GetFloat(column);

            return (float)value;
        }

        /// <summary>
        /// You should only use this to read single values.
        /// </summary>
        public Guid GetGuid(string commandText, string column)
        {
            object value = null;
            MySqlCommand cmd = CreateQuery();
            cmd.CommandText = commandText;

            using (MySqlDataReader rdr = cmd.ExecuteReader())
                while (rdr.Read())
                    value = rdr.GetGuid(column);

            return (Guid)value;
        }

        /// <summary>
        /// You should only use this to read single values.
        /// </summary>
        public short GetInt16Value(string commandText, string column)
        {
            object value = null;
            MySqlCommand cmd = CreateQuery();
            cmd.CommandText = commandText;

            using (MySqlDataReader rdr = cmd.ExecuteReader())
                while (rdr.Read())
                    value = rdr.GetInt16(column);

            return (short)value;
        }

        /// <summary>
        /// You should only use this to read single values.
        /// </summary>
        public int GetInt32Value(string commandText, string column)
        {
            object value = null;
            MySqlCommand cmd = CreateQuery();
            cmd.CommandText = commandText;
            
            using (MySqlDataReader rdr = cmd.ExecuteReader())
                while (rdr.Read())
                    value = rdr.GetInt32(column);

            return (int)value;
        }

        /// <summary>
        /// You should only use this to read single values.
        /// </summary>
        public long GetInt64Value(string commandText, string column)
        {
            object value = null;
            MySqlCommand cmd = CreateQuery();
            cmd.CommandText = commandText;

            using (MySqlDataReader rdr = cmd.ExecuteReader())
                while (rdr.Read())
                    value = rdr.GetInt64(column);

            return (long)value;
        }

        /// <summary>
        /// You should only use this to read single values.
        /// </summary>
        public MySqlDateTime GetMySqlDateTime(string commandText, string column)
        {
            object value = null;
            MySqlCommand cmd = CreateQuery();
            cmd.CommandText = commandText;

            using (MySqlDataReader rdr = cmd.ExecuteReader())
                while (rdr.Read())
                    value = rdr.GetMySqlDateTime(column);

            return (MySqlDateTime)value;
        }

        /// <summary>
        /// You should only use this to read single values.
        /// </summary>
        public MySqlDecimal GetMySqlDecimal(string commandText, string column)
        {
            object value = null;
            MySqlCommand cmd = CreateQuery();
            cmd.CommandText = commandText;

            using (MySqlDataReader rdr = cmd.ExecuteReader())
                while (rdr.Read())
                    value = rdr.GetMySqlDecimal(column);

            return (MySqlDecimal)value;
        }

        /// <summary>
        /// You should only use this to read single values.
        /// </summary>
        public int GetOrdinal(string commandText, string name)
        {
            object value = null;
            MySqlCommand cmd = CreateQuery();
            cmd.CommandText = commandText;

            using (MySqlDataReader rdr = cmd.ExecuteReader())
                while (rdr.Read())
                    value = rdr.GetOrdinal(name);

            return (int)value;
        }

        /// <summary>
        /// You should only use this to read single values.
        /// </summary>
        public sbyte GetSByte(string commandText, string name)
        {
            object value = null;
            MySqlCommand cmd = CreateQuery();
            cmd.CommandText = commandText;

            using (MySqlDataReader rdr = cmd.ExecuteReader())
                while (rdr.Read())
                    value = rdr.GetSByte(name);

            return (sbyte)value;
        }

        /// <summary>
        /// You should only use this to read single values.
        /// </summary>
        public string GetString(string commandText, string column)
        {
            object value = null;
            MySqlCommand cmd = CreateQuery();
            cmd.CommandText = commandText;

            using (MySqlDataReader rdr = cmd.ExecuteReader())
                while (rdr.Read())
                    value = rdr.GetString(column);

            return (string)value;
        }

        /// <summary>
        /// You should only use this to read single values.
        /// </summary>
        public TimeSpan GetTimeSpan(string commandText, string column)
        {
            object value = null;
            MySqlCommand cmd = CreateQuery();
            cmd.CommandText = commandText;

            using (MySqlDataReader rdr = cmd.ExecuteReader())
                while (rdr.Read())
                    value = rdr.GetTimeSpan(column);

            return (TimeSpan)value;
        }

        /// <summary>
        /// You should only use this to read single values.
        /// </summary>
        public ushort GetUInt16(string commandText, string column)
        {
            object value = null;
            MySqlCommand cmd = CreateQuery();
            cmd.CommandText = commandText;

            using (MySqlDataReader rdr = cmd.ExecuteReader())
                while (rdr.Read())
                    value = rdr.GetUInt16(column);

            return (ushort)value;
        }

        /// <summary>
        /// You should only use this to read single values.
        /// </summary>
        public uint GetUInt32(string commandText, string column)
        {
            object value = null;
            MySqlCommand cmd = CreateQuery();
            cmd.CommandText = commandText;

            using (MySqlDataReader rdr = cmd.ExecuteReader())
                while (rdr.Read())
                    value = rdr.GetUInt32(column);

            return (uint)value;
        }

        /// <summary>
        /// You should only use this to read single values.
        /// </summary>
        public ulong GetUInt64(string commandText, string column)
        {
            object value = null;
            MySqlCommand cmd = CreateQuery();
            cmd.CommandText = commandText;

            using (MySqlDataReader rdr = cmd.ExecuteReader())
                while (rdr.Read())
                    value = rdr.GetUInt64(column);

            return (ulong)value;
        }

        public static Database Instance
        {
            get
            {
                if (_db == null)
                    throw new NullReferenceException("Make a constructor first.");

                return _db;
            }
        }




        //Donrans tutorial method
        //public void load(ref string commandtext, ref int param2, ref bool param3, ref DateTime param4) //ref is not needed, i only used it to return some values
        //{
        //    //in here is an example for don how to load stuff

        //    MySqlCommand cmd = CreateQuery(); //start creating a query

        //    cmd.CommandText = "SELECT command FROM commands;"; //query text

        //    using (MySqlDataReader rdr = cmd.ExecuteReader()) //initialize the reader
        //    {
        //        while (rdr.Read()) //while reading
        //        {
        //            commandtext = rdr.GetString("collumCommandText");
        //            param2 = rdr.GetInt32("collumParam2Here");
        //            param3 = rdr.GetBoolean("collumParam3Here");
        //            param4 = rdr.GetDateTime("collumParam4Here");
        //        }
        //    }
        //}
    }
}
