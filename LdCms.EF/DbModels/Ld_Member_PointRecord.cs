using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Member_PointRecord
    {
        public long ID { get; set; }
        public int? SystemID { get; set; }
        public string CompanyID { get; set; }
        public string MemberID { get; set; }
        public byte? ClassID { get; set; }
        public string ClassName { get; set; }
        public byte? TypeID { get; set; }
        public string TypeName { get; set; }
        public int? Points { get; set; }
        public string Body { get; set; }
        public string Account { get; set; }
        public string NickName { get; set; }
        public string ipAddress { get; set; }
        public string Remark { get; set; }
        public bool? State { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
