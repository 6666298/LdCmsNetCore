using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Institution_Store
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string StoreID { get; set; }
        public string StoreName { get; set; }
        public string Logo { get; set; }
        public string Contacts { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? ProvinceID { get; set; }
        public int? CityID { get; set; }
        public int? AreaID { get; set; }
        public string Address { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public bool? Push { get; set; }
        public int? Sort { get; set; }
        public bool? State { get; set; }
        public bool? IsDel { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
