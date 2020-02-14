using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Seva.ViewModels
{
    public class WriteReviewViewModel
    {
        [Required(ErrorMessage = "Enter a title")]
        public string Title { get; set; }
        public string Text { get; set; }
        [Required(ErrorMessage = "Can not be 0")]
        public int Rating { get; set; }
        public OfferedServiceViewModel OfferedService { get; set; }
    }
}
