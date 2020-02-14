using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using MySql.Data.MySqlClient;
using Seva.DAL.Models;
using Seva.DAL.Utilities;
using Seva.Interface.IContextModels;
using Seva.Interface.IContexts;

namespace Seva.DAL.Contexts
{
    public class UserContext : IUserContext
    {
        public bool CheckEmailExistance(string email)
        {
            string query = "SELECT users.email FROM users WHERE users.email = '" + email + "'";
            using var connection = MySqlFactory.CreateConnection();
            connection.Open();
            var command = MySqlFactory.CreateCommand(query, connection);

            command.ExecuteNonQuery();

            var dataTable = new DataTable();
            var adapter = MySqlFactory.CreateDataAdapter(command);
            adapter.Fill(dataTable);

            return dataTable.Rows.Count == 1;
        }

        public bool CheckAccountExistance(string email, string password)
        {
            string query = "SELECT users.email, users.password FROM users WHERE users.email = '" + email +
                           "' AND users.password = '" + password + "'";

            using var connection = MySqlFactory.CreateConnection();
            connection.Open();
            var command = MySqlFactory.CreateCommand(query, connection);
            command.ExecuteNonQuery();

            var dataTable = new DataTable();
            var adapter = MySqlFactory.CreateDataAdapter(command);
            adapter.Fill(dataTable);

            return dataTable.Rows.Count == 1;
        }

        public void AddUser(IUserDTO user)
        {
            string query = "INSERT INTO users (firstName, lastName, email, password, roleId) VALUES('" +
                           user.FirstName + "', '" + user.LastName + "', '" + user.Email + "', '" + user.Password + "', '" + user.RoleId + "'); ";

            using var connection = MySqlFactory.CreateConnection();
            connection.Open();
            var command = MySqlFactory.CreateCommand(query, connection);
            command.ExecuteNonQuery();
        }

        public void UpdateUser(IUserDTO userDTO)
        {
            var query = $"UPDATE users SET firstname = '{userDTO.FirstName}', lastname = '{userDTO.LastName}', email = '{userDTO.Email}', password = '{userDTO.Password}', description = '{userDTO.Description}' WHERE id = {userDTO.Id}; ";

            using var connection = MySqlFactory.CreateConnection();
            connection.Open();
            var command = MySqlFactory.CreateCommand(query, connection);
            command.ExecuteNonQuery();
        }

        public void SetStatus(int statusId, int userId)
        {
            var query = $"UPDATE users SET statusId = {statusId} WHERE users.id = {userId}";
            using var connection = MySqlFactory.CreateConnection();
            connection.Open();
            var command = MySqlFactory.CreateCommand(query, connection);
            command.ExecuteNonQuery();
        }

        #region Gets

        private IUserDTO GetUserBy(string whereClause)
        {
            var userDTO = new UserDTO();
            var commandText = "SELECT * FROM users WHERE " + whereClause;

            using var connection = MySqlFactory.CreateConnection();
            connection.Open();
            var command = MySqlFactory.CreateCommand(commandText, connection);

            var dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                userDTO.Id = Convert.ToInt32(dataReader["id"]);
                userDTO.FirstName = dataReader["firstName"].ToString();
                userDTO.LastName = dataReader["lastName"].ToString();
                userDTO.Email = dataReader["email"].ToString();
                userDTO.Password = dataReader["password"].ToString();
                userDTO.Description = dataReader["description"].ToString();
                userDTO.RoleId = Convert.ToInt16(dataReader["roleId"]);
                userDTO.StatusId = Convert.ToInt16(dataReader["statusId"]);
                userDTO.IsDeletedId = Convert.ToInt16(dataReader["isDeleted"]);

                return userDTO;
            }

            return null;
        }


        public IUserDTO GetUserById(int userId)
        {
            string whereClause = "users.id = " + userId + "";
            return GetUserBy(whereClause);
        }

        public IUserDTO GetUserByEmail(string email)
        {
            string whereClause = "users.email = '" + email + "'";
            return GetUserBy(whereClause);
        }

        public IEnumerable<IUserDTO> GetAllUsers()
        {
            var userDTOs = new List<IUserDTO>();
            const string commandText = "SELECT * FROM users";

            using var connection = MySqlFactory.CreateConnection();
            connection.Open();
            var command = MySqlFactory.CreateCommand(commandText, connection);

            var dataReader = command.ExecuteReader();

            while (dataReader.Read())
            {
                userDTOs.Add(new UserDTO()
                {
                    Id = Convert.ToInt32(dataReader["id"]),
                    FirstName = dataReader["firstName"].ToString(),
                    LastName = dataReader["lastName"].ToString(),
                    Email = dataReader["email"].ToString(),
                    Password = dataReader["password"].ToString(),
                    Description = dataReader["description"].ToString(),
                    RoleId = Convert.ToInt16(dataReader["roleId"]),
                    StatusId = Convert.ToInt16(dataReader["statusId"]),
                    IsDeletedId = Convert.ToInt16(dataReader["isDeleted"])
                });
            }

            return userDTOs;
        }

        #endregion
    }
}
