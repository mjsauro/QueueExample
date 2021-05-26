using System.Threading;
using System.Threading.Tasks;
using BackgroundQueue.Models;

namespace BackgroundQueue.Services
{
    public interface IDrugNotificationSender
    {
        Task Send(DrugNotification drugNotification, CancellationToken stoppingToken);
    }
}