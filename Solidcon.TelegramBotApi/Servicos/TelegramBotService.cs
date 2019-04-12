using System.Collections.Generic;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace Solidcon.TelegramBotApi.Servicos
{

    public static class TelegramBotService
    {
        public static ITelegramBotClient Botclient { get; private set; }

        private const string BotToken = "";

        private static readonly ICollection<long> ChatIdLista = new HashSet<long>();

        public static void Startup()
        {
            Botclient = new TelegramBotClient(BotToken);

            Botclient.OnMessage += Bot_OnMessage;


            Botclient.StartReceiving();

        }
        private static async void Bot_OnMessage(object sender, MessageEventArgs e)
        {
            
            
            if (!string.IsNullOrEmpty(e.Message.Text))
            {
                var text = e.Message.Text.ToLower();

                var chatId = e.Message.Chat.Id;

                if (!ChatIdLista.Contains(chatId))
                    ChatIdLista.Add(chatId);

                if (text.Contains("/start") || text.Contains("chat_id") || text.Contains("chatid"))
                {
                    await Botclient.SendTextMessageAsync(chatId, $"Solidcon Informática - chatid: {chatId}");
                }

            }
        }


    }
}
