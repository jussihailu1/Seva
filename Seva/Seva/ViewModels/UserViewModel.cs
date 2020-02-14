using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Seva.Interface.ILogicModels;

namespace Seva.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public decimal AverageRating { get; set; }
        public IEnumerable<OfferedServiceViewModel> OfferedServices { get; set; }

        public UserViewModel(IUser user)
        {
            Id = user.Id;
            FullName = user.FullName;
            Email = user.Email;
            Description = user.Description;
            AverageRating = user.AverageRating;
        }

        public UserViewModel(IUser user, List<OfferedServiceViewModel> offeredServices)
        {
            Id = user.Id;
            FullName = user.FullName;
            Email = user.Email;
            Description = user.Description;
            AverageRating = user.AverageRating;
            OfferedServices = offeredServices;
        }
    }
}

