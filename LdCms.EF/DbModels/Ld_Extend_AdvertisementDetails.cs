using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Extend_AdvertisementDetails
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string DetailsID { get; set; }
        public string AdvertisementID { get; set; }
        public string Title { get; set; }
        public string Remark { get; set; }
        public string MediaID { get; set; }
        public string MediaType { get; set; }
        public string MediaPath { get; set; }
        public string Url { get; set; }
        public int? Sort { get; set; }
        public bool? State { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
