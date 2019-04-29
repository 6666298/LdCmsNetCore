using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace LdCms.BLL.Basics
{
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbModels;
    using LdCms.EF.DbStoredProcedure;
    using LdCms.IBLL.Basics;
    using LdCms.IDAL.Basics;
    using LdCms.Common.Json;
    /// <summary>
    /// 
    /// </summary>
    public partial class MediaService:BaseService<Ld_Basics_Media>,IMediaService
    {
        private readonly IMediaDAL MediaDAL;
        private readonly LdCmsDbEntitiesContext LdCmsDbEntitiesContext;
        public MediaService(LdCmsDbEntitiesContext LdCmsDbEntitiesContext, IMediaDAL MediaDAL)
        {
            this.LdCmsDbEntitiesContext = LdCmsDbEntitiesContext;
            this.MediaDAL = MediaDAL;
            this.Dal = MediaDAL;
        }
        public override void SetDal()
        {
            Dal = MediaDAL;
        }

        public bool SaveMedia(Ld_Basics_Media entity)
        {
            try
            {
                entity.State = true;
                entity.CreateDate = DateTime.Now;
                return Add(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int MediaInterface(string appId, Ld_Basics_Media entity)
        {
            try
            {
                int systemId = entity.SystemID;
                string companyId = entity.CompanyID;
                string mediaId = entity.MediaID;
                entity.State = true;
                entity.CreateDate = DateTime.Now;

                var mediaInterface = new Ld_Basics_MediaInterface()
                {
                    SystemID = systemId,
                    CompanyID = companyId,
                    MediaID = mediaId,
                    AppID = appId
                };

                int intnum = 0;
                var dbContext = new DAL.BaseDAL(LdCmsDbEntitiesContext);
                using (var db = dbContext.DbEntities())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            db.Add(entity);
                            db.Add(mediaInterface);
                            intnum = db.SaveChanges();
                            trans.Commit();
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                        }
                        return intnum;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int MediaInterface(string appId, List<Ld_Basics_Media> lists)
        {
            try
            {
                if (lists == null)
                    throw new Exception("写入列表不能为空！");
                lists.ForEach(m => { m.State = true; m.CreateDate = DateTime.Now; });
                List<Ld_Basics_MediaInterface> listMediaInterface = new List<Ld_Basics_MediaInterface>();
                foreach (var m in lists)
                {
                    listMediaInterface.Add(new Ld_Basics_MediaInterface()
                    {
                        SystemID = m.SystemID,
                        CompanyID = m.CompanyID,
                        MediaID = m.MediaID,
                        AppID = appId
                    });
                }

                int intnum = 0;
                var dbContext = new DAL.BaseDAL(LdCmsDbEntitiesContext);
                using (var db = dbContext.DbEntities())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            dbContext.Add(lists);
                            dbContext.Add(listMediaInterface);
                            intnum = db.SaveChanges();
                            trans.Commit();
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                        }
                        return intnum;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int SaveMediaMember(string memberId, Ld_Basics_Media entity)
        {
            try
            {
                int systemId = entity.SystemID;
                string companyId = entity.CompanyID;
                string mediaId = entity.MediaID;
                entity.State = true;
                entity.CreateDate = DateTime.Now;

                var mediaMember = new Ld_Basics_MediaMember()
                {
                    SystemID = systemId,
                    CompanyID = companyId,
                    MediaID = mediaId,
                    MemberID = memberId
                };

                int intnum = 0;
                var dbContext = new DAL.BaseDAL(LdCmsDbEntitiesContext);
                using (var db = dbContext.DbEntities())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            db.Add(entity);
                            db.Add(mediaMember);
                            intnum = db.SaveChanges();
                            trans.Commit();
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                        }
                        return intnum;
                    }
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public int SaveMediaMember(string memberId, List<Ld_Basics_Media> lists)
        {
            try
            {
                if (lists == null)
                    throw new Exception("写入列表不能为空！");
                lists.ForEach(m => { m.State = true; m.CreateDate = DateTime.Now; });
                List<Ld_Basics_MediaMember> listMediaMember = new List<Ld_Basics_MediaMember>();
                foreach (var m in lists)
                {
                    listMediaMember.Add(new Ld_Basics_MediaMember()
                    {
                        SystemID = m.SystemID,
                        CompanyID = m.CompanyID,
                        MediaID = m.MediaID,
                        MemberID = memberId
                    });
                }

                int intnum = 0;
                var dbContext = new DAL.BaseDAL(LdCmsDbEntitiesContext);
                using (var db = dbContext.DbEntities())
                {
                    using (var trans = db.Database.BeginTransaction())
                    {
                        try
                        {
                            dbContext.Add(lists);
                            dbContext.Add(listMediaMember);
                            intnum = db.SaveChanges();
                            trans.Commit();
                        }
                        catch (Exception)
                        {
                            trans.Rollback();
                        }
                        return intnum;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Ld_Basics_Media GetMedia(int systemId, string companyId, string mediaId)
        {
            try
            {
                return Find(m => m.SystemID == systemId && m.CompanyID == companyId && m.MediaID == mediaId);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
