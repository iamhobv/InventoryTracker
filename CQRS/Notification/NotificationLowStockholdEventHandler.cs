using InventoryTracker.CQRS.Notification.Commands;
using InventoryTracker.CQRS.Products.Events;
using InventoryTracker.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace InventoryTracker.CQRS.Notification
{
    public class NotificationLowStockholdEventHandler : INotificationHandler<ProductQuantityLessThanThresholdEvent>
    {
        private readonly IMediator mediator;
        private readonly UserManager<ApplicationUser> userManager;

        public NotificationLowStockholdEventHandler(IMediator mediator, UserManager<ApplicationUser> userManager)
        {
            this.mediator = mediator;
            this.userManager = userManager;
        }
        public async Task Handle(ProductQuantityLessThanThresholdEvent notification, CancellationToken cancellationToken)
        {
            var listOfAdmins = await userManager.GetUsersInRoleAsync("Admin");
            foreach (var admin in listOfAdmins)
            {
                await mediator.Send(new AddNotificationCommand()
                {
                    UserId = admin.Id,
                    ProductId = notification.ProductID,
                    Message = $"the product  {notification.ProductID} has low stock"
                });
            }


        }
    }
}
