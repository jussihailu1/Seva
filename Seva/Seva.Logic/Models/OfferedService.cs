using System;
using System.Collections.Generic;
using System.Text;
using Seva.Interface.ILogicModels;

namespace Seva.Logic.Models
{
    public class OfferedService : IOfferedService
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int DeliveryTimeDays { get; set; }
        public int DeliveryTimeHours { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Status { get; set; }
        public int ProviderId { get; set; }
        public DateTime DateOfPost { get; set; }
        public decimal AverageRating { get; set; }
        public bool IsDeleted { get; set; }
    }
}
