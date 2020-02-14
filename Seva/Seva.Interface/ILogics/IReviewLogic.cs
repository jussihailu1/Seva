using System;
using System.Collections.Generic;
using System.Text;
using Seva.Interface.ILogicModels;

namespace Seva.Interface.ILogics
{
    public interface IReviewLogic
    {
        void WriteReview(int writerId, string title, string text, int rating);

        #region Gets

        IReview GetReviewById(int reviewId);
        IEnumerable<IReview> GetReviewsByOfferedServiceId(int offeredServiceId);
        IEnumerable<IReview> GetReviewsByWriterId(int writerId);

        #endregion
    }
}
