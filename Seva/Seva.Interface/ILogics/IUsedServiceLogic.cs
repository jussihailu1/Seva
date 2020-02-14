using System;
using System.Collections.Generic;
using System.Text;
using Seva.Interface.ILogicModels;

namespace Seva.Interface.ILogics
{
    public interface IUsedServiceLogic
    {
        void CreateUsedService(int offeredServiceId, int consumerId);


        #region Gets

        IUsedService GetUsedServiceById(int usedServiceId);
        IUsedService GetLatestUsedServiceByUserId(int userId);
        IEnumerable<IUsedService> GetAllUsedServices();
        IEnumerable<IUsedService> GetUsedServicesByConsumerId(int consumerId);
        IEnumerable<IUsedService> GetUsedServicesByOfferdServiceId(int offeredServiceId);

        #endregion
    }
}
