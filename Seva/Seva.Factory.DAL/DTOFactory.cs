using System;
using System.Collections.Generic;
using System.Text;
using Seva.DAL.Models;
using Seva.Interface.IContextModels;

namespace Seva.Factory.DAL
{
    public class DTOFactory
    {
        public static IUserDTO CreateUserDTO(string firstName, string lastName, string email, string password, int roleId)
        {
            return new UserDTO()
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                RoleId = roleId
            };
        }

        public static IUserDTO CreateUserDTOForUpdate(int id, string firstName, string lastName, string email, string password, string description)
        {
            return new UserDTO()
            {
                Id = id,
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = password,
                Description = description
            };
        }

        public static IOfferedServiceDTO CreateOfferedServiceDTO(string name, int categoryId, int deliveryTimeDays, int deliveryTimeHours, string description, decimal price, bool isDeleted, int providerId, int statusId, DateTime dateOfPost)
        {
            return new OfferedServiceDTO()
            {
                Id = 0,
                Name = name,
                CategoryId = categoryId,
                Description = description,
                ProviderId = providerId,
                StatusId = statusId,
                DeliveryTimeDays = deliveryTimeDays,
                DeliveryTimeHours = deliveryTimeHours,
                DateOfPost = dateOfPost,
                Price = price,
                IsDeleted = isDeleted
            };
        }

        public static IOfferedServiceDTO CreateOfferedServiceDTOForUpdate(int offeredServiceId, string name, int categoryId, int deliveryTimeDays, int deliveryTimeHours, string description, decimal price)
        {
            return new OfferedServiceDTO()
            {
                Id = offeredServiceId,
                Name = name,
                CategoryId = categoryId,
                Description = description,
                DeliveryTimeDays = deliveryTimeDays,
                DeliveryTimeHours = deliveryTimeHours,
                Price = price
            };
        }

        public static IImageDTO CreateImageDTO(byte[] image, int isHeadImage, int offeredServiceId)
        {
            return new ImageDTO()
            {
                 Image = image, 
                 IsHeadImage = isHeadImage,
                 OfferedServiceId = offeredServiceId
            };
        }

        public static IUsedServiceDTO CreateUsedServiceDTO(int consumerId, DateTime dateOfPurchase, int offeredServiceId)
        {
            return new UsedServiceDTO()
            {
                ConsumerId = consumerId,
                DateOfPurchase = dateOfPurchase,
                OfferedServiceId = offeredServiceId
            };
        }

        public static IReviewDTO CreateReviewDTO(string text, string title, int rating, int writerId, int usedServiceId, DateTime dateOfPost)
        {
            return new ReviewDTO()
            {
                Title = title, 
                Text = text, 
                Rating = rating,
                DateOfPost = dateOfPost,
                UsedServiceId = usedServiceId,
                WriterId = writerId
            };
        }
    }
}
