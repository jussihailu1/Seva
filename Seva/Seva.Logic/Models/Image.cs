using System;
using System.Collections.Generic;
using System.Text;
using Seva.Interface.ILogicModels;

namespace Seva.Logic.Models
{
    public class Image : IImage
    {
        public int Id { get; set; }
        public int OfferedServiceId { get; set; }
        public string ImageAsString { get; set; }
        public bool IsHeadImage { get; set; }

        public override string ToString()
        {
            return ImageAsString;
        }
    }
}
