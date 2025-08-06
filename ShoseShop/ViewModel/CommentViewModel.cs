using PagedList;
using ShoseShop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace ShoseShop.ViewModel
{
    public class CommentViewModel
    {
        public PagedList<BinhLuan> blPage { get; set; }
        public double overallStar { get; set; }
        public int totalReview { get; set; }
        public int fiveStar { get; set; }
        public int fourStar { get; set; }
        public int threeStar { get; set; }

        public int twoStar { get; set; }
        public int oneStar { get; set; }
    }
}