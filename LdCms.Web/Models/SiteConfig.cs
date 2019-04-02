using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LdCms.Web.Models
{
    public class SiteConfig
    {
        public string Title { get; set; }
        public string Keywords { get; set; }
        public string Description { get; set; }
        public string CompanyID { get; set; }
        public string HomeUrl { get; set; }
        public string LoginUrl { get; set; }
    }
}
