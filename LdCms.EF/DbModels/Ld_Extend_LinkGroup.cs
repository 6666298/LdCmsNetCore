using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Extend_LinkGroup
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string GroupID { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public int? Sort { get; set; }
        public bool? IsExternal { get; set; }
        public bool? State { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
