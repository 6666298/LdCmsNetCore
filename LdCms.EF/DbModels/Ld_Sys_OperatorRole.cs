using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Sys_OperatorRole
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string StaffID { get; set; }
        public string RoleID { get; set; }
    }
}
