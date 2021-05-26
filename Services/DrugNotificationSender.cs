using System;
using System.Threading;
using System.Threading.Tasks;
using BackgroundQueue.Models;

namespace BackgroundQueue.Services
{
    public class DrugNotificationSender : IDrugNotificationSender
    {
        public Task Send(DrugNotification drugNotification, CancellationToken stoppingToken)
        {
            Console.WriteLine(drugNotification.Message);

            return Task.FromResult(drugNotification.Message);
        }
    }
}