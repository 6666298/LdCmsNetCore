using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Log_WebApiAccessRecord
    {
        public long ID { get; set; }
        public int? SystemID { get; set; }
        public string CompanyID { get; set; }
        public string AppID { get; set; }
        public byte? ClassID { get; set; }
        public string ClassName { get; set; }
        public string Method { get; set; }
        public string Url { get; set; }
        public string Parameter { get; set; }
        public string Version { get; set; }
        public string ControllerName { get; set; }
        public string ActionName { get; set; }
        public string ParameterName { get; set; }
        public string Result { get; set; }
        public string IpAddress { get; set; }
        public bool? State { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int? TotalMillisecond { get; set; }
    }
}
