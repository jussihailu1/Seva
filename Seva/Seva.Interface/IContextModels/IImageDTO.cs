using System;
using System.Collections.Generic;
using System.Text;

namespace Seva.Interface.IContextModels
{
    public interface IImageDTO
    {
        int Id { get; set; }
        int OfferedServiceId { get; set; }
        byte[] Image { get; set; }
        int IsHeadImage { get; set; }
    }
}
