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
    public class ReviewLogic : IReviewLogic
    {
        private readonly IReviewContext _reviewContext;
        private readonly IUsedServiceLogic _usedServiceLogic;

        public ReviewLogic()
        {
            _reviewContext = ContextFactory.CreateReviewContext();
            _usedServiceLogic = new UsedServiceLogic();
        }

        public void WriteReview(int writerId, string title, string text, int rating)
        {
            var usedService = _usedServiceLogic.GetLatestUsedServiceByUserId(writerId);
            var dateOfPost = DateTime.Now;
            var reviewDTO = DTOFactory.CreateReviewDTO(text, title, rating, writerId, usedService.Id, dateOfPost);

            _reviewContext.AddReview(reviewDTO);
        }

        private IReview ConvertReview(IReviewDTO reviewDTO)
        {
            return new Review()
            {
                Id = reviewDTO.Id,
                DateOfPost = reviewDTO.DateOfPost,
                Text = reviewDTO.Text,
                Title = reviewDTO.Title,
                WriterId = reviewDTO.WriterId,
                Rating = reviewDTO.Rating,
                UsedServiceId = reviewDTO.UsedServiceId
            };
        }

        #region Gets

        public IReview GetReviewById(int reviewId)
        {
            var reviewDTO = _reviewContext.GetReviewById(reviewId);
            var reviewToReturn = ConvertReview(reviewDTO);

            return reviewToReturn;
        }

        public IEnumerable<IReview> GetReviewsByOfferedServiceId(int offeredServiceId)
        {
            var usedServiceDTOs = _usedServiceLogic.GetUsedServicesByOfferdServiceId(offeredServiceId);
            var usedServices = usedServiceDTOs.Select(usedServiceDTO => usedServiceDTO);

            var reviewsToReturn = from usedService in usedServices
                                  from reviewDTO in _reviewContext.GetReviewsByUsedServiceId(usedService.Id)
                                  select ConvertReview(reviewDTO);

            return reviewsToReturn;
        }

        public IEnumerable<IReview> GetReviewsByWriterId(int writerId)
        {
            var reviewDTOs = _reviewContext.GetReviewsByWriterId(writerId);
            var reviewsToReturn = reviewDTOs.Select(ConvertReview);

            return reviewsToReturn;
        }

        #endregion
    }
}
