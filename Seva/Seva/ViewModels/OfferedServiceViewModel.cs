using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Seva.Interface.ILogicModels;
using Seva.Logic.Models;

namespace Seva.ViewModels
{
    public class OfferedServiceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int DeliveryTimeDays { get; set; }
        public int DeliveryTimeHours { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Status { get; set; }
        public UserViewModel Provider { get; set; }
        public string DateOfPost { get; set; }
        public double AverageRating { get; set; }
        public List<string> Images { get; set; }

        public OfferedServiceViewModel(IOfferedService offeredService, UserViewModel provider, List<string> images)
        {
            Id = offeredService.Id;
            Name = offeredService.Name;
            Category = offeredService.Category;
            DeliveryTimeDays = offeredService.DeliveryTimeDays;
            DeliveryTimeHours = offeredService.DeliveryTimeHours;
            Description = offeredService.Description;
            Price = offeredService.Price;
            Status = offeredService.Status == 0 ? "Active" : "Inactive";
            var dop = offeredService.DateOfPost;
            DateOfPost =  offeredService.DateOfPost.ToString("d");
            AverageRating = decimal.ToDouble(offeredService.AverageRating);
            Provider = provider;
            Images = images;
        }

        public OfferedServiceViewModel(IOfferedService offeredService, List<string> images)
        {
            Id = offeredService.Id;
            Name = offeredService.Name;
            Category = offeredService.Category;
            DeliveryTimeDays = offeredService.DeliveryTimeDays;
            DeliveryTimeHours = offeredService.DeliveryTimeHours;
            Description = offeredService.Description;
            Price = offeredService.Price;
            Status = offeredService.Status == 0 ? "Active" : "Inactive";
            DateOfPost = offeredService.DateOfPost.ToString("d");
            AverageRating = decimal.ToDouble(offeredService.AverageRating);
            Images = images;
        }

        public OfferedServiceViewModel(IOfferedService offeredService)
        {
            Id = offeredService.Id;
            Name = offeredService.Name;
            Category = offeredService.Category;
            DeliveryTimeDays = offeredService.DeliveryTimeDays;
            DeliveryTimeHours = offeredService.DeliveryTimeHours;
            Description = offeredService.Description;
            Price = offeredService.Price;
            Status = offeredService.Status == 0? "Active" : "Inactive";
            DateOfPost = offeredService.DateOfPost.ToString("d");
            AverageRating = decimal.ToDouble(offeredService.AverageRating);
        }
    }
}
