using System;
using System.Collections.Generic;
using System.Text;
using Seva.Interface.IContextModels;

namespace Seva.DAL.Models
{
    public class ImageDTO : IImageDTO
    {
        public int Id { get; set; }
        public int OfferedServiceId { get; set; }
        public byte[] Image { get; set; }
        public int IsHeadImage { get; set; }
    }
}
