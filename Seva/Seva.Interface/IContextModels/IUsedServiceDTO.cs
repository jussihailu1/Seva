using System;
using System.Collections.Generic;
using System.Text;
using Seva.Interface.ILogicModels;

namespace Seva.Interface.IContextModels
{
    public interface IUsedServiceDTO
    {
        int Id { get; set; }
        int OfferedServiceId { get; set; }
        int ConsumerId { get; set; }
        DateTime DateOfPurchase { get; set; }
    }
}
