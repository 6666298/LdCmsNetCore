using System;
using System.Collections.Generic;
using System.Text;

namespace LdCms.IBLL.Service
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    public partial interface IMessageBoardService:IBaseService<Ld_Service_MessageBoard>
    {
        bool SaveMessageBoard(Ld_Service_MessageBoard entity);
        bool DeleteMessageBoard(int systemId, string companyId, string messageId);
        bool UpdateMessageBoardState(int systemId, string companyId, string messageId, bool state);
        bool UpdateMessageBoardReply(int systemId, string companyId, string messageId, string reply, string replyStaffId, string replyStaffName, bool state);
        Ld_Service_MessageBoard GetMessageBoard(int systemId, string companyId, string messageId);
        List<Ld_Service_MessageBoard> GetMessageBoardPaging(int systemId, string companyId, int pageId, int pageSize);
        List<Ld_Service_MessageBoard> GetMessageBoardPaging(int systemId, string companyId, bool state, int pageId, int pageSize);
        List<Ld_Service_MessageBoard> SearchMessageBoard(int systemId, string companyId, string startTime, string endTime, string state, string keyword);

        int CountMessageBoard(int systemId, string companyId);
        int CountMessageBoard(int systemId, string companyId, bool state);


    }
}
