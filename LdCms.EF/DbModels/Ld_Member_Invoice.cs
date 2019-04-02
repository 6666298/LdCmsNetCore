using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Member_Invoice
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string MemberID { get; set; }
        public string InvoiceID { get; set; }
        public byte? ClassID { get; set; }
        public string ClassName { get; set; }
        public string CompanyName { get; set; }
        public string TaxpayerIdentificationNumber { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
        public string BusinessLicense { get; set; }
        public string Remark { get; set; }
        public bool? State { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
