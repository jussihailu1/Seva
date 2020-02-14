using System;
using System.Collections.Generic;
using System.Text;
using Seva.Interface.IContextModels;

namespace Seva.Interface.IContexts
{
    public interface IUsedServiceContext
    {
        void AddUsedService(IUsedServiceDTO usedServiceDTO);


        #region Gets

        IUsedServiceDTO GetUsedServiceById(int usedServiceId);
        IEnumerable<IUsedServiceDTO> GetAllUsedServices();
        IEnumerable<IUsedServiceDTO> GetUsedServicesByConsumerId(int consumerId);
        IEnumerable<IUsedServiceDTO> GetUsedServicesByOfferdServiceId(int offeredServiceId);
        IUsedServiceDTO GetLatestUsedServiceByConsumerId(int userId);

        #endregion
    }
}
