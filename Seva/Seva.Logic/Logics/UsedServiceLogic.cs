using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Seva.Factory.DAL;
using Seva.Interface.IContextModels;
using Seva.Interface.IContexts;
using Seva.Interface.ILogicModels;
using Seva.Interface.ILogics;
using Seva.Logic.Models;

namespace Seva.Logic.Logics
{
    public class UsedServiceLogic : IUsedServiceLogic
    {
        private readonly IUsedServiceContext _usedServiceContext;

        public UsedServiceLogic()
        {
            _usedServiceContext = ContextFactory.CreateUsedServiceContext();
        }

        public void CreateUsedService(int offeredServiceId, int consumerId)
        {
            var dateOfPurchase = DateTime.Now;
            var usedServiceDTO = DTOFactory.CreateUsedServiceDTO(consumerId, dateOfPurchase, offeredServiceId);

            _usedServiceContext.AddUsedService(usedServiceDTO);
        }

        private IUsedService ConvertUsedService(IUsedServiceDTO usedServiceDTO)
        {
            return new UsedService()
            {
                Id = usedServiceDTO.Id,
                ConsumerId = usedServiceDTO.ConsumerId,
                OfferedServiceId = usedServiceDTO.OfferedServiceId,
                DateOfPurchase = usedServiceDTO.DateOfPurchase
            };
        }

        #region Gets

        public IUsedService GetUsedServiceById(int usedServiceId)
        {
            var usedServiceDTO = _usedServiceContext.GetUsedServiceById(usedServiceId);
            var usedServiceToReturn = ConvertUsedService(usedServiceDTO);

            return usedServiceToReturn;
        }

        public IEnumerable<IUsedService> GetAllUsedServices()
        {
            var usedServiceDTOs = _usedServiceContext.GetAllUsedServices();
            var usedServicesToReturn = usedServiceDTOs.Select(ConvertUsedService);

            return usedServicesToReturn;
        }

        public IEnumerable<IUsedService> GetUsedServicesByConsumerId(int consumerId)
        {
            var usedServiceDTOs = _usedServiceContext.GetUsedServicesByConsumerId(consumerId);
            var usedServicesToReturn = usedServiceDTOs.Select(ConvertUsedService).ToList();

            return usedServicesToReturn;
        }

        public IEnumerable<IUsedService> GetUsedServicesByOfferdServiceId(int offeredServiceId)
        {
            var usedServiceDTOs = _usedServiceContext.GetUsedServicesByOfferdServiceId(offeredServiceId);
            var usedServicesToReturn = usedServiceDTOs.Select(ConvertUsedService).ToList();

            return usedServicesToReturn;
        }

        public IUsedService GetLatestUsedServiceByUserId(int userId)
        {
            var usedServiceDTO = _usedServiceContext.GetLatestUsedServiceByConsumerId(userId);
            var usedServiceToReturn = ConvertUsedService(usedServiceDTO);

            return usedServiceToReturn;
        }

        #endregion
    }
}
