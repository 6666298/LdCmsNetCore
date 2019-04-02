using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LdCms.Web.Models
{
    public class AccountModel
    {
        public string SessionID { get; set; }
        public string CompanyID { get; set; }
        public string StaffID { get; set; }
        public string StaffName { get; set; }
        public bool Online { get; set; }
        public string Roles { get; set; }
    }
}
