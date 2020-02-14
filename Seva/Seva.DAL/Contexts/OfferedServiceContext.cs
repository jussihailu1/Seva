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
    public class OfferedServiceContext : IOfferedServiceContext
    {
        public void Add(IOfferedServiceDTO offeredServiceDTO)
        {
            using var connection = MySqlFactory.CreateConnection();
            connection.Open();
            var cmd = MySqlFactory.CreateCommand("AddOfferedService", connection);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@name", offeredServiceDTO.Name);
            cmd.Parameters.AddWithValue("@categoryId", offeredServiceDTO.CategoryId);
            cmd.Parameters.AddWithValue("@price", offeredServiceDTO.Price);
            cmd.Parameters.AddWithValue("@providerId", offeredServiceDTO.ProviderId);
            cmd.Parameters.AddWithValue("@description", offeredServiceDTO.Description);
            cmd.Parameters.AddWithValue("@status", offeredServiceDTO.StatusId);
            cmd.Parameters.AddWithValue("@deliveryTimeDays", offeredServiceDTO.DeliveryTimeDays);
            cmd.Parameters.AddWithValue("@deliveryTimeHours", offeredServiceDTO.DeliveryTimeHours);
            cmd.Parameters.AddWithValue("@dateOfPost", offeredServiceDTO.DateOfPost);
            cmd.Parameters.AddWithValue("@isDeleted", Convert.ToInt16(offeredServiceDTO.IsDeleted));

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void Update(IOfferedServiceDTO offeredServiceDTO)
        {
            using var connection = MySqlFactory.CreateConnection();
            connection.Open();
            var cmd = MySqlFactory.CreateCommand("UpdateOfferedService", connection);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@_id", offeredServiceDTO.Id);
            cmd.Parameters.AddWithValue("@_name", offeredServiceDTO.Name);
            cmd.Parameters.AddWithValue("@_categoryId", offeredServiceDTO.CategoryId);
            cmd.Parameters.AddWithValue("@_price", offeredServiceDTO.Price);
            cmd.Parameters.AddWithValue("@_description", offeredServiceDTO.Description);
            cmd.Parameters.AddWithValue("@_deliveryTimeDays", offeredServiceDTO.DeliveryTimeDays);
            cmd.Parameters.AddWithValue("@_deliveryTimeHours", offeredServiceDTO.DeliveryTimeHours);

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        public void SetStatus(int offeredServiceId, int status)
        {
            var query = $"UPDATE offeredServices SET offeredservices.status = {status} WHERE id = {offeredServiceId};";

            using var connection = MySqlFactory.CreateConnection();
            connection.Open();
            var command = MySqlFactory.CreateCommand(query, connection);
            command.ExecuteNonQuery();
        }

        public void UpdateRating(int offeredServiceId, decimal rating)
        {
            using var connection = MySqlFactory.CreateConnection();
            connection.Open();
            var cmd = MySqlFactory.CreateCommand("UpdateRating", connection);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@id", offeredServiceId);
            cmd.Parameters.AddWithValue("@rating", rating);

            cmd.ExecuteNonQuery();
            connection.Close();
        }


        #region Gets

        private IEnumerable<IOfferedServiceDTO> GetOfferedServicesWhere(string whereClause)
        {
            var offeredServiceDTOs = new List<IOfferedServiceDTO>();

            string query = "SELECT * FROM offeredServices WHERE " + whereClause;

            using var connection = MySqlFactory.CreateConnection();
            connection.Open();
            var command = MySqlFactory.CreateCommand(query, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                offeredServiceDTOs.Add(new OfferedServiceDTO()
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    CategoryId = reader.GetInt16("categoryId"),
                    Price = reader.GetDecimal("price"),
                    StatusId = reader.GetInt16("status"),
                    DeliveryTimeDays = reader.GetInt32("deliveryTimeDays"),
                    DeliveryTimeHours = reader.GetInt32("deliveryTimeHours"),
                    DateOfPost = reader.GetDateTime("dateOfpost"),
                    Description = reader.GetString("description"),
                    ProviderId = reader.GetInt32("providerId"),
                    IsDeleted = Convert.ToBoolean(reader.GetInt16("isDeleted"))
                });
            }
            return offeredServiceDTOs;
        }

        public IOfferedServiceDTO GetOfferedServiceById(int offeredServiceId)
        {
            var offeredServiceDTO = new OfferedServiceDTO();
            string query = "SELECT * FROM offeredservices WHERE offeredServices.id = " + offeredServiceId + "";

            using var connection = MySqlFactory.CreateConnection();
            connection.Open();
            var command = MySqlFactory.CreateCommand(query, connection);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                offeredServiceDTO.Id = reader.GetInt32("id");
                offeredServiceDTO.Name = reader.GetString("name");
                offeredServiceDTO.CategoryId = reader.GetInt16("categoryId");
                offeredServiceDTO.Price = reader.GetDecimal("price");
                offeredServiceDTO.StatusId = reader.GetInt16("status");
                offeredServiceDTO.DeliveryTimeDays = reader.GetInt32("deliveryTimeDays");
                offeredServiceDTO.DeliveryTimeHours = reader.GetInt32("deliveryTimeHours");
                offeredServiceDTO.DateOfPost = reader.GetDateTime("dateOfpost");
                offeredServiceDTO.Description = reader.GetString("description");
                offeredServiceDTO.ProviderId = reader.GetInt32("providerId");
                offeredServiceDTO.IsDeleted = Convert.ToBoolean(reader.GetInt16("isDeleted"));

            }

            return offeredServiceDTO;
        }

        public IOfferedServiceDTO GetLatestOfferedServiceByProviderId(int providerId)
        {
            var offeredServiceDTO = new OfferedServiceDTO();
            string query = "SELECT * FROM offeredservices WHERE offeredservices.providerId = " +
                           providerId + "  ORDER BY offeredservices.id DESC LIMIT 1; ";

            using var connection = MySqlFactory.CreateConnection();
            connection.Open();
            MySqlCommand command = MySqlFactory.CreateCommand(query, connection);

            MySqlDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                offeredServiceDTO.Id = reader.GetInt32("id");
                offeredServiceDTO.Name = reader.GetString("name");
                offeredServiceDTO.CategoryId = reader.GetInt16("categoryId");
                offeredServiceDTO.Price = reader.GetDecimal("price");
                offeredServiceDTO.StatusId = reader.GetInt16("status");
                offeredServiceDTO.DeliveryTimeDays = reader.GetInt32("deliveryTimeDays");
                offeredServiceDTO.DeliveryTimeHours = reader.GetInt32("deliveryTimeHours");
                offeredServiceDTO.DateOfPost = reader.GetDateTime("dateOfpost");
                offeredServiceDTO.Description = reader.GetString("description");
                offeredServiceDTO.ProviderId = reader.GetInt32("providerId");
                offeredServiceDTO.IsDeleted = Convert.ToBoolean(reader.GetInt16("isDeleted"));
            }

            return offeredServiceDTO;
        }

        public IEnumerable<IOfferedServiceDTO> GetAllOfferedServices()
        {
            var offeredServiceDTOs = new List<IOfferedServiceDTO>();
            string query = "SELECT * FROM offeredServices";

            using var connection = MySqlFactory.CreateConnection();
            connection.Open();
            var command = MySqlFactory.CreateCommand(query, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                offeredServiceDTOs.Add(new OfferedServiceDTO()
                {
                    Id = reader.GetInt32("id"),
                    Name = reader.GetString("name"),
                    CategoryId = reader.GetInt16("categoryId"),
                    Price = reader.GetDecimal("price"),
                    StatusId = reader.GetInt16("status"),
                    DeliveryTimeDays = reader.GetInt32("deliveryTimeDays"),
                    DeliveryTimeHours = reader.GetInt32("deliveryTimeHours"),
                    DateOfPost = reader.GetDateTime("dateOfpost"),
                    Description = reader.GetString("description"),
                    ProviderId = reader.GetInt32("providerId"),
                    IsDeleted = Convert.ToBoolean(reader.GetInt16("isDeleted"))
                });
            }
            return offeredServiceDTOs;
        }

        public IEnumerable<IOfferedServiceDTO> GetOfferedServicesByUserId(int userId)
        {
            string whereClause = "offeredServices.providerId = '" + userId + "'";
            return GetOfferedServicesWhere(whereClause);
        }

        public IEnumerable<IOfferedServiceDTO> GetOfferedServicesByCategoryId(int categoryId)
        {
            string whereClause = "offeredServices.categoryId = '" + categoryId + "'";
            return GetOfferedServicesWhere(whereClause);
        }

        #endregion
    }
}
