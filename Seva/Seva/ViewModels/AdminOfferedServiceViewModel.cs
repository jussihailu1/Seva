using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Seva.Interface.ILogicModels;

namespace Seva.ViewModels
{
    public class AdminOfferedServiceViewModel
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
        public DateTime DateOfPost { get; set; }
        public double AverageRating { get; set; }
        public List<string> Images { get; set; }
        public List<UsedServiceViewModel> UsedServices { get; set; }
        public int AmountSold { get; set; }

        public AdminOfferedServiceViewModel(IOfferedService offeredService, UserViewModel provider, List<string> images, List<UsedServiceViewModel> usedServices, int amountSold)
        {
            Id = offeredService.Id; 
            Name = offeredService.Name;
            Category = offeredService.Category;
            DeliveryTimeDays = offeredService.DeliveryTimeDays;
            DeliveryTimeHours = offeredService.DeliveryTimeHours;
            Description = offeredService.Description;
            Price = offeredService.Price;
            Status = offeredService.Status == 0 ? "Active" : "Inactive";
            DateOfPost = offeredService.DateOfPost;
            AverageRating = decimal.ToDouble(offeredService.AverageRating);
            Provider = provider;
            Images = images;
            UsedServices = usedServices;
            AmountSold = amountSold;
        }

        public AdminOfferedServiceViewModel(IOfferedService offeredService, UserViewModel provider, int amountSold)
        {
            Id = offeredService.Id;
            Name = offeredService.Name;
            Category = offeredService.Category;
            DeliveryTimeDays = offeredService.DeliveryTimeDays;
            DeliveryTimeHours = offeredService.DeliveryTimeHours;
            Description = offeredService.Description;
            Price = offeredService.Price;
            Status = offeredService.Status == 0 ? "Active" : "Inactive";
            DateOfPost = offeredService.DateOfPost;
            AverageRating = decimal.ToDouble(offeredService.AverageRating);
            Provider = provider;
            AmountSold = amountSold;
        }
    }
}
