using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Info
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    public partial interface IBlockService:IBaseService<Ld_Info_Block>
    {
        bool SaveBlock(Ld_Info_Block entity);
        bool UpdateBlock(Ld_Info_Block entity);
        bool UpdateBlockState(int systemId, string companyId, string blockId, bool state);
        bool DeleteBlock(int systemId, string companyId, string blockId);
        Ld_Info_Block GetBlock(int systemId, string companyId, string blockId);
        List<Ld_Info_Block> GetBlockAll(int systemId, string companyId, string state);
        List<Ld_Info_Block> GetBlock(int systemId, string companyId, int pageId, int pageSize);
        List<Ld_Info_Block> SearchBlock(int systemId, string companyId, string startTime, string endTime, string state, string keyword);

    }
}
