using System;
using System.Collections.Generic;
using System.Text;
using Seva.Interface.IContextModels;

namespace Seva.Interface.IContexts
{
    public interface IUserContext
    {
        bool CheckEmailExistance(string email);
        bool CheckAccountExistance(string email, string password);
        void AddUser(IUserDTO user);
        void UpdateUser(IUserDTO userDTO);
        void SetStatus(int statusId, int userId);


        #region Gets

        IUserDTO GetUserById(int userId);
        IUserDTO GetUserByEmail(string email);
        IEnumerable<IUserDTO> GetAllUsers();

        #endregion
    }
}
