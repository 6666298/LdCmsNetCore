using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LdCms.BLL.Log
{
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.EF.DbModels;
    using LdCms.IBLL.Log;
    using LdCms.IDAL.Log;
    using LdCms.Common.Json;
    using LdCms.Common.Extension;
    /// <summary>
    /// 
    /// </summary>
    public partial class LoginRecordService:BaseService<Ld_Log_LoginRecord>,ILoginRecordService
    {
        private readonly ILoginRecordDAL LoginRecordDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public LoginRecordService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, ILoginRecordDAL LoginRecordDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.LoginRecordDAL = LoginRecordDAL;
            this.Dal = LoginRecordDAL;
        }
        public override void SetDal()
        {
            Dal = LoginRecordDAL;
        }

        public bool SaveLoginRecord(Ld_Log_LoginRecord entity)
        {
            try
            {
                entity.CreateDate = DateTime.Now;
                return Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteLoginRecord(int systemId, string companyId, long id)
        {
            try
            {
                return Delete(m => m.ID == id && m.SystemID == systemId && m.CompanyID == companyId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteLoginRecordAll(int systemId, string companyId)
        {
            try
            {
                DateTime time = DateTime.Now.AddDays(-3);
                var expression = ExtLinq.True<Ld_Log_LoginRecord>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.CreateDate.Value.Date <= time.Date);
                return Delete(expression);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Log_LoginRecord GetLoginRecord(long id)
        {
            try
            {
                return Find(id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Log_LoginRecord GetLoginRecord(int systemId,string companyId, long id)
        {
            try
            {
                return Find(m => m.SystemID == systemId && m.CompanyID == companyId && m.ID == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Log_LoginRecord> GetLoginRecordTop(int systemId, string companyId, int count)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Log_LoginRecord>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId);
                var lists = FindListTop(expression, m => m.ID, false, count);
                if (lists == null)
                    return null;
                else
                    return lists.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Log_LoginRecord> GetLoginRecordPaging(int systemId, string companyId, int pageId, int pageSize, out int totalRows)
        {
            try
            {
                
                var expression = ExtLinq.True<Ld_Log_LoginRecord>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId);
                totalRows = Count(expression);
                var lists = FindListPaging(expression, m => m.ID, false, pageId, pageSize);
                if (lists == null)
                    return null;
                else
                    return lists.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Log_LoginRecord> SearchLoginRecord(int systemId, string companyId, string startTime, string endTime, string clientId, string keyword)
        {
            try
            {
                DateTime dateStartTime;
                DateTime dateEndTime;
                if (string.IsNullOrWhiteSpace(startTime))
                {
                    var entity = Find(m => m.SystemID == systemId && m.CompanyID == companyId && m.CreateDate != null);
                    if (entity == null)
                        return null;
                    else
                        dateStartTime = entity.CreateDate.Value;
                }
                else
                {
                    dateStartTime = startTime.ToDate();
                }
                if (string.IsNullOrWhiteSpace(endTime))
                    dateEndTime = DateTime.Now;
                else
                    dateEndTime = endTime.ToDate();

                byte btClientId = 0;
                if (!string.IsNullOrWhiteSpace(clientId))
                    btClientId = clientId.ToByte();

                var expression = ExtLinq.True<Ld_Log_LoginRecord>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId && 
                m.CreateDate.Value.Date>= dateStartTime.Date&& m.CreateDate.Value.Date <= dateEndTime.Date &&
                m.ClientID == (string.IsNullOrWhiteSpace(clientId) ? m.ClientID : btClientId) && 
                (m.Account.Contains(keyword) || m.NickName.Contains(keyword) || m.IpAddress.Contains(keyword))
                );
                var lists = FindListTop(expression, m => m.ID, false, 1000);
                if (lists == null)
                    return null;
                else
                    return lists.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
