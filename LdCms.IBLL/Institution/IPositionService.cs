using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Institution
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbViews;
    using LdCms.EF.DbStoredProcedure;
    public partial interface IPositionService:IBaseService<Ld_Institution_Position>
    {
        bool SavePositionPro(int systemId, string companyId, string positionId, string positionName, string description, int sort, bool state);
        bool UpdatePositionPro(int systemId, string companyId, string positionId, string positionName, string description, int sort, bool state);
        bool UpdatePositionStatePro(int systemId, string companyId, string positionId, bool state);
        bool DeletePositionPro(int systemId, string companyId, string positionId);
        Ld_Institution_Position GetPositionPro(int systemId, string companyId, string positionId);
        List<Ld_Institution_Position> GetPositionByStatePro(int systemId, string companyId, string state);
        List<Ld_Institution_Position> GetPositionPagingPro(int systemId, string companyId, int pageId, int pageSize, out int rowCount);
        List<Ld_Institution_Position> SearchPositionPro(int systemId, string companyId, string startTime, string endTime, string keyword);

    }
}
