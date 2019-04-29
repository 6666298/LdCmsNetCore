using System;
using System.Collections;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace LdCms.EF.DbEntitiesContext
{
    using Microsoft.EntityFrameworkCore;
    using Common.Extension;
    using DbModels;
    using DbViews;
    using DbConfig;
    

    /// <summary>
    /// TestDbExContext继承TestDBContext，而TestDBContext又继承DbContext
    /// </summary>
    public partial class LdCmsDbEntitiesContext : LdCmsDbContext, ILdCmsDbEntitiesContext
    {
        public LdCmsDbEntitiesContext(){ }
        public LdCmsDbEntitiesContext(DbContextOptions<LdCmsDbEntitiesContext> options) { }

        /// <summary>
        /// 定义一个DbSet<V_Person>的集合属性V_Person，EF Core会自动为其赋值，然后可以利用TestDbExContext.V_Person属性来读取数据库中V_Person视图的数据
        /// </summary>
        public virtual DbSet<A_Sys_Test> V_Sys_Test { get; set; }
        /// <summary>
        /// 重写OnConfiguring 设置数据库链接
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var ConnectionStrings = ConfigurationHelper.GetAppSettings<ConnectionStrings>("ConnectionStrings");
                optionsBuilder.UseSqlServer(ConnectionStrings.SqlServerConnection);
            }
        }
        /// <summary>
        /// 在重写的OnModelCreating方法中，使用Fluent API来设置实体V_Person和数据库中V_Person视图的关系
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //先调用基类的OnModelCreating方法，设置数据库中其它表和实体的映射关系
            base.OnModelCreating(modelBuilder);
            //接着设置实体V_Person和数据库中V_Person视图的关系
            modelBuilder.Entity<A_Sys_Test>(entity =>
            {
                //告诉EF Core实体V_Person对应数据库中的V_Person视图，这里使用entity.ToTable方法后，上面的DbSet<V_Person> V_Person集合属性可以叫任何名字，比如我们可以将其定义为DbSet<V_Person> V_People也可以，如果不使用entity.ToTable方法，那么DbSet<V_Person> V_Person的属性名字必须和数据库视图V_Person的名字相同，否则EF Core会报错
                entity.ToTable("A_Sys_Test");
                //设置实体的唯一属性，因为我们知道数据库中V_Person视图的ID列值是唯一的，所以这里我们设置实体V_Person的Id属性为唯一属性
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.StaffID });
                //利用Fluent API将实体V_Person的每一列映射到数据库视图的每一列
                entity.Property(e => e.StaffName).HasMaxLength(20);
                entity.Property(e => e.CreateDate).HasColumnType("datetime");
            });
            //媒体资源文件视图表
            modelBuilder.Entity<V_Basics_Media>(entity => 
            {
                entity.ToTable("V_Basics_Media");
                entity.HasKey(e => new { e.SystemID, e.CompanyID, e.MediaID });
                entity.Property(e => e.CreateDate).HasColumnType("datetime");
            });



        }

        public ArrayList AA_Get_Sys_Test(int systemId, string companyId, string staffId, out int errCode, out string errMsg)
        {
            try
            {
                string cmdText = "AA_Get_Sys_Test";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.NVarChar,20),
                    new SqlParameter("@staffId", SqlDbType.NVarChar,20),
                    new SqlParameter("@errCode", SqlDbType.Int,4),
                    new SqlParameter("@errMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = staffId;
                param[3].Direction = ParameterDirection.Output;
                param[4].Direction = ParameterDirection.Output;
                var result = this.ExecuteReaderPro(cmdText, param);
                errCode = (int)param[3].Value;
                errMsg = (string)param[4].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public ArrayList AA_Get_Sys_TestAsync(int systemId, string companyId, string staffId, out int errCode, out string errMsg)
        {
            try
            {
                string cmdText = "AA_Get_Sys_Test";
                SqlParameter[] param = new SqlParameter[]
                {
                    new SqlParameter("@systemId",SqlDbType.Int,4),
                    new SqlParameter("@companyId", SqlDbType.NVarChar,20),
                    new SqlParameter("@staffId", SqlDbType.NVarChar,20),
                    new SqlParameter("@errCode", SqlDbType.Int,4),
                    new SqlParameter("@errMsg", SqlDbType.NVarChar,400)
                };
                param[0].Value = systemId;
                param[1].Value = companyId;
                param[2].Value = staffId;
                param[3].Direction = ParameterDirection.Output;
                param[4].Direction = ParameterDirection.Output;
                //this.ExecuteReaderProAsync("SP_Get_Sys_Test", param).Wait();                    //等待async方法执行完成
                var result = this.ExecuteReaderProAsync(cmdText, param).GetAwaiter().GetResult(); //用于同步获取async方法的返回结果
                errCode = (int)param[3].Value;
                errMsg = (string)param[4].Value;
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        

    }
}
