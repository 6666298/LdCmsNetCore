using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.EF.DbStoredProcedure
{
    public class SP_Get_Sys_InterfaceAccessWhiteListByAccount
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string Account { get; set; }
        public string IpAddress { get; set; }
        public byte? ClassID { get; set; }
        public string ClassName { get; set; }
        public string Remark { get; set; }
        public bool? State { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
