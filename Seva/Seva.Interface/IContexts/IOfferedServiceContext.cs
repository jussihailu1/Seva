using System;
using System.Collections.Generic;
using System.Text;
using Seva.Interface.IContextModels;

namespace Seva.Interface.IContexts
{
    public interface IOfferedServiceContext
    {
        void Add(IOfferedServiceDTO offeredServiceDTO);
        void Update(IOfferedServiceDTO offeredServiceDTO);
        void SetStatus(int offeredServiceId, int status);
        void UpdateRating(int offeredServiceId, decimal rating);

        #region Gets

        IEnumerable<IOfferedServiceDTO> GetAllOfferedServices();
        IEnumerable<IOfferedServiceDTO> GetOfferedServicesByUserId(int userId);
        IEnumerable<IOfferedServiceDTO> GetOfferedServicesByCategoryId(int categoryId);
        IOfferedServiceDTO GetOfferedServiceById(int offeredServiceId);
        IOfferedServiceDTO GetLatestOfferedServiceByProviderId(int providerId);

        #endregion
    }
}
