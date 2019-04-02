using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Member_Rank
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string RankID { get; set; }
        public string RankName { get; set; }
        public int? MinPoints { get; set; }
        public int? MaxPoints { get; set; }
        public byte? Discount { get; set; }
        public byte? ShowPrice { get; set; }
        public byte? SpecialRank { get; set; }
        public string Remark { get; set; }
        public bool? State { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
