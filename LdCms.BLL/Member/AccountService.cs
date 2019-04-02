using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LdCms.BLL.Member
{
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.IBLL.Member;
    using LdCms.IDAL.Member;
    using LdCms.Common.Extension;
    using LdCms.Common.Json;
    using LdCms.Common.Security;
    using LdCms.Common.Time;
    /// <summary>
    /// 会员帐号业务逻辑操作类
    /// 
    /// 
    /// </summary>
    public partial class AccountService:BaseService<Ld_Member_Account>,IAccountService
    {
        private readonly IAccountDAL AccountDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public AccountService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IAccountDAL AccountDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.AccountDAL = AccountDAL;
            this.Dal = AccountDAL;
        }
        public override void SetDal()
        {
            Dal = AccountDAL;
        }

        private bool IsAccount(int systemId, string companyId, string memberId)
        {
            try
            {
                return IsExists(m => m.SystemID == systemId && m.CompanyID == companyId && m.MemberID == memberId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Member_Account GetAccount(int systemId, string companyId, string memberId)
        {
            try
            {
                if (!IsAccount(systemId, companyId, memberId))
                    throw new Exception("会员ID不存在！");
                return Find(m => m.SystemID == systemId && m.CompanyID == companyId && m.MemberID == memberId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Member_Account GetAccountByUserName(int systemId, string companyId, string username)
        {
            try
            {
                return Find(m => m.SystemID == systemId && m.CompanyID == companyId && m.UserName == username);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Member_Account> GetAccountPaging(int systemId, string companyId, int pageId, int pageSize)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Member_Account>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId);
                var lists = FindListPaging(expression, m => m.CreateDate, false, pageId, pageSize);
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
        public bool SaveAccount(Ld_Member_Account entity)
        {
            try
            {
                int systemId = entity.SystemID;
                string companyId = entity.CompanyID;
                string memberId = PrimaryKeyHelper.MakePrimaryKey(PrimaryKeyHelper.PrimaryKeyType.MemberAccount, PrimaryKeyHelper.PrimaryKeyLen.V2);
                DateTime CreateDate = DateTime.Now;
                if (IsAccount(systemId, companyId, memberId))
                    throw new Exception("会员ID已存在！");
                entity.MemberID = memberId;
                entity.State = true;
                entity.IsDel = false;
                entity.CreateDate = CreateDate;

                return Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateAccount(Ld_Member_Account entity)
        {
            try
            {
                int systemId = entity.SystemID;
                string companyId = entity.CompanyID;
                string memberId = entity.MemberID;
                if (!IsAccount(systemId, companyId, memberId))
                    throw new Exception("会员ID不存在！");

                return Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateAccountState(int systemId, string companyId, string memberId, bool state)
        {
            try
            {
                var entity = GetAccount(systemId, companyId, memberId);
                if (entity == null)
                    throw new Exception("会员ID不存在！");
                bool oldState = entity.State.ToBool();
                if (oldState == state)
                    throw new Exception("状态没有改变！");
                entity.State = state;
                return Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateAccountDelete(int systemId, string companyId, string memberId, bool del)
        {
            try
            {
                var entity = GetAccount(systemId, companyId, memberId);
                if (entity == null)
                    throw new Exception("会员ID不存在！");
                bool isDel = entity.IsDel.ToBool();
                if (isDel == del)
                    throw new Exception("状态没有改变！");
                entity.IsDel = del;
                return Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteAccount(int systemId, string companyId, string memberId)
        {
            try
            {
                var entity = GetAccount(systemId, companyId, memberId);
                if (entity == null)
                    throw new Exception("会员ID不存在！");
                return Delete(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int DeleteAccountAll(int systemId, string companyId, string memberId)
        {
            try
            {
                var entity = GetAccount(systemId, companyId, memberId);
                if (entity == null)
                    throw new Exception("会员ID不存在！");

                var expressionAddress = ExtLinq.True<Ld_Member_Address>();
                expressionAddress = expressionAddress.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.MemberID == memberId);
                var expressionInvoice = ExtLinq.True<Ld_Member_Invoice>();
                expressionInvoice = expressionInvoice.And(m => m.SystemID == systemId && m.CompanyID == companyId && m.MemberID == memberId);

                int intnum = 0;
                var dbContext = new DAL.BaseDAL(LdCmsDbEntitiesContext);
                using (var db = dbContext.DbEntities())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            dbContext.Delete(expressionAddress);
                            dbContext.Delete(expressionInvoice);
                            db.Set<Ld_Member_Account>().Remove(entity);
                            intnum = db.SaveChanges();
                            trans.Commit();
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                        }
                        return intnum;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #region 存储过程方法
        public bool SaveAccountRegisterPro(int systemId, string companyId, string memberId, string password, string phone, string ipAddress)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Add_Member_AccountRegister(systemId, companyId, memberId, password, phone, ipAddress, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateAccountStatePro(int systemId, string companyId, string memberId, bool state)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Update_Member_AccountState(systemId, companyId, memberId, state, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateAccountDeletePro(int systemId, string companyId, string memberId, bool delete)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Update_Member_AccountDelete(systemId, companyId, memberId, delete, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateAccountPasswordPro(int systemId, string companyId, string memberId, string password)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Update_Member_AccountPassword(systemId, companyId, memberId, password, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteAccountPro(int systemId, string companyId, string memberId)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Delete_Member_Account(systemId, companyId, memberId, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Ld_Member_Account GetAccountByAccessTokenPro(int systemId, string accessToken)
        {
            try
            {
                if (systemId == 0)
                    throw new Exception("系统编号不能为0！");
                if (string.IsNullOrWhiteSpace(accessToken))
                    throw new Exception("access token 不能为空！");
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Get_Member_AccountByAccessToken(systemId, accessToken, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                if (result == null)
                    return null;
                else
                    return result.ToObject<List<Ld_Member_Account>>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Member_Account GetAccountByRefreshTokenPro(int systemId, string refreshToken)
        {
            try
            {
                if (systemId == 0)
                    throw new Exception("系统编号不能为0！");
                if (!string.IsNullOrWhiteSpace(refreshToken))
                    throw new Exception("access token 不能为空！");
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Get_Member_AccountByRefreshToken(systemId, refreshToken, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                if (result == null)
                    return null;
                else
                    return result.ToObject<List<Ld_Member_Account>>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Member_Account GetAccountPro(int systemId, string companyId, string memberId)
        {
            try
            {
                if (systemId == 0)
                    throw new Exception("系统编号不能为0！");
                if (string.IsNullOrEmpty(companyId))
                    throw new Exception("公司编号不能为0！");
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Get_Member_Account(systemId, companyId, memberId, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                if (result == null)
                    return null;
                else
                    return result.ToObject<List<Ld_Member_Account>>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Member_Account> GetAccountPagingPro(int systemId, string companyId, string delete, int pageId, int pageSize, out int rowCount)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Get_Member_AccountPaging(systemId, companyId, delete, pageId, pageSize, out errCode, out errMsg, out rowCount);
                if (errCode != 0)
                    throw new Exception(errMsg);
                if (result == null)
                    return null;
                else
                    return result.ToJson().ToObject<List<Ld_Member_Account>>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Member_Account> SearchAccountPro(int systemId, string companyId, string startTime, string endTime, string rankId, string delete, string keyword)
        {
            try
            {
                if (systemId == 0)
                    throw new Exception("系统编号不能为0！");
                if (string.IsNullOrEmpty(companyId))
                    throw new Exception("公司编号不能为0！");
                if (!string.IsNullOrWhiteSpace(startTime))
                {
                    if (!TimeHelper.IsDate(startTime))
                        throw new Exception("开始时间格式错误！");
                }
                if (!string.IsNullOrWhiteSpace(endTime))
                {
                    if (!TimeHelper.IsDate(endTime))
                        throw new Exception("结束时间格式错误！");
                }
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Search_Member_Account(systemId, companyId, startTime, endTime, rankId, delete, keyword, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                if (result == null)
                    return null;
                else
                    return result.ToJson().ToObject<List<Ld_Member_Account>>();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool VerifyAccountLoginPro(int systemId, string companyId, string account, string password)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Verify_Member_AccountLogin(systemId, companyId, account, password, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion
    }
}
