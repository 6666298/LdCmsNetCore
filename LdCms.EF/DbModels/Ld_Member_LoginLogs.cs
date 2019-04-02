using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Member_LoginLogs
    {
        public long ID { get; set; }
        public int? SystemID { get; set; }
        public string CompanyID { get; set; }
        public string MemberID { get; set; }
        public byte? ClientID { get; set; }
        public string Account { get; set; }
        public string NickName { get; set; }
        public string IpAddress { get; set; }
        public bool? IsResult { get; set; }
        public string Description { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
