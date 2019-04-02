using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LdCms.Web.Services
{
    public partial interface IBaseManager
    {
        bool IsPermission(string companyId, string staffId, string functionId);
        string GetQueryString(string name);
        string GetFormValue(string name);
        string GetFormValueArr(string name);
        string GetFormValue(FormCollection formValue, string name);
    }
}
