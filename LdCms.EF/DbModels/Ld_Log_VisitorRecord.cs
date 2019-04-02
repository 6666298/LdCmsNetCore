using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Log_VisitorRecord
    {
        public long ID { get; set; }
        public int? SystemID { get; set; }
        public string CompanyID { get; set; }
        public byte? ClientID { get; set; }
        public string IpAddress { get; set; }
        public string Host { get; set; }
        public string AbsoluteUri { get; set; }
        public string QueryString { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
