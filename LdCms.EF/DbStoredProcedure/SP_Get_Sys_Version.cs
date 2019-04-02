using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.EF.DbStoredProcedure
{
    public partial class SP_Get_Sys_Version
    {
        public string VersionID { get; set; }
        public string VersionName { get; set; }
        public decimal? MarketPrice { get; set; }
        public decimal? DealerPrice { get; set; }
        public int? DepartmentTotalQuantity { get; set; }
        public int? StaffTotalQuantity { get; set; }
        public int? StoreTotalQuantity { get; set; }
        public int? WarehouseTotalQuantity { get; set; }
        public string Description { get; set; }
        public bool? State { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
