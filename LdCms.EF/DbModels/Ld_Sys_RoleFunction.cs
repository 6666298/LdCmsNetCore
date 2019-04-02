using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Sys_RoleFunction
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string RoleID { get; set; }
        public string FunctionID { get; set; }
    }
}
