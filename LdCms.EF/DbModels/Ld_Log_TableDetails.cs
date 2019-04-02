using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Log_TableDetails
    {
        public long ID { get; set; }
        public string TableID { get; set; }
        public string TableName { get; set; }
        public string ColumnName { get; set; }
        public string ColumnText { get; set; }
        public string ColumnDataType { get; set; }
        public bool? IsPrimaryKey { get; set; }
        public string Account { get; set; }
        public string NickName { get; set; }
        public string Remark { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
