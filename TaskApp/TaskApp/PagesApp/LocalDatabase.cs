using System;
using System.Data.SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TaskApp.PagesApp
{
    public class LocalDatabase : IManageData
    {
        private string nameDatabase;
        public LocalDatabase(string name) => nameDatabase = $"{name}.sqlite";

        public DataTask GetData(int id)
        {
            if (File.Exists(nameDatabase))
            {
                SQLiteConnection conn = new SQLiteConnection("URI=file:" + nameDatabase);
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SQLiteCommand command = conn.CreateCommand();
                    command.CommandText = $"SELECT * FROM Tasks";
                    command.ExecuteNonQuery();
                    SQLiteDataReader readData = command.ExecuteReader();

                    while (readData.Read())
                        Console.WriteLine(readData["Title"].ToString());
                    conn.Close();
                    return new DataTask();
                }
                else
                {
                    conn.Close();
                    return new DataTask();
                }
            }
            else
                return new DataTask();
        }

        public bool SetData(string cm = "CREATE DATABASE tasks;")
        {
            string sourceDb = nameDatabase;
            if(!File.Exists(sourceDb)) File.Create(sourceDb);

            SQLiteConnection conn = new SQLiteConnection("URI=file:" + sourceDb);
            conn.Open();

            if (conn.State == System.Data.ConnectionState.Open)
            {
                SQLiteCommand command = conn.CreateCommand();
                command.CommandText = cm;
                command.ExecuteNonQuery();
                conn.Close();
                return true;
            }
            else
            {
                conn.Close();
                return false;
            }
        }
    }
}