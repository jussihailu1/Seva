using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Seva.Interface.ILogicModels;

namespace Seva.ViewModels
{
    public class ProfileViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public List<OfferedServiceViewModel> OfferedServiceViewModels { get; set; }
        public List<UsedServiceViewModel> UsedServiceViewModels { get; set; }
    }
}
