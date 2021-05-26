using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BackgroundQueue.Models;
using BackgroundQueue.Services;

namespace BackgroundQueue.Controllers
{
    public class HomeController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBackgroundQueue<DrugNotification> _queue;

        public HomeController(ILogger<HomeController> logger, IBackgroundQueue<DrugNotification> queue)
        {
            _logger = logger;
            _queue = queue;
        }

        [HttpPost]
        public async Task<IActionResult> Notify([FromBody] DrugNotification notification)
        {
            _queue.Enqueue(notification);
            return await Task.FromResult(Accepted());
        }
    }
}
