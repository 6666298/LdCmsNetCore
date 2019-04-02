using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Extend_Link
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string LinkID { get; set; }
        public string GroupID { get; set; }
        public string GroupName { get; set; }
        public byte? TypeID { get; set; }
        public string TypeName { get; set; }
        public string Name { get; set; }
        public string Logo { get; set; }
        public string Url { get; set; }
        public int? Sort { get; set; }
        public bool? State { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
