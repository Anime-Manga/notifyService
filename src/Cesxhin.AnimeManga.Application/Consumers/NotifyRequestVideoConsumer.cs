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
    public class NotifyRequestVideoConsumer : IConsumer<NotifyDTO>
    {
        //nlog
        private readonly NLogConsole _logger = new(LogManager.GetCurrentClassLogger());

        //webhook discord
        private readonly string _webhookDiscord = Environment.GetEnvironmentVariable("WEBHOOK_DISCORD_VIDEO_REQUEST");

        public Task Consume(ConsumeContext<NotifyDTO> context)
        {
            DiscordWebhookClient discord = new(_webhookDiscord);
            var managementNotify = new NotifyDiscord();

            var notify = context.Message;
            _logger.Info($"Recive this message: {notify.Message}");

            managementNotify.SendNotify(discord, notify, null);

            return Task.CompletedTask;
        }
    }
}
