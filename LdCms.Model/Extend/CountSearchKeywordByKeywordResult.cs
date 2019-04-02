using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.Model.Extend
{
    public class CountSearchKeywordByKeywordResult
    {
        public long ID { get; set; }
        public string Keyword { get; set; }
        public int Total { get; set; }
        public DateTime MinDate { get; set; }
        public DateTime MaxDate { get; set; }
    }
}
