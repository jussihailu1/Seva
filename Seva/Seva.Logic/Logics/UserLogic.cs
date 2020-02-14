using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Seva.Factory.DAL;
using Seva.Interface.IContextModels;
using Seva.Interface.IContexts;
using Seva.Interface.ILogicModels;
using Seva.Interface.ILogics;
using Seva.Logic.Models;

namespace Seva.Logic.Logics
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserContext _userContext;
        private readonly IOfferedServiceLogic _offeredServiceLogic;

        public UserLogic()
        {
            _userContext = ContextFactory.CreateUserContext();
            _offeredServiceLogic = new OfferedServiceLogic();
        }

        public bool CheckAccountExistance(string email, string password) => _userContext.CheckAccountExistance(email, password);

        public bool CheckEmailExistance(string email) => _userContext.CheckEmailExistance(email);

        public void Register(string firstName, string lastName, string email, string password, int roleId)
        {
            var user = DTOFactory.CreateUserDTO(firstName, lastName, email, password, roleId);
            _userContext.AddUser(user);
        }

        public void UpdateUser(int id, string firstName, string lastName, string email, string password, string description)
        {
            var userDTO = DTOFactory.CreateUserDTOForUpdate(id, firstName, lastName, email, password, description);
            _userContext.UpdateUser(userDTO);
        }

        public void SetStatus(int oldStatus, int userId)
        {
            var newStatus = oldStatus == 0 ? 1 : 0;
            _userContext.SetStatus(newStatus, userId);
        }

        public decimal CalcUserAvgRating(int userId) => _offeredServiceLogic.GetOfferedServicesByUserId(userId).Average(offeredService => offeredService.AverageRating);


        private IUser ConvertUser(IUserDTO userDTO)
        {
            return new User()
            {
                Id = userDTO.Id,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                Email = userDTO.Email,
                Password = userDTO.Password,
                Description = userDTO.Description,
                Role = userDTO.RoleId,
                Status = userDTO.StatusId
            };
        }

        #region Gets

        public IUser GetUserById(int userId)
        {
            var userDTO = _userContext.GetUserById(userId);

            return ConvertUser(userDTO);
        }

        public IUser GetUserByEmail(string email)
        {
            var userDTO = _userContext.GetUserByEmail(email);

            return ConvertUser(userDTO);
        }

        public IEnumerable<IUser> GetAllUsers()
        {
            var userDTOs = _userContext.GetAllUsers();
            var usersToReturn = userDTOs.Select(ConvertUser);

            return usersToReturn;
        }

        #endregion
    }
}
