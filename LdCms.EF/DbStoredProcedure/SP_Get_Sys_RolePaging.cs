using System;

namespace LdCms.EF.DbStoredProcedure
{
    public class SP_Get_Sys_RolePaging
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public string Remark { get; set; }
        public bool? State { get; set; }
        public bool? IsDel { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
