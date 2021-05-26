using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using BackgroundQueue.Models;
using BackgroundQueue.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace BackgroundQueue.Worker
{
    public class BackgroundWorker : BackgroundService
    {
        private readonly IBackgroundQueue<DrugNotification> _queue;
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<BackgroundWorker> _logger;

        public BackgroundWorker(IBackgroundQueue<DrugNotification> queue, IServiceScopeFactory scopeFactory, ILogger<BackgroundWorker> logger)
        {
            _queue = queue;
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await BackgroundProcessing(stoppingToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogCritical("STOP!");
            return base.StopAsync(cancellationToken);
        }

        private async Task BackgroundProcessing(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay(5000, stoppingToken);

                    var drugNotification = _queue.Dequeue();

                    if (drugNotification == null) continue;

                    using var scope = _scopeFactory.CreateScope();
                    var sender = scope.ServiceProvider.GetRequiredService<IDrugNotificationSender>();
                    await sender.Send(drugNotification, stoppingToken);
                }
                catch (Exception ex)
                {
                    _logger.LogCritical($"Some exception: {ex}");
                }
            }
        }
    }
}
