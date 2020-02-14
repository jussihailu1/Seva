using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;
using Seva.Interface.ILogicModels;

namespace Seva.Interface.ILogics
{
    public interface IImageLogic
    {
        void UploadImages(List<IFormFile> images, int currentUserId);

        #region Gets

        IEnumerable<IImage> GetImagesByOfferedServiceId(int offeredServiceId);

        #endregion
    }
}
