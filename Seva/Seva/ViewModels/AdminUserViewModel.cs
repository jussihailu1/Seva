using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Seva.Interface.ILogicModels;

namespace Seva.ViewModels
{
    public class AdminUserViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public decimal AverageRating { get; set; }
        public string Status { get; set; }
        public IEnumerable<OfferedServiceViewModel> OfferedServices { get; set; }
        public IEnumerable<UsedServiceViewModel> UsedServices { get; set; }
        public int OfferedServicesCount { get; set; }
        public int SoldServicesCount { get; set; }
        public int UsedServicesCount { get; set; }

        public AdminUserViewModel(IUser user)
        {
            Id = user.Id;
            FullName = user.FullName;
            Email = user.Email;
            Description = user.Description;
            AverageRating = user.AverageRating;
            Status = user.Status == 0 ? "Active" : "Inactive";
        }

        public AdminUserViewModel(IUser user, IEnumerable<OfferedServiceViewModel> offeredServices, IEnumerable<UsedServiceViewModel> usedServices, int offeredServicesCount, int usedServicesCount, int soldServicesCount)
        {
            Id = user.Id;
            FullName = user.FullName;
            Email = user.Email;
            Description = user.Description;
            AverageRating = user.AverageRating;
            Status = user.Status == 0 ? "Active" : "Inactive";
            OfferedServices = offeredServices;
            UsedServices = usedServices;
            OfferedServicesCount = offeredServicesCount;
            SoldServicesCount = soldServicesCount;
            UsedServicesCount = usedServicesCount;
        }
    }
}
