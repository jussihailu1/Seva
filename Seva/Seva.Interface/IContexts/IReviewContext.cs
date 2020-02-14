using System;
using System.Collections.Generic;
using System.Text;
using Seva.Interface.IContextModels;

namespace Seva.Interface.IContexts
{
    public interface IReviewContext
    {
        void AddReview(IReviewDTO reviewDTO);

        #region Gets

        IReviewDTO GetReviewById(int reviewId);
        IEnumerable<IReviewDTO> GetReviewsByUsedServiceId(int usedServiceId);
        IEnumerable<IReviewDTO> GetReviewsByWriterId(int writerId);

        #endregion
    }
}
