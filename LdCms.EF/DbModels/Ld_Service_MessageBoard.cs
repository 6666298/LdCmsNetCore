using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Service_MessageBoard
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string MessageID { get; set; }
        public string CompanyName { get; set; }
        public string Name { get; set; }
        public string Tel { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string ImgSrc { get; set; }
        public string Content { get; set; }
        public string IpAddress { get; set; }
        public string Reply { get; set; }
        public string ReplyStaffId { get; set; }
        public string ReplyStaffName { get; set; }
        public DateTime? ReplyTime { get; set; }
        public bool? IsTop { get; set; }
        public bool? State { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
