using System.ComponentModel.DataAnnotations.Schema;
using static InventoryTracker.Enums.ReportType;

namespace InventoryTracker.Models
{
    public class Report : BaseModel
    {
        public DateTime CreationDate { get; set; }
        public ReportTypeEnum ReportType { get; set; }


        [ForeignKey("User")]
        public string UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
