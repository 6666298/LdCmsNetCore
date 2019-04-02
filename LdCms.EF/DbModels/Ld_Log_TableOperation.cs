using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Log_TableOperation
    {
        public long ID { get; set; }
        public string TableID { get; set; }
        public string TableName { get; set; }
        public byte? ClientID { get; set; }
        public string ClientName { get; set; }
        public byte? ClassID { get; set; }
        public string ClassName { get; set; }
        public string PrimaryKeyValue { get; set; }
        public string OldContent { get; set; }
        public string NewContent { get; set; }
        public string Url { get; set; }
        public string IpAdress { get; set; }
        public string Account { get; set; }
        public string NickName { get; set; }
        public string Remark { get; set; }
        public bool? State { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
