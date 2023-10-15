using Discord.Webhook;

namespace Cesxhin.AnimeManga.Application.Notify
{
    public abstract class Notify<S, T>
    {
        public abstract void SendNotify(S whereSend, T notify, string token);
    }
}
