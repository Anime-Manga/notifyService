using Cesxhin.AnimeManga.Application.Notify;
using Cesxhin.AnimeManga.Modules.NlogManager;
using Cesxhin.AnimeManga.Domain.DTO;
using MassTransit;
using NLog;
using System;
using System.Threading.Tasks;
using Telegram.Bot;

namespace Cesxhin.AnimeManga.Application.Consumers
{
    public class NotifyRequestTelegramConsumer : IConsumer<NotifyDTO>
    {
        //nlog
        private readonly NLogConsole _logger = new(LogManager.GetCurrentClassLogger());

        //settings for bot telegram
        private readonly string tokenBot = Environment.GetEnvironmentVariable("TOKEN_BOT");
        private readonly string chat_id = Environment.GetEnvironmentVariable("CHAT_ID_REQUEST");

        public Task Consume(ConsumeContext<NotifyDTO> context)
        {
            var botTelegram = new TelegramBotClient(tokenBot);
            var managementNotify = new NotifyTelegramChannel();

            var notify = context.Message;
            _logger.Info($"Recive this message: {context.MessageId}");

            managementNotify.SendNotify(botTelegram, notify, chat_id);

            return Task.CompletedTask;
        }
    }
}
