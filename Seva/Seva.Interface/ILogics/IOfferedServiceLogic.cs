using System;
using System.Collections.Generic;
using System.Text;
using Seva.Interface.ILogicModels;

namespace Seva.Interface.ILogics
{
    public interface IOfferedServiceLogic
    {
        void CreateService(string name, string categoryName, int deliveryTimeDays, int deliveryTimeHours, string description, decimal price, int providerId);
        void EditOfferedService(int offeredServiceId, string name, string category, int deliveryTimeDays, int deliveryTimeHours, string description, decimal price);
        void PurchaseOfferedService(int offeredServiceId, int conusmerId);
        void SetStatus(int offeredServiceId, int oldStatus);
        decimal CalcAverageRating(int offeredServiceId);

        #region Gets

        int GetLatestOfferedServiceIdByProviderId(int providerId);
        IEnumerable<IOfferedService> GetAllOfferedServices();
        IEnumerable<IOfferedService> GetOfferedServicesByUserId(int userId);
        IOfferedService GetOfferedServiceById(int offeredServiceId);

        #endregion
    }
}
