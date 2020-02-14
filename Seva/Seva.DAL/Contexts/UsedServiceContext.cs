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
    public class UsedServiceContext : IUsedServiceContext
    {
        public void AddUsedService(IUsedServiceDTO usedServiceDTO)
        {
            using var connection = MySqlFactory.CreateConnection();
            connection.Open();
            var command = MySqlFactory.CreateCommand("AddUsedService", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@consumerId", usedServiceDTO.ConsumerId);
            command.Parameters.AddWithValue("@offeredServiceId", usedServiceDTO.OfferedServiceId);
            command.Parameters.AddWithValue("@dateOfPurchase", usedServiceDTO.DateOfPurchase);

            command.ExecuteNonQuery();
            connection.Close();
        }

        #region Gets

        public IUsedServiceDTO GetUsedServiceById(int usedServiceId)
        {
            var usedServiceDTO = new UsedServiceDTO();
            string query = "SELECT * FROM usedservices WHERE usedservices.id = " + usedServiceId + "";

            using var connection = MySqlFactory.CreateConnection();
            connection.Open();
            var command = MySqlFactory.CreateCommand(query, connection);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {

                usedServiceDTO.Id = reader.GetInt32("id");
                usedServiceDTO.ConsumerId = reader.GetInt32("consumerId");
                usedServiceDTO.OfferedServiceId = reader.GetInt32("offeredServiceId");
                usedServiceDTO.DateOfPurchase = reader.GetDateTime("dateOfPurchase");
            }

            return usedServiceDTO;
        }

        private IEnumerable<IUsedServiceDTO> GetUsedServicesWhere(string whereClause)
        {
            var usedServiceDTOs = new List<IUsedServiceDTO>();
            string query = "SELECT * FROM usedServices WHERE " + whereClause;

            using var connection = MySqlFactory.CreateConnection();
            connection.Open();
            var command = MySqlFactory.CreateCommand(query, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                usedServiceDTOs.Add(new UsedServiceDTO()
                {
                    Id = reader.GetInt32("id"),
                    ConsumerId = reader.GetInt32("consumerId"),
                    OfferedServiceId = reader.GetInt32("offeredServiceId"),
                    DateOfPurchase = reader.GetDateTime("dateOfPurchase")
                });
            }

            return usedServiceDTOs;
        }


        public IEnumerable<IUsedServiceDTO> GetAllUsedServices()
        {
            var usedServiceDTOs = new List<IUsedServiceDTO>();
            const string commandText = "SELECT * FROM usedServices";

            using var connection = MySqlFactory.CreateConnection();
            connection.Open();
            var command = MySqlFactory.CreateCommand(commandText, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                usedServiceDTOs.Add(new UsedServiceDTO()
                {
                    Id = reader.GetInt32("id"),
                    ConsumerId = reader.GetInt32("consumerId"),
                    OfferedServiceId = reader.GetInt32("offeredServiceId"),
                    DateOfPurchase = reader.GetDateTime("dateOfPurchase")
                });
            }

            return usedServiceDTOs;
        }

        public IEnumerable<IUsedServiceDTO> GetUsedServicesByConsumerId(int consumerId)
        {
            string whereClause = "usedservices.consumerId = '" + consumerId + "'";
            return GetUsedServicesWhere(whereClause);
        }

        public IEnumerable<IUsedServiceDTO> GetUsedServicesByOfferdServiceId(int offeredServiceId)
        {
            string whereClause = "usedservices.offeredServiceId = '" + offeredServiceId + "'";
            return GetUsedServicesWhere(whereClause);
        }

        public IUsedServiceDTO GetLatestUsedServiceByConsumerId(int providerId)
        {
            var usedServiceDTO = new UsedServiceDTO();
            string query = "SELECT * FROM usedservices order by usedservices.dateOfPurchase desc limit 1; ";

            using var connection = MySqlFactory.CreateConnection();
            connection.Open();
            var command = MySqlFactory.CreateCommand(query, connection);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                usedServiceDTO.Id = reader.GetInt32("id");
                usedServiceDTO.ConsumerId = reader.GetInt32("consumerId");
                usedServiceDTO.OfferedServiceId = reader.GetInt32("offeredServiceId");
                usedServiceDTO.DateOfPurchase = reader.GetDateTime("dateOfPurchase");
            }

            return usedServiceDTO;
        }

        #endregion
    }
}
