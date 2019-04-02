using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LdCms.BLL.Service
{
    using LdCms.EF.DbModels;
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.IBLL.Service;
    using LdCms.IDAL.Service;
    using LdCms.Common.Json;
    using LdCms.Common.Security;
    using LdCms.Common.Extension;

    public partial class MessageBoardService:BaseService<Ld_Service_MessageBoard>,IMessageBoardService
    {
        private readonly IMessageBoardDAL MessageBoardDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public MessageBoardService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IMessageBoardDAL MessageBoardDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.MessageBoardDAL = MessageBoardDAL;
            this.Dal = MessageBoardDAL;
        }
        public override void SetDal()
        {
            Dal = MessageBoardDAL;
        }

        public bool SaveMessageBoard(Ld_Service_MessageBoard entity)
        {
            try
            {
                string messageId = PrimaryKeyHelper.MakePrimaryKey(PrimaryKeyHelper.PrimaryKeyType.ServiceMessageBoard);
                entity.MessageID = messageId;
                entity.IsTop = false;
                entity.State = false;
                entity.CreateDate = DateTime.Now;
                return Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public Ld_Service_MessageBoard GetMessageBoard(int systemId, string companyId, string messageId)
        {
            try
            {
                return Find(m => m.SystemID == systemId && m.CompanyID == companyId && m.MessageID == messageId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool DeleteMessageBoard(int systemId, string companyId, string messageId)
        {
            try
            {
                return Delete(m => m.SystemID == systemId && m.CompanyID == companyId && m.MessageID == messageId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateMessageBoardState(int systemId, string companyId, string messageId, bool state)
        {
            try
            {
                var entity = GetMessageBoard(systemId, companyId, messageId);
                if (entity == null)
                    throw new Exception("not data！");
                entity.State = state;
                return Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool UpdateMessageBoardReply(int systemId, string companyId, string messageId, string reply, string replyStaffId, string replyStaffName, bool state)
        {
            try
            {
                var entity = GetMessageBoard(systemId, companyId, messageId);
                if (entity == null)
                    throw new Exception("not data！");
                entity.Reply = reply;
                entity.ReplyStaffId = replyStaffId;
                entity.ReplyStaffName = replyStaffName;
                entity.ReplyTime = DateTime.Now;
                entity.State = state;
                return Update(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Service_MessageBoard> GetMessageBoardPaging(int systemId, string companyId, int pageId, int pageSize, out int rowCount)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Service_MessageBoard>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId);
                rowCount = Count(expression);
                var lists = FindListPaging(expression, m => m.CreateDate, false, pageId, pageSize);
                return lists == null ? null : lists.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<Ld_Service_MessageBoard> GetMessageBoardPaging(int systemId, string companyId, bool state, int pageId, int pageSize, out int rowCount)
        {
            try
            {
                var expression = ExtLinq.True<Ld_Service_MessageBoard>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId && (m.State.HasValue ? m.State.Value : false) == state);
                rowCount = Count(expression);
                var lists = FindListPaging(expression, m => m.CreateDate, false, pageId, pageSize);
                return lists == null ? null : lists.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Ld_Service_MessageBoard> SearchMessageBoard(int systemId, string companyId, string startTime, string endTime, string state, string keyword)
        {
            try
            {
                DateTime dateStartTime;
                DateTime dateEndTime;
                if (string.IsNullOrWhiteSpace(startTime))
                {
                    var entity = Find(m => m.CreateDate != null);
                    if (entity == null)
                        return null;
                    else
                        dateStartTime = entity.CreateDate.Value;
                }
                else
                {
                    dateStartTime = startTime.ToDate();
                }
                if (string.IsNullOrWhiteSpace(endTime))
                    dateEndTime = DateTime.Now;
                else
                    dateEndTime = endTime.ToDate();

                bool blnState = state.ToBool();

                var expression = ExtLinq.True<Ld_Service_MessageBoard>();
                expression = expression.And(m => m.SystemID == systemId && m.CompanyID == companyId &&
                m.CreateDate.Value.Date >= dateStartTime.Date && m.CreateDate.Value.Date <= dateEndTime.Date &&
                m.State == (string.IsNullOrWhiteSpace(state) ? m.State : blnState) && (m.Name.Contains(keyword) || m.Content.Contains(keyword)));

                var lists = FindListTop(expression, m => m.CreateDate, false, 1000);
                return lists == null ? null : lists.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
