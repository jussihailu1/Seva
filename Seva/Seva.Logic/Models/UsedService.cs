using System;
using System.Collections.Generic;
using System.Text;
using Seva.Interface.ILogicModels;

namespace Seva.Logic.Models
{
    public class UsedService : IUsedService
    {
        public int Id { get; set; }
        public int OfferedServiceId { get; set; }
        public int ConsumerId { get; set; }
        public DateTime DateOfPurchase { get; set; }
    }
}
