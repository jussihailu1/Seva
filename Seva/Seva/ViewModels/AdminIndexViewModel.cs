using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seva.ViewModels
{
    public class AdminIndexViewModel
    {
        public List<AdminUserViewModel> Users { get; set; }  
        public List<AdminOfferedServiceViewModel> OfferedServices { get; set; }
        public List<AdminUsedServiceViewModel> UsedServices { get; set; }    
    }
}
