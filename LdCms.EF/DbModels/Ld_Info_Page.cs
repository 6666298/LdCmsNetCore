using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Info_Page
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string PageID { get; set; }
        public string Title { get; set; }
        public string ClassID { get; set; }
        public string ClassName { get; set; }
        public string ImgSrc { get; set; }
        public string ImgArray { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public int? Sort { get; set; }
        public bool? State { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
