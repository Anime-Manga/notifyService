using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;

namespace Cesxhin.AnimeManga.NotifyService
{
    public class Worker : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1200000, stoppingToken);
            }
        }
    }
}