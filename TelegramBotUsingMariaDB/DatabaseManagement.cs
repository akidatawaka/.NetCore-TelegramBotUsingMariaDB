using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace TelegramBotUsingMariaDB
{
    class DatabaseManagement
    {
        private string server;
        private string database;
        private string userID;
        private string password;
        private MySqlConnection connection;


        public DatabaseManagement()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = "localhost";
            database = "belajar_mariadb";
            userID = "root";
            password = "Password1234";

            string connectionString;

            connectionString = "SERVER=" + server + ";"
                + "DATABASE=" + database + ";"
                + "UID=" + userID + ";"
                + "PASSWORD=" + password + ";";

            connection = new MySqlConnection(connectionString);
        }

        private bool openConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch(Exception e)
            {
                Console.WriteLine("Koneksi Gagal Tersambung");
                return false;
            }
        }

        private bool closeConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("Koneksi gagal ditutup");
                return false;
            }
        }

        public string GetIDTelegram(string query)
        {
            string hasil = "";
            

            if(this.openConnection() == true)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = command.ExecuteReader();

                while (dataReader.Read())
                {
                    hasil = dataReader["id_telegram"].ToString();
                }
            }

            return hasil;
        }


    }
}
