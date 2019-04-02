using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LdCms.BLL.Extend
{
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.IBLL.Extend;
    using LdCms.IDAL.Extend;
    using LdCms.Common.Json;
    using LdCms.Common.Security;
    using LdCms.Common.Extension;
    /// <summary>
    /// 
    /// </summary>
    public partial class AdvertisementDetailsService : BaseService<Ld_Extend_AdvertisementDetails>, IAdvertisementDetailsService
    {
        private readonly IAdvertisementDetailsDAL AdvertisementGroupDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public AdvertisementDetailsService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IAdvertisementDetailsDAL AdvertisementGroupDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.AdvertisementGroupDAL = AdvertisementGroupDAL;
            this.Dal = AdvertisementGroupDAL;
        }
        public override void SetDal()
        {
            Dal = AdvertisementGroupDAL;
        }

        public bool SaveAdvertisementDetails(Ld_Extend_AdvertisementDetails entity)
        {
            try
            {
                var advertisementDetails = PrimaryKeyHelper.PrimaryKeyType.ExtendAdvertisementDetails;
                var version = PrimaryKeyHelper.PrimaryKeyLen.V1;
                string advertisementDetailsId = PrimaryKeyHelper.MakePrimaryKey(advertisementDetails, version);
                entity.DetailsID = advertisementDetailsId;
                entity.Sort = entity.Sort.ToInt();
                entity.State = entity.State.ToBool();
                entity.CreateDate = DateTime.Now;
                return Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateAdvertisementDetails(Ld_Extend_AdvertisementDetails entity)
        {
            try
            {
                entity.Sort = entity.Sort.ToInt();
                entity.State = entity.State.ToBool();
                entity.CreateDate = DateTime.Now;
                return Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteAdvertisementDetails(int systemId, string companyId, string detailsId)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Extend_AdvertisementDetails>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.DetailsID == detailsId);
                if (!IsExists(expression))
                    throw new Exception("主建ID无效！");
                return Delete(expression);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Ld_Extend_AdvertisementDetails GetAdvertisementDetails(int systemId, string companyId, string detailsId)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Extend_AdvertisementDetails>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.DetailsID == detailsId);
                if (!IsExists(expression))
                    throw new Exception("主建ID无效！");
                return Find(expression);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Extend_AdvertisementDetails> GetAdvertisementDetailsByAdvertisementId(int systemId, string companyId, string advertisementId)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Extend_AdvertisementDetails>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.AdvertisementID == advertisementId);
                return FindList(expression, m => m.Sort.Value, true).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
