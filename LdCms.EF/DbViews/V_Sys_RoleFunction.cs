using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.EF.DbViews
{
    public class V_Sys_RoleFunction
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public string FunctionID { get; set; }
        public string FunctionName { get; set; }
        public int? RankID { get; set; }
        public bool? State { get; set; }
        public bool? IsDel { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
