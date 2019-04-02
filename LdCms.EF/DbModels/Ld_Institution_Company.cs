using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Institution_Company
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string DealerID { get; set; }
        public byte? ClassID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public string LogoImages { get; set; }
        public string NickName { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string RegisterIpAddress { get; set; }
        public DateTime? RegisterTime { get; set; }
        public string Version { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int? LoginTotalNumber { get; set; }
        public bool? State { get; set; }
        public bool? IsDal { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
