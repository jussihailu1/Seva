using System;
using System.Collections.Generic;
using System.Text;

namespace Seva.Interface.ILogicModels
{
    public interface IOfferedService
    {
        int Id { get; set; }
        string Name { get; set; }
        string Category { get; set; }
        int DeliveryTimeDays { get; set; }
        int DeliveryTimeHours { get; set; }
        string Description { get; set; }
        decimal Price { get; set; }
        int Status { get; set; }
        int ProviderId { get; set; }
        DateTime DateOfPost { get; set; }
        decimal AverageRating { get; set; }
        bool IsDeleted { get; set; }
    }
}
