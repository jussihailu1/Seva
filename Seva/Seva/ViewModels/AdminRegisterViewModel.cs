using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Seva.ViewModels
{
    public class AdminRegisterViewModel
    {
        [Required(ErrorMessage = "Enter first name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Enter last name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Enter email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Enter password")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Enter currently loggedin admins password")]
        public string AdminPassword { get; set; }
    }
}
