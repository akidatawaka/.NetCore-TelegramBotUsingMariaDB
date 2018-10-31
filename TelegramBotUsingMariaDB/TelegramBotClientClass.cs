using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using System.Threading;

namespace TelegramBotUsingMariaDB
{
    class TelegramBotClientClass
    {
        readonly ITelegramBotClient botClient;
        private long IDTelegram;
        private readonly string telegramBotToken = "695278516:AAGNTqhMroKgIIXuFUeI8SydbznzzPBWh8M";
        string message = "0";

        public TelegramBotClientClass()
        {
            botClient = new TelegramBotClient(telegramBotToken);
            var me = botClient.GetMeAsync().Result;
            Console.WriteLine(
                 $"Hello, World! I am user {me.Id} and my name is {me.FirstName}."
               );
        }

        public void SendMessage()
        {
            botClient.OnMessage += Botclient_OnMessage;
            botClient.StartReceiving();

            Thread.Sleep(int.MaxValue);
        }

        private async void Botclient_OnMessage(object sender, MessageEventArgs e)
        {
            try
            {
                string idTelegram = (e.Message.Chat.Id).ToString();
                string query = "SELECT id_telegram"
                    + " FROM tbl_id_telegram"
                    + " WHERE id_telegram = '"
                    + idTelegram + "';";

                DatabaseManagement db = new DatabaseManagement();
                string hasil = db.GetIDTelegram(query);

                if (hasil == "")
                {
                    Console.WriteLine("Telegram Account Not Registered");
                }
                else
                {
                    while (message != "end")
                    {
                        Console.Write("Enter Message : ");
                        message = Console.ReadLine();
                        try
                        {
                            await botClient.SendTextMessageAsync(idTelegram, message);
                        }
                        catch (Exception exp)
                        {
                            Console.WriteLine(exp);
                        }
                    }

                    Environment.Exit(1);
                }
            }
            catch(Exception exp)
            {
                Console.WriteLine(exp);
            }  
        }
    }
}
