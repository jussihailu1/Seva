using System;
using System.Collections.Generic;
using System.Text;
using Seva.Interface.IContextModels;

namespace Seva.DAL.Models
{
    public class UserDTO : IUserDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Description { get; set; }
        public int RoleId { get; set; }
        public int StatusId { get; set; }
        public int IsDeletedId { get; set; }
    }
}
