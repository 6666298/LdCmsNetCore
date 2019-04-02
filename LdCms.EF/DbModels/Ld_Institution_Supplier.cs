using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Institution_Supplier
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string SupplierID { get; set; }
        public byte? ClassID { get; set; }
        public string ClassName { get; set; }
        public string CompanyName { get; set; }
        public string Representative { get; set; }
        public string CompanyNature { get; set; }
        public string Business { get; set; }
        public string LicenseNumber { get; set; }
        public string LicenseImageUrl { get; set; }
        public string CodeCertificateNumber { get; set; }
        public string CodeCertificateImageUrl { get; set; }
        public string Address { get; set; }
        public string Contacts { get; set; }
        public string Tel { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Description { get; set; }
        public bool? State { get; set; }
        public bool? IsDel { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
