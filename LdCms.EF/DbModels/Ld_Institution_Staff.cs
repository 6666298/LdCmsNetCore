using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Institution_Staff
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string StaffID { get; set; }
        public string StaffName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string NickName { get; set; }
        public string HeadImgUrl { get; set; }
        public string Name { get; set; }
        public byte? Sex { get; set; }
        public DateTime? BirthDate { get; set; }
        public string BirthPlace { get; set; }
        public string Identification { get; set; }
        public string Education { get; set; }
        public string Phone { get; set; }
        public string QQ { get; set; }
        public string Weixin { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public decimal? Wages { get; set; }
        public int? Probation { get; set; }
        public DateTime? StartWorkDate { get; set; }
        public DateTime? EndWorkDate { get; set; }
        public DateTime? SignContractDate { get; set; }
        public DateTime? ExpirationContractDate { get; set; }
        public string DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string PositionID { get; set; }
        public string PositionName { get; set; }
        public string StoreID { get; set; }
        public string StoreName { get; set; }
        public string WarehouseID { get; set; }
        public string WarehouseName { get; set; }
        public string Description { get; set; }
        public bool? IsInit { get; set; }
        public bool? State { get; set; }
        public bool? IsDel { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
