using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.EF.DbViews
{
    public class V_Sys_OperatorPermission
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string StaffID { get; set; }
        public string StaffName { get; set; }
        public string UserName { get; set; }
        public string NickName { get; set; }
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public bool? RoleState { get; set; }
        public string FunctionID { get; set; }
        public string FunctionName { get; set; }
        public bool? FunctionState { get; set; }
        public bool? State { get; set; }
    }
}
