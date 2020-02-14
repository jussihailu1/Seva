using System;
using System.Collections.Generic;
using System.Text;

namespace Seva.Interface.ILogicModels
{
    public interface IUsedService
    {
        int Id { get; set; }
        int OfferedServiceId { get; set; }
        int ConsumerId { get; set; }
        DateTime DateOfPurchase { get; set; }
    }
}
