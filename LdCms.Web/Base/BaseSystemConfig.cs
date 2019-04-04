using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LdCms.Web
{
    /// <summary>
    /// 系统配置参数
    /// </summary>
    public static class BaseSystemConfig
    {
        public static int SystemID = 100201;                    //系统编号
        public static int AccessTokenExpiresIn = 7200;          //token过期时间：  7200秒    2小时
        public static int RefreshTokenExpiresIn = 2592000;      //refresh过期时间：2592000秒 30天
        public static string SessionName = ".LdCmsNetCore";     //Session名称


    }
}
