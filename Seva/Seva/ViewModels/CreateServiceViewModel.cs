using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Seva.ViewModels
{
    public class CreateServiceViewModel
    {
        [Required(ErrorMessage = "Enter a name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Select a category")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Enter delivery time days")]
        public int DeliveryTimeDays { get; set; }

        [Required(ErrorMessage = "Enter delivery time days")]
        public int DeliveryTimeHours { get; set; }
        
        [Required(ErrorMessage = "Enter a description")]
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Enter a price")]
        [DataType(DataType.Text)]
        public decimal Price { get; set; }

        [DataType(DataType.Upload)]
        public List<IFormFile> ImageFiles { get; set; }
    }
}
