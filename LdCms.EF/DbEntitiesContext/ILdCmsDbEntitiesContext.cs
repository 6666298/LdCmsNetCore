using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LdCms.EF.DbEntitiesContext
{
    public partial interface ILdCmsDbEntitiesContext
    {
        ArrayList AA_Get_Sys_Test(int systemId, string companyId, string staffId, out int errCode, out string errMsg);
        ArrayList AA_Get_Sys_TestAsync(int systemId, string companyId, string staffId, out int errCode, out string errMsg);


    }
}
