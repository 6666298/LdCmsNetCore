using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace LdCms.Web.Controllers.MVC.Extend
{
    using LdCms.EF.DbModels;
    using LdCms.IBLL.Extend;
    using LdCms.Common.Extension;
    using LdCms.Common.Utility;
    using LdCms.Common.Web;
    using LdCms.Web.Models;
    using LdCms.Web.Services;

    [AdminAuthorize(Roles = "Admins")]
    public class ExtendAdvertisementController : BaseController
    {
        private readonly IBaseManager BaseManager;
        private readonly IAdvertisementService AdvertisementService;
        public ExtendAdvertisementController(IBaseManager BaseManager, IAdvertisementService AdvertisementService) : base(BaseManager)
        {
            this.BaseManager = BaseManager;
            this.AdvertisementService = AdvertisementService;
        }
        public override IActionResult Index()
        {
            return View();
        }

        public JsonResult Save()
        {
            try
            {
                var lists = new List<Ld_Extend_AdvertisementDetails>()
                {
                    new Ld_Extend_AdvertisementDetails(){ Title="A1" },
                    new Ld_Extend_AdvertisementDetails(){ Title="A2" },
                    new Ld_Extend_AdvertisementDetails(){ Title="A3" },
                    new Ld_Extend_AdvertisementDetails(){ Title="A4" },
                    new Ld_Extend_AdvertisementDetails(){ Title="A5" },
                    new Ld_Extend_AdvertisementDetails(){ Title="A6" },
                    new Ld_Extend_AdvertisementDetails(){ Title="A7" },
                    new Ld_Extend_AdvertisementDetails(){ Title="A8" },
                    new Ld_Extend_AdvertisementDetails(){ Title="A9" },
                    new Ld_Extend_AdvertisementDetails(){ Title="A0" }
                };
                var s = AdvertisementService.SaveAdvertisement(new Ld_Extend_Advertisement()
                {
                    SystemID = SystemID,
                    CompanyID = CompanyID,
                    Name = "广告名称",
                    Remark = "备注"
                }, lists);
                return Success("ok");
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }




    }
}