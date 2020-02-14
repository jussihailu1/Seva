using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace Seva.Interface.ILogicModels
{
    public interface IUser
    {
        int Id { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string FullName { get; set; }
        string Email { get; set; }
        string Password { get; set; }
        string Description { get; set; }
        int Role { get; set; }
        decimal AverageRating { get; set; }
        int Status { get; set; }
    }
}
