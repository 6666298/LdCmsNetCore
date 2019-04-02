using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LdCms.BLL.Institution
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbViews;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.EF.DbEntitiesContext;
    using LdCms.IBLL.Institution;
    using LdCms.IDAL.Institution;
    using LdCms.Common.Json;
    public partial class PositionService:BaseService<Ld_Institution_Position>,IPositionService
    {
        private readonly IPositionDAL PositionDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public PositionService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IPositionDAL PositionDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.PositionDAL = PositionDAL;
            this.Dal = PositionDAL;
        }
        public override void SetDal()
        {
            Dal = PositionDAL;
        }

        public bool SavePositionPro(int systemId, string companyId, string positionId, string positionName, string description, int sort, bool state)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Add_Institution_Position(systemId, companyId, positionId, positionName, description, sort, state, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdatePositionPro(int systemId, string companyId, string positionId, string positionName, string description, int sort, bool state)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Update_Institution_Position(systemId, companyId, positionId, positionName, description, sort, state, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdatePositionStatePro(int systemId, string companyId, string positionId, bool state)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Update_Institution_PositionState(systemId, companyId, positionId, state, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeletePositionPro(int systemId, string companyId, string positionId)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Delete_Institution_Position(systemId, companyId, positionId, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Institution_Position GetPositionPro(int systemId, string companyId, string positionId)
        {
            try
            {
                if (systemId == 0)
                    throw new Exception("系统编号不能为0！");
                if (string.IsNullOrWhiteSpace(companyId))
                    throw new Exception("公司编号不能为空！");
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Get_Institution_Position(systemId, companyId, positionId, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                if (result == null)
                    return null;
                else
                    return result.ToObject<List<Ld_Institution_Position>>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Institution_Position> GetPositionByStatePro(int systemId, string companyId, string state)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Get_Institution_PositionByState(systemId, companyId, state, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                if (result == null)
                    return null;
                else
                    return result.ToObject<List<Ld_Institution_Position>>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Institution_Position> GetPositionPagingPro(int systemId, string companyId, int pageId, int pageSize, out int rowCount)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Get_Institution_PositionPaging(systemId, companyId, pageId, pageSize, out errCode, out errMsg, out rowCount);
                if (errCode != 0)
                    throw new Exception(errMsg);
                if (result == null)
                    return null;
                else
                    return result.ToObject<List<Ld_Institution_Position>>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Institution_Position> SearchPositionPro(int systemId, string companyId, string startTime, string endTime, string keyword)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Search_Institution_Position(systemId, companyId, startTime, endTime, keyword, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                if (result == null)
                    return null;
                else
                    return result.ToObject<List<Ld_Institution_Position>>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
