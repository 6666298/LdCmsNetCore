using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.Common.Utility
{
    public static partial class Utility
    {
        public static int ToTopTotal(int count)
        {
            try
            {
                int total = count <= 0 ? 10 : count;
                return total > 1000 ? 1000 : total;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static int ToPageIndex(int pageId)
        {
            try
            {
                int pageIndex = pageId <= 0 ? 1 : pageId;
                return pageIndex > 1000 ? 1000 : pageIndex;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static int ToPageCount(int pageSize)
        {
            try
            {
                int pageCount = pageSize <= 0 ? 10 : pageSize;
                return pageCount > 100 ? 100 : pageCount;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
