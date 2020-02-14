using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seva.ViewModels
{
    public class MyOfferedServiceViewModel
    {
        public OfferedServiceViewModel OfferedService { get; set; }
        public int AmountUsed { get; set; }
        public List<ReviewViewModel> Reviews { get; set; }
    }
}
