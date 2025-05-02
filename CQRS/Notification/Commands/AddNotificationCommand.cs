using InventoryTracker.Models;
using System.ComponentModel.DataAnnotations.Schema;
using MediatR;
using static InventoryTracker.Enums.NotificationStatus;
using InventoryTracker.Data;
using InventoryTracker.Services;

namespace InventoryTracker.CQRS.Notification.Commands
{
    public class AddNotificationCommand : IRequest<bool>
    {
        public string Message { get; set; }

        public int ProductId { get; set; }

        public string UserId { get; set; }
    }
    public class AddNotificationCommandHandler : IRequestHandler<AddNotificationCommand, bool>
    {
        private readonly IGeneralRepo<Models.Notification> repo;
        private readonly IMediator mediator;

        public AddNotificationCommandHandler(IGeneralRepo<Models.Notification> repo, IMediator mediator)
        {
            this.repo = repo;
            this.mediator = mediator;
        }
        public async Task<bool> Handle(AddNotificationCommand request, CancellationToken cancellationToken)
        {
            Models.Notification notification = request.Map<Models.Notification>();
            notification.IsDeleted = false;
            notification.Date = DateTime.Now;
            notification.NotificationStatus = NotificationStatusEnum.Pending;
            notification.Message = request.Message;

            repo.Add(notification);
            await mediator.Send(new SaveChanges());
            return true;

        }
    }

}
