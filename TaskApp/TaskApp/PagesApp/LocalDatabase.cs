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
                    command.CommandText = $"SELECT * FROM Tasks WHERE id={id}";
                    command.ExecuteNonQuery();
                    SQLiteDataReader readData = command.ExecuteReader();

                    if (readData.Read())
                    {
                        bool done = (bool)readData["done"] ? true : false;
                        return new DataTask(0, readData["title"].ToString(),
                            readData["description"].ToString(), readData["creation_data"].ToString(), done);
                    }
                    else
                    {
                        conn.Close();
                        return new DataTask();
                    }
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
            if(!File.Exists(nameDatabase)) File.Create(nameDatabase);

            SQLiteConnection conn = new SQLiteConnection("URI=file:" + nameDatabase);
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

        public int GetNumberRows(string nameTable = "Tasks")
        {
            if (File.Exists(nameDatabase))
            {
                SQLiteConnection conn = new SQLiteConnection("URI=file:" + nameDatabase);
                conn.Open();

                if (conn.State == System.Data.ConnectionState.Open)
                {
                    SQLiteCommand command = conn.CreateCommand();
                    command.CommandText = $"SELECT COUNT(title) FROM Tasks";
                    command.ExecuteNonQuery();
                    SQLiteDataReader readData = command.ExecuteReader();

                    if (readData.Read())
                        return readData.GetInt32(0);
                    else
                        return 0;
                }
                else
                {
                    conn.Close();
                    return 0;
                }
            }
            else
                return 0;
        }
    }
}