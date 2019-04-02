using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Sys_Operator
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string StaffID { get; set; }
        public string Remark { get; set; }
        public bool? State { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
