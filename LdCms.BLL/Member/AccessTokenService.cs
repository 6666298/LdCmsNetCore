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
    public partial class AccessTokenService:BaseService<Ld_Member_AccessToken>,IAccessTokenService
    {
        private readonly IAccessTokenDAL AccessTokenDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public AccessTokenService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IAccessTokenDAL AccessTokenDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.AccessTokenDAL = AccessTokenDAL;
            this.Dal = AccessTokenDAL;
        }
        public override void SetDal()
        {
            Dal = AccessTokenDAL;
        }
        public Ld_Member_AccessToken GetAccessToken(string token)
        {
            try
            {
                return Find(m => m.Token == token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public SP_Get_Member_AccessToken GetAccessTokenPro(string token)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Get_Member_AccessToken(token, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                if (result == null)
                    return null;
                else
                    return result.ToObject<List<SP_Get_Member_AccessToken>>().FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool SaveAccessTokenPro(string token, string refreshToken, int systemId, string companyId, string memberId, string uuid, int expiresIn, int refreshTokenExpiresIn, string ipAddress, int createTimestamp)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Add_Member_AccessToken(token, refreshToken,systemId, companyId, memberId, uuid, expiresIn, refreshTokenExpiresIn, ipAddress, createTimestamp, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool SaveRefreshTokenPro(string verifyRefreshToken, string token, string refreshToken, int expiresIn,int refreshTokenExpiresIn, string ipAddress, int createTimestamp)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Add_Member_RefreshToken(verifyRefreshToken,token, refreshToken, expiresIn, refreshTokenExpiresIn, ipAddress, createTimestamp, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool VerifyAccessTokenPro(string token, int timestamp)
        {
            try
            {
                int errCode = -1;
                string errMsg = "fail";
                var result = LdCmsDbEntitiesContext.SP_Verify_Member_AccessToken(token, timestamp, out errCode, out errMsg);
                if (errCode != 0)
                    throw new Exception(errMsg);
                return errCode == 0 ? true : false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
