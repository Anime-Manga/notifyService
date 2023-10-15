using Cesxhin.AnimeManga.Domain.DTO;
using Cesxhin.AnimeManga.Modules.NlogManager;
using NLog;
using System;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Cesxhin.AnimeManga.Application.Notify
{
    public class NotifyTelegramChannel : Notify<TelegramBotClient, NotifyDTO>
    {
        //nlog
        private readonly NLogConsole _logger = new(LogManager.GetCurrentClassLogger());

        public override void SendNotify(TelegramBotClient bot, NotifyDTO notify, string chat_id)
        {
            if (notify.Image != null)
            {
                Uri urlImage = new(notify.Image);
                try
                {
                    bot.SendPhotoAsync(chat_id, InputFile.FromUri(urlImage), null, notify.Message).GetAwaiter();
                }
                catch (Exception err)
                {
                    _logger.Error($"Problem bot telegram cannot send photo and text message, details: {err.Message}");
                }
            }
            else
            {
                try
                {
                    bot.SendTextMessageAsync(chat_id, notify.Message);
                }
                catch (Exception err)
                {
                    _logger.Error($"Problem bot telegram cannot send text message, details: {err.Message}");
                }
            }

            _logger.Info("Ok send done!");
        }
    }
}
