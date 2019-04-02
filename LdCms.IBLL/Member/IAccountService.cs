using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Member
{
    using EF.DbModels;
    using LdCms.EF.DbStoredProcedure;

    /// <summary>
    /// 会员帐号业务逻辑服务类
    /// </summary>
    public partial interface IAccountService:IBaseService<Ld_Member_Account>
    {
        Ld_Member_Account GetAccount(int systemId, string companyId, string memberId);
        Ld_Member_Account GetAccountByUserName(int systemId, string companyId, string username);
        List<Ld_Member_Account> GetAccountPaging(int systemId, string companyId, int pageId, int pageSize);
        
        bool SaveAccount(Ld_Member_Account entity);
        bool UpdateAccount(Ld_Member_Account entity);
        bool UpdateAccountState(int systemId, string companyId, string memberId, bool state);
        bool UpdateAccountDelete(int systemId, string companyId, string memberId, bool del);
        bool DeleteAccount(int systemId, string companyId, string memberId);
        int DeleteAccountAll(int systemId, string companyId, string memberId);


        bool SaveAccountRegisterPro(int systemId, string companyId, string memberId, string password, string phone, string ipAddress);
        bool UpdateAccountStatePro(int systemId, string companyId, string memberId, bool state);
        bool UpdateAccountDeletePro(int systemId, string companyId, string memberId, bool delete);
        bool UpdateAccountPasswordPro(int systemId, string companyId, string memberId, string password);
        bool DeleteAccountPro(int systemId, string companyId, string memberId);

        Ld_Member_Account GetAccountByAccessTokenPro(int systemId, string accessToken);
        Ld_Member_Account GetAccountByRefreshTokenPro(int systemId, string refreshToken);
        Ld_Member_Account GetAccountPro(int systemId, string companyId, string memberId);
        List<Ld_Member_Account> GetAccountPagingPro(int systemId, string companyId, string delete, int pageId, int pageSize, out int rowCount);
        List<Ld_Member_Account> SearchAccountPro(int systemId, string companyId, string startTime, string endTime, string rankId, string delete, string keyword);
        bool VerifyAccountLoginPro(int systemId, string companyId, string account, string password);

    }
}
