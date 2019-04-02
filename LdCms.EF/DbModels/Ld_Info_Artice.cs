using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Info_Artice
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string ArticeID { get; set; }
        public string Title { get; set; }
        public string TitleBrief { get; set; }
        public string ClassID { get; set; }
        public string ClassName { get; set; }
        public string TypeID { get; set; }
        public string TypeName { get; set; }
        public string ImgSrc { get; set; }
        public string ImgArray { get; set; }
        public string Author { get; set; }
        public string Source { get; set; }
        public string Tags { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public int? Hits { get; set; }
        public int? Sort { get; set; }
        public int? UpNum { get; set; }
        public int? DownNum { get; set; }
        public bool? AllowComment { get; set; }
        public DateTime? CommentStartTime { get; set; }
        public DateTime? CommentEndTime { get; set; }
        public bool? IsTop { get; set; }
        public bool? IsPush { get; set; }
        public bool? IsVip { get; set; }
        public bool? IsDraft { get; set; }
        public bool? State { get; set; }
        public bool? IsDel { get; set; }
        public string Account { get; set; }
        public string NickName { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
