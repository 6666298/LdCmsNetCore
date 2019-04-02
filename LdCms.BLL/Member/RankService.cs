using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LdCms.BLL.Member
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.IBLL.Member;
    using LdCms.IDAL.Member;
    using LdCms.Common.Json;

    /// <summary>
    /// 
    /// </summary>
    public partial class RankService : BaseService<Ld_Member_Rank>, IRankService
    {
        private readonly IRankDAL RankDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public RankService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IRankDAL RankDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.RankDAL = RankDAL;
            this.Dal = RankDAL;
        }
        public override void SetDal()
        {
            Dal = RankDAL;
        }

        public bool SaveRankPro(int systemId, string companyId, string rankName, int maxPoints, int discount, int showPrice, string remark, bool state)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Add_Member_Rank(systemId, companyId, rankName, maxPoints, discount, showPrice, remark, state, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateRankPro(int systemId, string companyId, string rankId, string rankName, int maxPoints, int discount, int showPrice, string remark, bool state)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Update_Member_Rank(systemId, companyId, rankId, rankName, maxPoints, discount, showPrice, remark, state, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateRankStatePro(int systemId, string companyId, string rankId, bool state)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Update_Member_RankState(systemId, companyId, rankId, state, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteRankPro(int systemId, string companyId, string rankId)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Delete_Member_Rank(systemId, companyId, rankId, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Member_Rank GetRankPro(int systemId, string companyId, string rankId)
        {
            try
            {
                if (systemId == 0)
                    throw new Exception("系统编号不能为0！");
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Get_Member_Rank(systemId, companyId, rankId, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return result == null ? null : result.ToObject<List<Ld_Member_Rank>>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Member_Rank> GetRankStatePro(int systemId, string companyId, string state)
        {
            try
            {
                if (systemId == 0)
                    throw new Exception("系统编号不能为0！");
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Get_Member_RankState(systemId, companyId, state, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return result == null ? null : result.ToObject<List<Ld_Member_Rank>>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Ld_Member_Rank> GetRankPagingPro(int systemId, string companyId, int pageId, int pageSize, out int rowCount)
        {
            try
            {
                if (systemId == 0)
                    throw new Exception("系统编号不能为0！");
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Get_Member_RankPaging(systemId, companyId, pageId, pageSize, out errCode, out errMsg, out rowCount);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return result == null ? null : result.ToObject<List<Ld_Member_Rank>>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Member_Rank> SearchRankPro(int systemId, string companyId, string startTime, string endTime, string keyword)
        {
            try
            {
                if (systemId == 0)
                    throw new Exception("系统编号不能为0！");
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Search_Member_Rank(systemId, companyId, startTime, endTime, keyword, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return result == null ? null : result.ToObject<List<Ld_Member_Rank>>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
