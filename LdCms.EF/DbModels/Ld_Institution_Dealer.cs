using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Institution_Dealer
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string DealerID { get; set; }
        public string DealerName { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string ClassID { get; set; }
        public string ClassName { get; set; }
        public string RankID { get; set; }
        public string RankName { get; set; }
        public int? TotalCredit { get; set; }
        public string CompanyName { get; set; }
        public string Contacts { get; set; }
        public byte? Sex { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string QQ { get; set; }
        public string Weixin { get; set; }
        public string Address { get; set; }
        public string Logo { get; set; }
        public string Description { get; set; }
        public bool? State { get; set; }
        public bool? IsDel { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
