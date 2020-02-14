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
    public class ReviewContext : IReviewContext
    {
        public void AddReview(IReviewDTO reviewDTO)
        {
            using var connection = MySqlFactory.CreateConnection();
            connection.Open();
            var command = MySqlFactory.CreateCommand("AddReview", connection);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.AddWithValue("@title", reviewDTO.Title);
            command.Parameters.AddWithValue("@text", reviewDTO.Text);
            command.Parameters.AddWithValue("@rating", reviewDTO.Rating);
            command.Parameters.AddWithValue("@dateOfPost", reviewDTO.DateOfPost);
            command.Parameters.AddWithValue("@writerId", reviewDTO.WriterId);
            command.Parameters.AddWithValue("@usedServiceId", reviewDTO.UsedServiceId);

            command.ExecuteNonQuery();
            connection.Close();
        }

        #region Gets

        public IReviewDTO GetReviewById(int reviewId)
        {
            var reviewDTO = new ReviewDTO();
            string query = "SELECT * FROM reviews WHERE reviews.id = " + reviewId + "";

            using var connection = MySqlFactory.CreateConnection();
            connection.Open();
            var command = MySqlFactory.CreateCommand(query, connection);

            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                reviewDTO.Id = reader.GetInt32("id");
                reviewDTO.Title = reader.GetString("title");
                reviewDTO.Text = reader.GetString("text");
                reviewDTO.Rating = reader.GetInt16("rating");
                reviewDTO.WriterId = reader.GetInt32("writerId");
                reviewDTO.UsedServiceId = reader.GetInt32("usedServiceId");
                reviewDTO.DateOfPost = reader.GetDateTime("dateOfPost");
            }

            return reviewDTO;
        }

        private IEnumerable<IReviewDTO> GetReviewsWhere(string whereClause)
        {
            var reviewDTOs = new List<IReviewDTO>();

            string commandText = "SELECT * FROM reviews WHERE " + whereClause;

            using var connection = MySqlFactory.CreateConnection();
            connection.Open();
            var command = MySqlFactory.CreateCommand(commandText, connection);
            var reader = command.ExecuteReader();

            while (reader.Read())
            {
                reviewDTOs.Add(new ReviewDTO()
                {
                    Id = reader.GetInt32("id"),
                    Title = reader.GetString("title"),
                    Text = reader.GetString("text"),
                    Rating = reader.GetInt16("rating"),
                    WriterId = reader.GetInt32("writerId"),
                    UsedServiceId = reader.GetInt32("usedServiceId"),
                    DateOfPost = reader.GetDateTime("dateOfPost")
                });
            }

            return reviewDTOs;
        }

        public IEnumerable<IReviewDTO> GetReviewsByUsedServiceId(int usedServiceId)
        {
            string whereClause = "reviews.usedServiceId = " + usedServiceId;
            return GetReviewsWhere(whereClause);
        }

        public IEnumerable<IReviewDTO> GetReviewsByWriterId(int writerId)
        {
            string whereClause = "reviews.writerId = " + writerId;
            return GetReviewsWhere(whereClause);
        }

        #endregion
    }
}
