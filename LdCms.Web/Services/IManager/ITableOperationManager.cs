using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.Web
{
    public partial interface ITableOperationManager<T> where T : new()
    {
        string PrimaryKey { get; set; }
        string Account { get; set; }
        string NickName { get; set; }
        bool Select(T t, out long operationId);
        bool Add(T t, out long operationId);
        bool Update(T t, string newContent, out long operationId);
        bool Delete(T t, out long operationId);
        bool SetState(long id, bool state);
    }
}
