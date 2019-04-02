using System;
using System.Collections.Generic;

namespace LdCms.EF.DbModels
{
    public partial class Ld_Sys_Config
    {
        public int SystemID { get; set; }
        public string CompanyID { get; set; }
        public string Title { get; set; }
        public string Keyword { get; set; }
        public string Description { get; set; }
        public string HomeUrl { get; set; }
        public string StyleSrc { get; set; }
        public string UploadRoot { get; set; }
        public string Copyright { get; set; }
        public string IcpNumber { get; set; }
        public string StatisticsCode { get; set; }
        public bool? IsLoginIpAddress { get; set; }
        public string LoginIpAddressWhiteList { get; set; }
        public int? MaxLoginFail { get; set; }
        public string Shielding { get; set; }
        public string EmailSendPattern { get; set; }
        public string EmailHost { get; set; }
        public int? EmailPort { get; set; }
        public string EmailName { get; set; }
        public string EmailPassword { get; set; }
        public string EmailAddress { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
