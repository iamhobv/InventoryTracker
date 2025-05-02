using System.ComponentModel.DataAnnotations.Schema;
using static InventoryTracker.Enums.NotificationStatus;

namespace InventoryTracker.Models
{
    public class Notification : BaseModel
    {
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public NotificationStatusEnum NotificationStatus { get; set; }


        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
