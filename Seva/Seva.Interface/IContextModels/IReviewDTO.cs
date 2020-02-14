using System;
using System.Collections.Generic;
using System.Text;

namespace Seva.Interface.IContextModels
{
    public interface IReviewDTO
    {
        int Id { get; set; }
        string Title { get; set; }
        string Text { get; set; }
        int Rating { get; set; }
        DateTime DateOfPost { get; set; }
        int WriterId { get; set; }
        int UsedServiceId { get; set; }
    }
}
