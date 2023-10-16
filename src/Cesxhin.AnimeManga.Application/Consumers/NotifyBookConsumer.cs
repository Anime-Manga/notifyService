using Cesxhin.AnimeManga.Application.Notify;
using Cesxhin.AnimeManga.Modules.NlogManager;
using Cesxhin.AnimeManga.Domain.DTO;
using Discord.Webhook;
using MassTransit;
using NLog;
using System;
using System.Threading.Tasks;

namespace Cesxhin.AnimeManga.Application.Consumers
{
    public class NotifyBookConsumer : IConsumer<NotifyMangaDTO>
    {
        //nlog
        private readonly NLogConsole _logger = new(LogManager.GetCurrentClassLogger());

        //webhook discord
        private readonly string _webhookDiscord = Environment.GetEnvironmentVariable("WEBHOOK_DISCORD_BOOK");

        public Task Consume(ConsumeContext<NotifyMangaDTO> context)
        {
            DiscordWebhookClient discord = new(_webhookDiscord);
            var managementNotify = new NotifyDiscord();

            var notify = context.Message;
            _logger.Info($"Recive this message: {notify.Message}");

            managementNotify.SendNotify(discord, GenericNotify.NotifyMangaDTOToGenericNotify(notify), null);

            return Task.CompletedTask;
        }
    }
}
