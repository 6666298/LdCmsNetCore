using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.EF.DbStoredProcedure
{
    public class SP_Get_Member_AccessToken
    {
        public string Token { get; set; }
        public int? SystemID { get; set; }
        public string CompanyID { get; set; }
        public string MemberID { get; set; }
        public string UuiD { get; set; }
        public int? ExpiresIn { get; set; }
        public string IpAddress { get; set; }
        public int? CreateTimestamp { get; set; }
        public bool? State { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
