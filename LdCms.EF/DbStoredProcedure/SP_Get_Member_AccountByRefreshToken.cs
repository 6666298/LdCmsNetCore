using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.EF.DbStoredProcedure
{
    public class SP_Get_Member_AccountByRefreshToken
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string MemberID { get; set; }
        public string RankID { get; set; }
        public string RankName { get; set; }
        public byte? ClassID { get; set; }
        public string ClassName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public string NickName { get; set; }
        public string HeadImageUrl { get; set; }
        public byte? Sex { get; set; }
        public string Phone { get; set; }
        public string Tel { get; set; }
        public string Email { get; set; }
        public string QQ { get; set; }
        public string Weixin { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Area { get; set; }
        public string Address { get; set; }
        public decimal? Balance { get; set; }
        public int? TotalPoints { get; set; }
        public decimal? TotalConsumption { get; set; }
        public decimal? TotalRecharge { get; set; }
        public string Remark { get; set; }
        public string RegisterIpAddress { get; set; }
        public DateTime? RegisterTime { get; set; }
        public string LastLoginIpAddress { get; set; }
        public DateTime? LastLoginTime { get; set; }
        public bool? State { get; set; }
        public bool? IsDel { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
