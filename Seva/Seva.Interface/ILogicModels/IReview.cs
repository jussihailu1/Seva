using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;

namespace Seva.Interface.ILogicModels
{
    public interface IReview
    {
        int Id { get; set; }
        int WriterId { get; set; }
        int UsedServiceId { get; set; }
        string Title { get; set; }
        string Text { get; set; }
        int Rating { get; set; }
        DateTime DateOfPost { get; set; }
    }
}
