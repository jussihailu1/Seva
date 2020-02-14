using System;
using System.Collections.Generic;
using System.Text;
using Seva.Interface.IContextModels;

namespace Seva.DAL.Models
{
    public class UsedServiceDTO : IUsedServiceDTO
    {
        public int Id { get; set; }
        public int OfferedServiceId { get; set; }
        public int ConsumerId { get; set; }
        public DateTime DateOfPurchase { get; set; }
    }
}
