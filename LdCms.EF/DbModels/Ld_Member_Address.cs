using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Member_Address
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string MemberID { get; set; }
        public string AddressID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public string Tags { get; set; }
        public string Remark { get; set; }
        public bool? State { get; set; }
        public bool? IsDefault { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
