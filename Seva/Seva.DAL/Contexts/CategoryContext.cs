using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Seva.DAL.Models;
using Seva.DAL.Utilities;
using Seva.Interface.IContextModels;
using Seva.Interface.IContexts;

namespace Seva.DAL.Contexts
{
    public class CategoryContext : ICategoryContext
    {

        #region Gets
        public IEnumerable<ICategoryDTO> GetAllCategories()
        {
            var categoryDTOs = new List<ICategoryDTO>();
            string query = "SELECT * FROM categories";

            using var connection = MySqlFactory.CreateConnection();
            connection.Open();
            var command = MySqlFactory.CreateCommand(query, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                categoryDTOs.Add(new CategoryDTO()
                {
                    Id = reader.GetInt16("id"),
                    Name = reader.GetString("name")
                });
            }

            return categoryDTOs;
        }

        #endregion
    }
}
