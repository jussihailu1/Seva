using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Seva.Interface.ILogicModels
{
    public interface IImage
    {
        int Id { get; set; }
        int OfferedServiceId { get; set; }
        string ImageAsString { get; set; }
        bool IsHeadImage { get; set; }
    }
}
