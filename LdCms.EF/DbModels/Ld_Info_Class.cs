using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Info_Class
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string ClassID { get; set; }
        public string ClassName { get; set; }
        public byte? ClassType { get; set; }
        public string ClassTypeName { get; set; }
        public byte? ClassRank { get; set; }
        public string ParentID { get; set; }
        public string ParentPath { get; set; }
        public int? OrderID { get; set; }
        public string OrderPath { get; set; }
        public string ImgSrc { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public bool? State { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
