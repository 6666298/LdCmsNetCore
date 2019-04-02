using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Member
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;

    /// <summary>
    /// 
    /// </summary>
    public partial interface IAccessTokenService:IBaseService<Ld_Member_AccessToken>
    {

        Ld_Member_AccessToken GetAccessToken(string token);

        SP_Get_Member_AccessToken GetAccessTokenPro(string token);
        bool SaveAccessTokenPro(string token, string refreshToken, int systemId, string companyId, string memberId, string uuid, int expiresIn, int refreshTokenExpiresIn, string ipAddress, int createTimestamp);
        bool SaveRefreshTokenPro(string verifyRefreshToken, string token, string refreshToken, int expiresIn,int refreshTokenExpiresIn, string ipAddress, int createTimestamp);
        bool VerifyAccessTokenPro(string token, int timestamp);

    }
}
