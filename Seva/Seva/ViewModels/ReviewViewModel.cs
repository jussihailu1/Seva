using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Seva.Interface.ILogicModels;

namespace Seva.ViewModels
{
    public class ReviewViewModel
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public DateTime DateOfPost { get; set; }
        public string ReviewWriterFullName { get; set; }

        public ReviewViewModel(IReview review, string reviewWriterFullName)
        {
            Title = review.Title;
            Text = review.Text;
            Rating = review.Rating;
            DateOfPost = review.DateOfPost;
            ReviewWriterFullName = reviewWriterFullName;
        }
        
        public ReviewViewModel(IReview review)
        {
            Title = review.Title;
            Text = review.Text;
            Rating = review.Rating;
            DateOfPost = review.DateOfPost;
        }
    }
}
