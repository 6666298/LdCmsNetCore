using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.EF.DbStoredProcedure
{
    public class SP_Get_Sys_Code
    {
        public int SystemID { get; set; }
        public string SystemName { get; set; }
        public string Description { get; set; }
        public bool? State { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
