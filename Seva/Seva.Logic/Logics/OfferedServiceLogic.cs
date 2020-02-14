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
    public class OfferedServiceLogic : IOfferedServiceLogic
    {
        private readonly IOfferedServiceContext _offeredServiceContext;
        private readonly IUsedServiceLogic _usedServiceLogic;
        private readonly ICategoryLogic _categoryLogic;
        private readonly IReviewLogic _reviewLogic;

        public OfferedServiceLogic()
        {
            _offeredServiceContext = ContextFactory.CreateOfferedServiceContext();
            _usedServiceLogic = new UsedServiceLogic();
            _categoryLogic = new CategoryLogic();
            _reviewLogic = new ReviewLogic();
        }

        public void CreateService(string name, string categoryName, int deliveryTimeDays, int deliveryTimeHours, string description, decimal price, int providerId)
        {
            int categoryId = _categoryLogic.GetCategoryByName(categoryName).Id;
            const bool isDeleted = false;
            const int statusId = 0;
            DateTime dateOfPost = DateTime.Now;

            var offeredService = DTOFactory.CreateOfferedServiceDTO(name, categoryId, deliveryTimeDays, deliveryTimeHours, description, price, isDeleted, providerId, statusId, dateOfPost);

            _offeredServiceContext.Add(offeredService);
        }

        public void EditOfferedService(int offeredServiceId, string name, string categoryName, int deliveryTimeDays, int deliveryTimeHours, string description, decimal price)
        {
            int categoryId = _categoryLogic.GetCategoryByName(categoryName).Id;
            var offeredService = DTOFactory.CreateOfferedServiceDTOForUpdate(offeredServiceId, name, categoryId, deliveryTimeDays, deliveryTimeHours, description, price);

            _offeredServiceContext.Update(offeredService);
        }

        public void PurchaseOfferedService(int offeredServiceId, int consumerId) => _usedServiceLogic.CreateUsedService(offeredServiceId, consumerId);

        public void SetStatus(int offeredServiceId, int oldStatus)
        {
            var newStatus = oldStatus == 0 ? 1 : 0;

            _offeredServiceContext.SetStatus(offeredServiceId, newStatus);
        }

        public decimal CalcAverageRating(int offeredServiceId)
        {
            var reviews = _reviewLogic.GetReviewsByOfferedServiceId(offeredServiceId);
            int sum = reviews.Sum(review => review.Rating);
            int count = reviews.Count();
            if (count == 0) return 0;
            return sum / (decimal) count;
        }

        private OfferedService ConvertOfferedService(IOfferedServiceDTO offeredServiceDTO)
        {
            return new OfferedService()
            {
                Id = offeredServiceDTO.Id,
                Name = offeredServiceDTO.Name,
                Category = _categoryLogic.GetCategoryById(offeredServiceDTO.CategoryId).Name,
                Price = offeredServiceDTO.Price,
                ProviderId = offeredServiceDTO.ProviderId,
                Description = offeredServiceDTO.Description,
                DeliveryTimeDays = offeredServiceDTO.DeliveryTimeDays,
                DeliveryTimeHours = offeredServiceDTO.DeliveryTimeHours,
                DateOfPost = offeredServiceDTO.DateOfPost,
                AverageRating = CalcAverageRating(offeredServiceDTO.Id),
                Status = offeredServiceDTO.StatusId,
                IsDeleted = offeredServiceDTO.IsDeleted
            };
        }

        #region Gets

        public IOfferedService GetOfferedServiceById(int offeredServiceId)
        {
            var offeredServiceDTO = _offeredServiceContext.GetOfferedServiceById(offeredServiceId);
            var offeredServiceToReturn = ConvertOfferedService(offeredServiceDTO);

            return offeredServiceToReturn;
        }

        public int GetLatestOfferedServiceIdByProviderId(int providerId)
        {
            var offeredServiceDTO = _offeredServiceContext.GetLatestOfferedServiceByProviderId(providerId);
            var offeredServiceToReturn = ConvertOfferedService(offeredServiceDTO);

            return offeredServiceToReturn.Id;
        }

        public IEnumerable<IOfferedService> GetAllOfferedServices()
        {
            var offeredServiceDTOs = _offeredServiceContext.GetAllOfferedServices();
            var offeredServicesToReturn = offeredServiceDTOs.Select(ConvertOfferedService);

            return offeredServicesToReturn;
        }

        public IEnumerable<IOfferedService> GetOfferedServicesByUserId(int userId)
        {
            var offeredServiceDTOs = _offeredServiceContext.GetOfferedServicesByUserId(userId);
            var offeredServicesToReturn = offeredServiceDTOs.Select(ConvertOfferedService);

            return offeredServicesToReturn;
        }

        #endregion
    }
}
