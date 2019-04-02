using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Basics_Media
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string MediaID { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public long? FileSize { get; set; }
        public string Url { get; set; }
        public string Src { get; set; }
        public string Remark { get; set; }
        public bool? State { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
