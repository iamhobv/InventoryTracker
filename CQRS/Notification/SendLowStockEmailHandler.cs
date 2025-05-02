using InventoryTracker.CQRS.Notification.Commands;
using InventoryTracker.CQRS.Products.Events;
using InventoryTracker.Models;
using MediatR;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;

namespace InventoryTracker.CQRS.Notification
{
    public class SendLowStockEmailHandler : INotificationHandler<ProductQuantityLessThanThresholdEvent>
    {
        private readonly ILogger<SendLowStockEmailHandler> logger;
        private readonly UserManager<ApplicationUser> userManager;

        public SendLowStockEmailHandler(ILogger<SendLowStockEmailHandler> logger, UserManager<ApplicationUser> userManager)
        {
            this.logger = logger;
            this.userManager = userManager;
        }
        public async Task Handle(ProductQuantityLessThanThresholdEvent notification, CancellationToken cancellationToken)
        {
            var listOfAdmins = await userManager.GetUsersInRoleAsync("Admin");

            foreach (var admin in listOfAdmins)
            {
                logger.LogInformation($"Sending LowStock email to {admin.Id} notify him that the product of Id {notification.ProductID} has low stock ");

            }


        }
    }
}
