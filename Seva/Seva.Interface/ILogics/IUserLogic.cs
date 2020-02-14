using System;
using System.Collections.Generic;
using System.Text;
using Seva.Interface.ILogicModels;

namespace Seva.Interface.ILogics
{
    public interface IUserLogic
    {
        bool CheckAccountExistance(string email, string password);
        bool CheckEmailExistance(string email);
        void Register(string firstName, string lastName, string email, string password, int roleId);
        void UpdateUser(int id, string firstName, string lastName, string email, string password, string description);
        void SetStatus(int oldStatus, int userId);
        decimal CalcUserAvgRating(int userId);

        #region Gets

        IUser GetUserById(int userId);
        IUser GetUserByEmail(string email);
        IEnumerable<IUser> GetAllUsers();

        #endregion
    }
}
