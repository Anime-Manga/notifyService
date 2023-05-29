﻿using Cesxhin.AnimeManga.Modules.Exceptions;
using Cesxhin.AnimeManga.Modules.NlogManager;
using Cesxhin.AnimeManga.Domain.DTO;
using Discord.Webhook;
using MassTransit;
using NLog;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace Cesxhin.AnimeManga.Application.Consumers
{
    public class NotifyConsumer : IConsumer<NotifyDTO>
    {
        //nlog
        private readonly NLogConsole _logger = new(LogManager.GetCurrentClassLogger());

        //webhook discord
        private readonly string _webhookDiscord = Environment.GetEnvironmentVariable("WEBHOOK_DISCORD");

        public Task Consume(ConsumeContext<NotifyDTO> context)
        {
            DiscordWebhookClient discord = new(_webhookDiscord);

            var notify = context.Message;
            _logger.Info($"Recive this message: {notify.Message}");

            try
            {
                if (notify.Image != null)
                {
                    byte[] bufferImage;
                    using (WebClient client = new WebClient())
                    {
                        bufferImage = client.DownloadData(new Uri(notify.Image));
                    }

                    Stream image = new MemoryStream(bufferImage);
                    discord.SendFileAsync(image, "Cover.png", notify.Message).GetAwaiter().GetResult();
                }
                else
                    discord.SendMessageAsync(notify.Message).GetAwaiter().GetResult();
                _logger.Info("Ok send done!");
            }
            catch (ApiGenericException ex)
            {
                _logger.Fatal($"error send webhook to discord, details error: {ex.Message}");
            }

            return Task.CompletedTask;
        }
    }
}