using InventoryTracker.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace InventoryTracker.Data
{
    public class GeneralResponse
    {
        public bool IsPass { get; set; }
        public dynamic Data { get; set; }
    }
}
