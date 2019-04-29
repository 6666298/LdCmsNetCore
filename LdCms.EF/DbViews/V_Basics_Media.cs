using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.EF.DbViews
{
    public class V_Basics_Media
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string MediaID { get; set; }
        public string AppID { get; set; }
        public string MemberID { get; set; }
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
