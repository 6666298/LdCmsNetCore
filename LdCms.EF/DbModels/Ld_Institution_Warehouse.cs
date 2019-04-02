using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Institution_Warehouse
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string WarehouseID { get; set; }
        public string WarehouseName { get; set; }
        public string Contacts { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public bool? State { get; set; }
        public bool? IsDel { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
