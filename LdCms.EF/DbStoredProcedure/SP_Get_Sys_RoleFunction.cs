using System;

namespace LdCms.EF.DbStoredProcedure
{
    public class SP_Get_Sys_RoleFunction
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string RoleID { get; set; }
        public string RoleName { get; set; }
        public string FunctionID { get; set; }
        public string FunctionName { get; set; }
        public int RankID { get; set; }
    }
}
