using System;

namespace TelegramBotUsingMariaDB
{
    class Program
    {
        static void Main(string[] args)
        {
            TelegramBotClientClass newBot = new TelegramBotClientClass();
            newBot.SendMessage();
        }
    }
}
