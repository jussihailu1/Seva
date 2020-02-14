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
    public class ImageContext : IImageContext
    {
        public void AddImage(IImageDTO imageDTO)
        {
            using var connection = MySqlFactory.CreateConnection();
            connection.Open();
            var cmd = MySqlFactory.CreateCommand("AddImage", connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@offeredServiceId", imageDTO.OfferedServiceId);
            cmd.Parameters.AddWithValue("@image", imageDTO.Image);
            cmd.Parameters.AddWithValue("@isHeadImage", imageDTO.IsHeadImage);

            cmd.ExecuteNonQuery();
            connection.Close();
        }

        #region Gets

        public IEnumerable<IImageDTO> GetImagesByOfferedServiceId(int offeredServiceId)
        {
            var imageDTOs = new List<IImageDTO>();
            string commandText = "SELECT * FROM images WHERE images.offeredServiceId = '" + offeredServiceId + "'";

            using var connection = MySqlFactory.CreateConnection();
            connection.Open();
            var command = MySqlFactory.CreateCommand(commandText, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                imageDTOs.Add(new ImageDTO()
                {
                    Id = reader.GetInt32("id"),
                    OfferedServiceId = reader.GetInt32("offeredServiceId"),
                    Image = (byte[])reader["image"],
                    IsHeadImage = reader.GetInt16("IsHeadImage")
                });
            }

            return imageDTOs;   
        }

        #endregion
    }
}
