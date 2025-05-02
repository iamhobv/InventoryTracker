using System.ComponentModel.DataAnnotations.Schema;
using System.Transactions;
using Microsoft.AspNetCore.Identity;

namespace InventoryTracker.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsDeleted { get; set; }


        public List<InventoryTransaction>? InventoryTransactions { get; set; }
        public List<ArchivedInventoryTransaction>? ArchivedInventoryTransaction { get; set; }
        public List<Report>? Reports { get; set; }

        public List<Notification>? Notifications { get; set; }
    }
}
