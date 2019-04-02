using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Extend_Advertisement
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string AdvertisementID { get; set; }
        public string Name { get; set; }
        public string Remark { get; set; }
        public int? Sort { get; set; }
        public bool? State { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
