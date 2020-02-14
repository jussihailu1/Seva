using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Seva.Interface.ILogicModels;

namespace Seva.ViewModels
{
    public class UsedServiceViewModel
    {
        public int Id { get; set; }
        public OfferedServiceViewModel OfferedService { get; set; }
        public DateTime DateOfPurchase { get; set; }
        public ReviewViewModel Review { get; set; }

        public UsedServiceViewModel(IUsedService usedService, OfferedServiceViewModel offeredService, ReviewViewModel review)
        {
            Id = usedService.Id;
            OfferedService = offeredService;
            DateOfPurchase = usedService.DateOfPurchase;
            Review = review;
        }

        public UsedServiceViewModel(IUsedService usedService, OfferedServiceViewModel offeredService)
        {
            Id = usedService.Id;
            OfferedService = offeredService;
            DateOfPurchase = usedService.DateOfPurchase;
        }

        public UsedServiceViewModel(DateTime dateOfPurchase, ReviewViewModel review)
        {
            DateOfPurchase = dateOfPurchase;
            Review = review;
        }
    }
}
