using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Institution_Position
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string PositionID { get; set; }
        public string PositionName { get; set; }
        public string Description { get; set; }
        public int? Sort { get; set; }
        public bool? State { get; set; }
        public bool? IsDel { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
