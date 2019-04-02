using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LdCms.EF.DbModels
{
    public partial class LdCmsDbContext : DbContext
    {
        public LdCmsDbContext()
        {
        }

        public LdCmsDbContext(DbContextOptions<LdCmsDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ld_Basics_Media> Ld_Basics_Media { get; set; }
        public virtual DbSet<Ld_Basics_MediaInterface> Ld_Basics_MediaInterface { get; set; }
        public virtual DbSet<Ld_Basics_MediaMember> Ld_Basics_MediaMember { get; set; }
        public virtual DbSet<Ld_Extend_Advertisement> Ld_Extend_Advertisement { get; set; }
        public virtual DbSet<Ld_Extend_AdvertisementDetails> Ld_Extend_AdvertisementDetails { get; set; }
        public virtual DbSet<Ld_Extend_Link> Ld_Extend_Link { get; set; }
        public virtual DbSet<Ld_Extend_LinkGroup> Ld_Extend_LinkGroup { get; set; }
        public virtual DbSet<Ld_Extend_SearchKeyword> Ld_Extend_SearchKeyword { get; set; }
        public virtual DbSet<Ld_Info_Artice> Ld_Info_Artice { get; set; }
        public virtual DbSet<Ld_Info_Block> Ld_Info_Block { get; set; }
        public virtual DbSet<Ld_Info_Class> Ld_Info_Class { get; set; }
        public virtual DbSet<Ld_Info_Notice> Ld_Info_Notice { get; set; }
        public virtual DbSet<Ld_Info_NoticeCategory> Ld_Info_NoticeCategory { get; set; }
        public virtual DbSet<Ld_Info_Page> Ld_Info_Page { get; set; }
        public virtual DbSet<Ld_Institution_Company> Ld_Institution_Company { get; set; }
        public virtual DbSet<Ld_Institution_Dealer> Ld_Institution_Dealer { get; set; }
        public virtual DbSet<Ld_Institution_Department> Ld_Institution_Department { get; set; }
        public virtual DbSet<Ld_Institution_Position> Ld_Institution_Position { get; set; }
        public virtual DbSet<Ld_Institution_Staff> Ld_Institution_Staff { get; set; }
        public virtual DbSet<Ld_Institution_Store> Ld_Institution_Store { get; set; }
        public virtual DbSet<Ld_Institution_Supplier> Ld_Institution_Supplier { get; set; }
        public virtual DbSet<Ld_Institution_Warehouse> Ld_Institution_Warehouse { get; set; }
        public virtual DbSet<Ld_Log_ErrorRecord> Ld_Log_ErrorRecord { get; set; }
        public virtual DbSet<Ld_Log_LoginRecord> Ld_Log_LoginRecord { get; set; }
        public virtual DbSet<Ld_Log_Table> Ld_Log_Table { get; set; }
        public virtual DbSet<Ld_Log_TableDetails> Ld_Log_TableDetails { get; set; }
        public virtual DbSet<Ld_Log_TableOperation> Ld_Log_TableOperation { get; set; }
        public virtual DbSet<Ld_Log_VisitorRecord> Ld_Log_VisitorRecord { get; set; }
        public virtual DbSet<Ld_Log_WebApiAccessRecord> Ld_Log_WebApiAccessRecord { get; set; }
        public virtual DbSet<Ld_Member_AccessToken> Ld_Member_AccessToken { get; set; }
        public virtual DbSet<Ld_Member_Account> Ld_Member_Account { get; set; }
        public virtual DbSet<Ld_Member_Address> Ld_Member_Address { get; set; }
        public virtual DbSet<Ld_Member_AmountRecord> Ld_Member_AmountRecord { get; set; }
        public virtual DbSet<Ld_Member_Invoice> Ld_Member_Invoice { get; set; }
        public virtual DbSet<Ld_Member_LoginLogs> Ld_Member_LoginLogs { get; set; }
        public virtual DbSet<Ld_Member_PointRecord> Ld_Member_PointRecord { get; set; }
        public virtual DbSet<Ld_Member_Rank> Ld_Member_Rank { get; set; }
        public virtual DbSet<Ld_Member_RefreshToken> Ld_Member_RefreshToken { get; set; }
        public virtual DbSet<Ld_Service_MessageBoard> Ld_Service_MessageBoard { get; set; }
        public virtual DbSet<Ld_Sys_AccessCorsHost> Ld_Sys_AccessCorsHost { get; set; }
        public virtual DbSet<Ld_Sys_Code> Ld_Sys_Code { get; set; }
        public virtual DbSet<Ld_Sys_Config> Ld_Sys_Config { get; set; }
        public virtual DbSet<Ld_Sys_Function> Ld_Sys_Function { get; set; }
        public virtual DbSet<Ld_Sys_InterfaceAccessToken> Ld_Sys_InterfaceAccessToken { get; set; }
        public virtual DbSet<Ld_Sys_InterfaceAccessWhiteList> Ld_Sys_InterfaceAccessWhiteList { get; set; }
        public virtual DbSet<Ld_Sys_InterfaceAccount> Ld_Sys_InterfaceAccount { get; set; }
        public virtual DbSet<Ld_Sys_Operator> Ld_Sys_Operator { get; set; }
        public virtual DbSet<Ld_Sys_OperatorRole> Ld_Sys_OperatorRole { get; set; }
        public virtual DbSet<Ld_Sys_Role> Ld_Sys_Role { get; set; }
        public virtual DbSet<Ld_Sys_RoleFunction> Ld_Sys_RoleFunction { get; set; }
        public virtual DbSet<Ld_Sys_Version> Ld_Sys_Version { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=119.29.147.31;User Id=sa;Password=Shierpu#6666298;Database=LdCms_CloudBusiness");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ld_Basics_Media>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.MediaID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MediaID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.FileExtension)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FileName).HasMaxLength(250);

                entity.Property(e => e.Remark).HasMaxLength(400);

                entity.Property(e => e.Src).HasMaxLength(250);

                entity.Property(e => e.Title).HasMaxLength(400);

                entity.Property(e => e.Type)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Url).HasMaxLength(250);
            });

            modelBuilder.Entity<Ld_Basics_MediaInterface>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.MediaID, e.AppID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MediaID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AppID)
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ld_Basics_MediaMember>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.MemberID, e.MediaID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MemberID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MediaID)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ld_Extend_Advertisement>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.AdvertisementID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AdvertisementID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(400);
            });

            modelBuilder.Entity<Ld_Extend_AdvertisementDetails>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.DetailsID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DetailsID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AdvertisementID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.MediaID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MediaPath)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.MediaType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Remark).HasMaxLength(400);

                entity.Property(e => e.Title).HasMaxLength(100);

                entity.Property(e => e.Url)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ld_Extend_Link>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.LinkID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LinkID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.GroupID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.GroupName).HasMaxLength(20);

                entity.Property(e => e.Logo)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.TypeName).HasMaxLength(20);

                entity.Property(e => e.Url).HasMaxLength(250);
            });

            modelBuilder.Entity<Ld_Extend_LinkGroup>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.GroupID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.GroupID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(400);
            });

            modelBuilder.Entity<Ld_Extend_SearchKeyword>(entity =>
            {
                entity.HasIndex(e => new { e.IsTop, e.ID })
                    .HasName("IDX_SearchKeyword_OrderBy");

                entity.HasIndex(e => new { e.SystemID, e.CompanyID })
                    .HasName("IDX_SearchKeyword_Content");

                entity.HasIndex(e => new { e.SystemID, e.CompanyID, e.Keyword, e.CreateDate })
                    .HasName("IDX_SearchKeyword_Search");

                entity.Property(e => e.ClientName).HasMaxLength(20);

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Keyword).HasMaxLength(50);

                entity.Property(e => e.MemberID)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ld_Info_Artice>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.ArticeID });

                entity.HasIndex(e => new { e.SystemID, e.CompanyID, e.IsDel, e.CreateDate })
                    .HasName("IDX_Artice_Content");

                entity.HasIndex(e => new { e.SystemID, e.CompanyID, e.ClassID, e.IsDel, e.CreateDate })
                    .HasName("IDX_Artice_Content_ClassId");

                entity.HasIndex(e => new { e.SystemID, e.CompanyID, e.Title, e.ClassID, e.State, e.IsDel, e.Account, e.NickName, e.CreateDate })
                    .HasName("IDX_Artice_Search");

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ArticeID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Account)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Author).HasMaxLength(50);

                entity.Property(e => e.ClassID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ClassName).HasMaxLength(50);

                entity.Property(e => e.CommentEndTime).HasColumnType("datetime");

                entity.Property(e => e.CommentStartTime).HasColumnType("datetime");

                entity.Property(e => e.Content).HasColumnType("ntext");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(400);

                entity.Property(e => e.ImgArray)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ImgSrc)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Keyword).HasMaxLength(200);

                entity.Property(e => e.NickName).HasMaxLength(20);

                entity.Property(e => e.Source).HasMaxLength(50);

                entity.Property(e => e.Tags).HasMaxLength(50);

                entity.Property(e => e.Title).HasMaxLength(200);

                entity.Property(e => e.TitleBrief).HasMaxLength(100);

                entity.Property(e => e.TypeID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TypeName).HasMaxLength(50);

                entity.Property(e => e.Url)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ld_Info_Block>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.BlockID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.BlockID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Content).HasColumnType("ntext");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(400);

                entity.Property(e => e.Tags).HasMaxLength(200);

                entity.Property(e => e.Title).HasMaxLength(200);
            });

            modelBuilder.Entity<Ld_Info_Class>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.ClassID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ClassID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ClassName).HasMaxLength(50);

                entity.Property(e => e.ClassTypeName).HasMaxLength(20);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(400);

                entity.Property(e => e.ImgSrc)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Keyword).HasMaxLength(200);

                entity.Property(e => e.OrderPath)
                    .HasMaxLength(400)
                    .IsUnicode(false);

                entity.Property(e => e.ParentID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ParentPath)
                    .HasMaxLength(400)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ld_Info_Notice>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.NoticeID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NoticeID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Author).HasMaxLength(50);

                entity.Property(e => e.ClassID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ClassName).HasMaxLength(50);

                entity.Property(e => e.Content).HasColumnType("ntext");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(400);

                entity.Property(e => e.ImgArray)
                    .HasMaxLength(2000)
                    .IsUnicode(false);

                entity.Property(e => e.ImgSrc)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Keyword).HasMaxLength(200);

                entity.Property(e => e.StaffId)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StaffName).HasMaxLength(20);

                entity.Property(e => e.Title).HasMaxLength(200);
            });

            modelBuilder.Entity<Ld_Info_NoticeCategory>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.CategoryID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CategoryName).HasMaxLength(20);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(400);
            });

            modelBuilder.Entity<Ld_Info_Page>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.PageID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PageID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ClassID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ClassName).HasMaxLength(20);

                entity.Property(e => e.Content).HasColumnType("ntext");

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(400);

                entity.Property(e => e.ImgArray)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ImgSrc)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Keyword).HasMaxLength(200);

                entity.Property(e => e.Title).HasMaxLength(200);
            });

            modelBuilder.Entity<Ld_Institution_Company>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.CompanyName).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DealerID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.Fax)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LogoImages).HasMaxLength(255);

                entity.Property(e => e.NickName).HasMaxLength(20);

                entity.Property(e => e.Password)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.RegisterIpAddress)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterTime).HasColumnType("datetime");

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.Tel)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserName).HasMaxLength(20);

                entity.Property(e => e.Version)
                    .HasMaxLength(10)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ld_Institution_Dealer>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.DealerID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DealerID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.ClassID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ClassName).HasMaxLength(20);

                entity.Property(e => e.CompanyName).HasMaxLength(100);

                entity.Property(e => e.Contacts).HasMaxLength(20);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DealerName).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(400);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.Fax)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Logo)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Password).HasMaxLength(32);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.QQ)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RankID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RankName).HasMaxLength(20);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.Tel)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserName).HasMaxLength(20);

                entity.Property(e => e.Weixin)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ld_Institution_Department>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.DepartmentID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentName).HasMaxLength(20);

                entity.Property(e => e.Description).HasMaxLength(400);

                entity.Property(e => e.NodePath)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.ParentID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SortPath)
                    .HasMaxLength(400)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ld_Institution_Position>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.PositionID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PositionID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.PositionName).HasMaxLength(20);
            });

            modelBuilder.Entity<Ld_Institution_Staff>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.StaffID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StaffID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.BirthDate).HasColumnType("date");

                entity.Property(e => e.BirthPlace).HasMaxLength(200);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.DepartmentID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DepartmentName).HasMaxLength(50);

                entity.Property(e => e.Description).HasMaxLength(400);

                entity.Property(e => e.Education).HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.EndWorkDate).HasColumnType("date");

                entity.Property(e => e.ExpirationContractDate).HasColumnType("date");

                entity.Property(e => e.HeadImgUrl).HasMaxLength(200);

                entity.Property(e => e.Identification)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.NickName).HasMaxLength(20);

                entity.Property(e => e.Password)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PositionID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PositionName).HasMaxLength(50);

                entity.Property(e => e.QQ)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SignContractDate).HasColumnType("date");

                entity.Property(e => e.StaffName).HasMaxLength(10);

                entity.Property(e => e.StartWorkDate).HasColumnType("date");

                entity.Property(e => e.StoreID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StoreName).HasMaxLength(50);

                entity.Property(e => e.UserName).HasMaxLength(50);

                entity.Property(e => e.WarehouseID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.WarehouseName).HasMaxLength(50);

                entity.Property(e => e.Weixin)
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ld_Institution_Store>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.StoreID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StoreID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.Contacts).HasMaxLength(10);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(400);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.Fax)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Keyword).HasMaxLength(200);

                entity.Property(e => e.Logo)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.StoreName).HasMaxLength(100);

                entity.Property(e => e.Tel)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ld_Institution_Supplier>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.SupplierID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.SupplierID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Business).HasMaxLength(400);

                entity.Property(e => e.ClassName).HasMaxLength(20);

                entity.Property(e => e.CodeCertificateImageUrl)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.CodeCertificateNumber).HasMaxLength(50);

                entity.Property(e => e.CompanyName).HasMaxLength(50);

                entity.Property(e => e.CompanyNature).HasMaxLength(50);

                entity.Property(e => e.Contacts)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(400);

                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Fax)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LicenseImageUrl).HasMaxLength(250);

                entity.Property(e => e.LicenseNumber).HasMaxLength(50);

                entity.Property(e => e.Representative).HasMaxLength(20);

                entity.Property(e => e.Tel)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ld_Institution_Warehouse>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.WarehouseID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.WarehouseID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.Contacts).HasMaxLength(10);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(400);

                entity.Property(e => e.Fax)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Tel)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.WarehouseName).HasMaxLength(100);
            });

            modelBuilder.Entity<Ld_Log_ErrorRecord>(entity =>
            {
                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.IpAddress).HasMaxLength(20);

                entity.Property(e => e.Message).HasMaxLength(4000);

                entity.Property(e => e.Url).HasMaxLength(255);
            });

            modelBuilder.Entity<Ld_Log_LoginRecord>(entity =>
            {
                entity.Property(e => e.Account)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ClientName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NickName).HasMaxLength(20);

                entity.Property(e => e.TypeName).HasMaxLength(20);
            });

            modelBuilder.Entity<Ld_Log_Table>(entity =>
            {
                entity.HasKey(e => e.TableID);

                entity.Property(e => e.TableID)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.Account).HasMaxLength(50);

                entity.Property(e => e.BusinessName).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.NickName).HasMaxLength(50);

                entity.Property(e => e.PrimaryKey)
                    .HasMaxLength(800)
                    .IsUnicode(false);

                entity.Property(e => e.Remark).HasMaxLength(400);

                entity.Property(e => e.TableName).HasMaxLength(50);

                entity.Property(e => e.UrlTemplate).HasMaxLength(400);
            });

            modelBuilder.Entity<Ld_Log_TableDetails>(entity =>
            {
                entity.Property(e => e.Account).HasMaxLength(50);

                entity.Property(e => e.ColumnDataType).HasMaxLength(50);

                entity.Property(e => e.ColumnName).HasMaxLength(50);

                entity.Property(e => e.ColumnText).HasMaxLength(50);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.NickName).HasMaxLength(50);

                entity.Property(e => e.Remark).HasMaxLength(400);

                entity.Property(e => e.TableID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TableName)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ld_Log_TableOperation>(entity =>
            {
                entity.Property(e => e.Account).HasMaxLength(20);

                entity.Property(e => e.ClassName).HasMaxLength(20);

                entity.Property(e => e.ClientName).HasMaxLength(20);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.IpAdress)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NewContent).HasColumnType("text");

                entity.Property(e => e.NickName).HasMaxLength(20);

                entity.Property(e => e.OldContent).HasColumnType("text");

                entity.Property(e => e.PrimaryKeyValue).HasMaxLength(200);

                entity.Property(e => e.Remark).HasMaxLength(400);

                entity.Property(e => e.TableID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.TableName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Url).HasMaxLength(400);
            });

            modelBuilder.Entity<Ld_Log_VisitorRecord>(entity =>
            {
                entity.Property(e => e.AbsoluteUri).HasMaxLength(250);

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Host).HasMaxLength(50);

                entity.Property(e => e.IpAddress).HasMaxLength(20);

                entity.Property(e => e.QueryString).HasMaxLength(4000);
            });

            modelBuilder.Entity<Ld_Log_WebApiAccessRecord>(entity =>
            {
                entity.Property(e => e.ActionName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.AppID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ClassName).HasMaxLength(20);

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ControllerName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Method)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Parameter).HasColumnType("ntext");

                entity.Property(e => e.ParameterName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Result).HasColumnType("ntext");

                entity.Property(e => e.TotalMillisecond).HasComputedColumnSql("(datediff(millisecond,[createdate],[updatedate]))");

                entity.Property(e => e.UpdateDate).HasColumnType("datetime");

                entity.Property(e => e.Url)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Version)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ld_Member_AccessToken>(entity =>
            {
                entity.HasKey(e => e.Token);

                entity.Property(e => e.Token)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MemberID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Uuid)
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ld_Member_Account>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.MemberID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MemberID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Address).HasMaxLength(128);

                entity.Property(e => e.Area).HasMaxLength(20);

                entity.Property(e => e.City).HasMaxLength(20);

                entity.Property(e => e.ClassName).HasMaxLength(20);

                entity.Property(e => e.CompanyName).HasMaxLength(50);

                entity.Property(e => e.Country).HasMaxLength(20);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.HeadImageUrl)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.LastLoginIpAddress)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.LastLoginTime).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(10);

                entity.Property(e => e.NickName).HasMaxLength(20);

                entity.Property(e => e.Password)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Province).HasMaxLength(20);

                entity.Property(e => e.QQ)
                    .HasMaxLength(64)
                    .IsUnicode(false);

                entity.Property(e => e.RankID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RankName).HasMaxLength(20);

                entity.Property(e => e.RegisterIpAddress)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RegisterTime).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.Tel)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Weixin)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ld_Member_Address>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.MemberID, e.AddressID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MemberID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AddressID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Address).HasMaxLength(150);

                entity.Property(e => e.Area).HasMaxLength(20);

                entity.Property(e => e.City).HasMaxLength(20);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Name).HasMaxLength(20);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Province).HasMaxLength(20);

                entity.Property(e => e.Remark).HasMaxLength(400);

                entity.Property(e => e.Tags).HasMaxLength(200);
            });

            modelBuilder.Entity<Ld_Member_AmountRecord>(entity =>
            {
                entity.Property(e => e.Account)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Body).HasColumnType("ntext");

                entity.Property(e => e.ClassName).HasMaxLength(20);

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.MemberID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NickName).HasMaxLength(20);

                entity.Property(e => e.Remark).HasMaxLength(400);
            });

            modelBuilder.Entity<Ld_Member_Invoice>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.MemberID, e.InvoiceID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MemberID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.InvoiceID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.BankAccount)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BankName).HasMaxLength(50);

                entity.Property(e => e.BusinessLicense).HasMaxLength(400);

                entity.Property(e => e.ClassName).HasMaxLength(20);

                entity.Property(e => e.CompanyName).HasMaxLength(100);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(400);

                entity.Property(e => e.TaxpayerIdentificationNumber)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Tel)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ld_Member_LoginLogs>(entity =>
            {
                entity.Property(e => e.Account)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MemberID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NickName).HasMaxLength(20);
            });

            modelBuilder.Entity<Ld_Member_PointRecord>(entity =>
            {
                entity.Property(e => e.Account).HasMaxLength(20);

                entity.Property(e => e.Body).HasColumnType("ntext");

                entity.Property(e => e.ClassName).HasMaxLength(20);

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.MemberID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.NickName).HasMaxLength(20);

                entity.Property(e => e.Remark).HasMaxLength(400);

                entity.Property(e => e.TypeName).HasMaxLength(20);

                entity.Property(e => e.ipAddress)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ld_Member_Rank>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.RankID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RankID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.RankName).HasMaxLength(20);

                entity.Property(e => e.Remark).HasMaxLength(400);
            });

            modelBuilder.Entity<Ld_Member_RefreshToken>(entity =>
            {
                entity.HasKey(e => e.RefreshToken);

                entity.Property(e => e.RefreshToken)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Token)
                    .HasMaxLength(64)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ld_Service_MessageBoard>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.MessageID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.MessageID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Address).HasMaxLength(250);

                entity.Property(e => e.CompanyName).HasMaxLength(50);

                entity.Property(e => e.Content).HasMaxLength(400);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ImgSrc)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Reply).HasMaxLength(400);

                entity.Property(e => e.ReplyStaffId)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ReplyStaffName).HasMaxLength(10);

                entity.Property(e => e.ReplyTime).HasColumnType("datetime");

                entity.Property(e => e.Tel)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ld_Sys_AccessCorsHost>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.WebHost });

                entity.Property(e => e.WebHost)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.Account)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.NickName).HasMaxLength(20);

                entity.Property(e => e.Remark).HasMaxLength(400);
            });

            modelBuilder.Entity<Ld_Sys_Code>(entity =>
            {
                entity.HasKey(e => e.SystemID);

                entity.Property(e => e.SystemID).ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.SystemName).HasMaxLength(50);
            });

            modelBuilder.Entity<Ld_Sys_Config>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Copyright).HasMaxLength(250);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(400);

                entity.Property(e => e.EmailAddress).HasMaxLength(100);

                entity.Property(e => e.EmailHost).HasMaxLength(50);

                entity.Property(e => e.EmailName).HasMaxLength(100);

                entity.Property(e => e.EmailPassword).HasMaxLength(32);

                entity.Property(e => e.EmailSendPattern).HasMaxLength(50);

                entity.Property(e => e.HomeUrl)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.Property(e => e.IcpNumber).HasMaxLength(50);

                entity.Property(e => e.Keyword).HasMaxLength(200);

                entity.Property(e => e.LoginIpAddressWhiteList).HasMaxLength(1000);

                entity.Property(e => e.Shielding).HasMaxLength(2000);

                entity.Property(e => e.StatisticsCode).HasMaxLength(800);

                entity.Property(e => e.StyleSrc)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title).HasMaxLength(250);

                entity.Property(e => e.UploadRoot)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ld_Sys_Function>(entity =>
            {
                entity.HasKey(e => e.FunctionID);

                entity.Property(e => e.FunctionID)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.FunctionName).HasMaxLength(50);

                entity.Property(e => e.ParentID)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ld_Sys_InterfaceAccessToken>(entity =>
            {
                entity.HasKey(e => e.Token);

                entity.Property(e => e.Token)
                    .HasMaxLength(128)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.AppID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CompanyID)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ld_Sys_InterfaceAccessWhiteList>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.Account, e.IpAddress, e.ClassID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Account)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.IpAddress)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ClassName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(400);
            });

            modelBuilder.Entity<Ld_Sys_InterfaceAccount>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.Account });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Account)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AppID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.AppKey)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.AppSecret)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Password)
                    .HasMaxLength(32)
                    .IsUnicode(false);

                entity.Property(e => e.Uuid)
                    .HasMaxLength(32)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ld_Sys_Operator>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.StaffID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StaffID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(200);
            });

            modelBuilder.Entity<Ld_Sys_OperatorRole>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.StaffID, e.RoleID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.StaffID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RoleID)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ld_Sys_Role>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.RoleID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RoleID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Remark).HasMaxLength(200);

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<Ld_Sys_RoleFunction>(entity =>
            {
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.RoleID, e.FunctionID });

                entity.Property(e => e.CompanyID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RoleID)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.FunctionID)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Ld_Sys_Version>(entity =>
            {
                entity.HasKey(e => e.VersionID);

                entity.Property(e => e.VersionID)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.CreateDate).HasColumnType("datetime");

                entity.Property(e => e.Description).HasMaxLength(400);

                entity.Property(e => e.VersionName).HasMaxLength(50);
            });
        }
    }
}
