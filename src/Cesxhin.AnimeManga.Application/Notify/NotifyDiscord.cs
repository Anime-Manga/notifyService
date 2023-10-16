using Cesxhin.AnimeManga.Domain.DTO;
using Cesxhin.AnimeManga.Modules.NlogManager;
using Discord.Webhook;
using NLog;
using System;
using System.IO;
using System.Net;

namespace Cesxhin.AnimeManga.Application.Notify
{
    public class NotifyDiscord : Notify<DiscordWebhookClient, GenericNotify>
    {
        //nlog
        private readonly NLogConsole _logger = new(LogManager.GetCurrentClassLogger());

        public override void SendNotify(DiscordWebhookClient discord, GenericNotify notify, string token)
        {
            if (notify.Image != null)
            {
                byte[] bufferImage;
                using (WebClient client = new WebClient())
                {
                    bufferImage = client.DownloadData(new Uri(notify.Image));
                }

                Stream image = new MemoryStream(bufferImage);
                try
                {
                    discord.SendFileAsync(image, "Cover.png", notify.Message).GetAwaiter().GetResult();
                }
                catch (Exception err)
                {
                    _logger.Error($"Problem webhook discord cannot send photo and text message, details: {err.Message}");
                }
            }
            else
            {
                try
                {
                    discord.SendMessageAsync(notify.Message).GetAwaiter().GetResult();
                }
                catch (Exception err)
                {
                    _logger.Error($"Problem webhook discord cannot send text message, details: {err.Message}");
                }
            }
            _logger.Info("Ok send done!");
        }
    }
}
