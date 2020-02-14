using System;
using System.Collections.Generic;
using System.Text;

namespace Seva.Interface.IContextModels
{
    public interface IOfferedServiceDTO
    {
        int Id { get; set; }
        string Name { get; set; }
        int CategoryId { get; set; }
        int DeliveryTimeDays { get; set; }
        int DeliveryTimeHours { get; set; }
        int StatusId { get; set; }
        int ProviderId { get; set; }
        string Description { get; set; }
        decimal Price { get; set; }
        DateTime DateOfPost { get; set; }
        bool IsDeleted { get; set; }
    }
}
