using System;
using System.Collections.Generic;
using System.Text;
using Seva.Interface.IContextModels;

namespace Seva.DAL.Models
{
    public class OfferedServiceDTO : IOfferedServiceDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public int DeliveryTimeDays { get; set; }
        public int DeliveryTimeHours { get; set; }
        public int StatusId { get; set; }
        public int ProviderId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime DateOfPost { get; set; }
        public bool IsDeleted { get; set; }
    }
}
