using System;
using System.Collections.Generic;
using System.Text;
using Seva.Interface.IContextModels;

namespace Seva.DAL.Models
{
    public class ReviewDTO : IReviewDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public DateTime DateOfPost { get; set; }
        public int WriterId { get; set; }
        public int UsedServiceId { get; set; }
    }
}
