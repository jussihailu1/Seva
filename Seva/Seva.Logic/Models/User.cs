using System;
using System.Collections.Generic;
using System.Text;
using Seva.Interface.ILogicModels;

namespace Seva.Logic.Models
{
    public class User : IUser
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public decimal AverageRating { get; set; }
        public int Status { get; set; }
        public int Role { get; set; }
        public string FullName
        {
            get => FirstName + " " + LastName;
            set
            {
                
            }
        }
    }
}
