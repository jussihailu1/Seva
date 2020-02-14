using System;
using System.Collections.Generic;
using System.Text;
using Seva.Interface.IContextModels;

namespace Seva.Interface.IContexts
{
    public interface IImageContext
    {
        void AddImage(IImageDTO imageDTO);


        //Gets

        IEnumerable<IImageDTO> GetImagesByOfferedServiceId(int offeredServiceId);
    }
}
