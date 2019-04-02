using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LdCms.Web.Services
{
    /// <summary>
    /// 
    /// </summary>
    public partial interface IApiRecordManager
    {
        long Save(string appId);
        long Save(string appId, string parameter);
        long Save(string token, object formValue);
        bool Update(long id, string result, bool state);
    }
}
