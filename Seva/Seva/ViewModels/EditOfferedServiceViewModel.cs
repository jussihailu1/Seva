using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Seva.ViewModels
{
    public class EditOfferedServiceViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public int DeliveryTimeDays { get; set; }
        public int DeliveryTimeHours { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public List<string> Images { get; set; }
    }
}
