using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.EF.DbStoredProcedure
{
    public class SP_Search_Sys_Operator
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string StaffID { get; set; }
        public string StaffName { get; set; }
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public byte? Sex { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Remark { get; set; }
        public bool? State { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
