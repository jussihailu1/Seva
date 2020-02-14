using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Seva.Interface.ILogicModels;

namespace Seva.ViewModels
{
    public class AdminUsedServiceViewModel
    {
        public int Id { get; set; }
        public OfferedServiceViewModel OfferedService { get; set; }
        public UserViewModel Consumer { get; set; }
        public DateTime DateOfPurchase { get; set; }


        public AdminUsedServiceViewModel(IUsedService usedService, OfferedServiceViewModel offeredService, UserViewModel user)
        {
            Id = usedService.Id;
            OfferedService = offeredService;
            DateOfPurchase = usedService.DateOfPurchase;
            Consumer = user;
        }
    }
}
