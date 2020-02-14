using System;
using System.Collections.Generic;
using System.Text;

namespace Seva.Interface.IContextModels
{
    public interface IUserDTO
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string Description { get; set; }
        int RoleId { get; set; }
        int StatusId { get; set; }
        int IsDeletedId { get; set; }
    }
}
