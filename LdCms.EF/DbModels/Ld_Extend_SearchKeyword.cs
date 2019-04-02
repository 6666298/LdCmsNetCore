using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Extend_SearchKeyword
    {
        public long ID { get; set; }
        public int? SystemID { get; set; }
        public string CompanyID { get; set; }
        public string MemberID { get; set; }
        public string Keyword { get; set; }
        public byte? ClientID { get; set; }
        public string ClientName { get; set; }
        public string IpAddress { get; set; }
        public int? Hits { get; set; }
        public bool? IsTop { get; set; }
        public bool? State { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
