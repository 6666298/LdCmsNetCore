using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LdCms.Web
{
    using Autofac;
    using LdCms.EF.DbEntitiesContext;
    using LdCms.EF.DbModels;
    using LdCms.Web.Services;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Logging;

    public class AutofacModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // The generic ILogger<TCategoryName> service was added to the ServiceCollection by ASP.NET Core.
            // It was then registered with Autofac using the Populate method in ConfigureServices.
            //builder.Register(c => new ValuesService(c.Resolve<ILogger<ValuesService>>())).As<IValuesService>().InstancePerLifetimeScope();
            builder.Register(c => new BaseManager(c.Resolve<IHttpContextAccessor>(), c.Resolve<IBLL.Sys.IOperatorService>())).As<IBaseManager>().InstancePerLifetimeScope();
            builder.Register(c => new BaseApiManager(c.Resolve<IApiRecordManager>(), c.Resolve<IBLL.Sys.IInterfaceAccountService>(), c.Resolve<IBLL.Sys.IInterfaceAccessTokenService>(), c.Resolve<IBLL.Member.IAccountService>(), c.Resolve<IBLL.Member.IAccessTokenService>())).As<IBaseApiManager>().InstancePerLifetimeScope();
            builder.Register(c => new ApiRecordManager(c.Resolve<IHttpContextAccessor>(), c.Resolve<IBLL.Sys.IInterfaceAccountService>(), c.Resolve<IBLL.Sys.IInterfaceAccessTokenService>(), c.Resolve<IBLL.Member.IAccessTokenService>(), c.Resolve<IBLL.Log.IWebApiAccessRecordService>())).As<IApiRecordManager>().InstancePerLifetimeScope();

            #region 操作日志类注册容器 Manager
            //系统部分
            builder.Register(c => new TableOperationManager<Ld_Sys_AccessCorsHost>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Sys_AccessCorsHost>>().InstancePerLifetimeScope();
            builder.Register(c => new TableOperationManager<Ld_Sys_Code>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Sys_Code>>().InstancePerLifetimeScope();
            builder.Register(c => new TableOperationManager<Ld_Sys_Config>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Sys_Config>>().InstancePerLifetimeScope();
            builder.Register(c => new TableOperationManager<Ld_Sys_Function>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Sys_Function>>().InstancePerLifetimeScope();
            builder.Register(c => new TableOperationManager<Ld_Sys_InterfaceAccessToken>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Sys_InterfaceAccessToken>>().InstancePerLifetimeScope();
            builder.Register(c => new TableOperationManager<Ld_Sys_InterfaceAccessWhiteList>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Sys_InterfaceAccessWhiteList>>().InstancePerLifetimeScope();
            builder.Register(c => new TableOperationManager<Ld_Sys_InterfaceAccount>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Sys_InterfaceAccount>>().InstancePerLifetimeScope();
            builder.Register(c => new TableOperationManager<Ld_Sys_Operator>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Sys_Operator>>().InstancePerLifetimeScope();
            builder.Register(c => new TableOperationManager<Ld_Sys_OperatorRole>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Sys_OperatorRole>>().InstancePerLifetimeScope();
            builder.Register(c => new TableOperationManager<Ld_Sys_Role>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Sys_Role>>().InstancePerLifetimeScope();
            builder.Register(c => new TableOperationManager<Ld_Sys_RoleFunction>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Sys_RoleFunction>>().InstancePerLifetimeScope();
            builder.Register(c => new TableOperationManager<Ld_Sys_Version>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Sys_Version>>().InstancePerLifetimeScope();

            //公司部分
            builder.Register(c => new TableOperationManager<Ld_Institution_Company>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Institution_Company>>().InstancePerLifetimeScope();
            builder.Register(c => new TableOperationManager<Ld_Institution_Dealer>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Institution_Dealer>>().InstancePerLifetimeScope();
            builder.Register(c => new TableOperationManager<Ld_Institution_Department>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Institution_Department>>().InstancePerLifetimeScope();
            builder.Register(c => new TableOperationManager<Ld_Institution_Position>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Institution_Position>>().InstancePerLifetimeScope();
            builder.Register(c => new TableOperationManager<Ld_Institution_Staff>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Institution_Staff>>().InstancePerLifetimeScope();
            builder.Register(c => new TableOperationManager<Ld_Institution_Store>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Institution_Store>>().InstancePerLifetimeScope();
            builder.Register(c => new TableOperationManager<Ld_Institution_Supplier>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Institution_Supplier>>().InstancePerLifetimeScope();
            builder.Register(c => new TableOperationManager<Ld_Institution_Warehouse>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Institution_Warehouse>>().InstancePerLifetimeScope();


            //日志部分
            builder.Register(c => new TableOperationManager<Ld_Log_ErrorRecord>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Log_ErrorRecord>>().InstancePerLifetimeScope();
            builder.Register(c => new TableOperationManager<Ld_Log_LoginRecord>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Log_LoginRecord>>().InstancePerLifetimeScope();
            builder.Register(c => new TableOperationManager<Ld_Log_Table>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Log_Table>>().InstancePerLifetimeScope();
            builder.Register(c => new TableOperationManager<Ld_Log_TableDetails>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Log_TableDetails>>().InstancePerLifetimeScope();
            builder.Register(c => new TableOperationManager<Ld_Log_TableOperation>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Log_TableOperation>>().InstancePerLifetimeScope();
            builder.Register(c => new TableOperationManager<Ld_Log_VisitorRecord>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Log_VisitorRecord>>().InstancePerLifetimeScope();
            builder.Register(c => new TableOperationManager<Ld_Log_WebApiAccessRecord>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Log_WebApiAccessRecord>>().InstancePerLifetimeScope();

            //会员部分
            builder.Register(c => new TableOperationManager<Ld_Member_Rank>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Member_Rank>>().InstancePerLifetimeScope();


            //基础公共部分
            builder.Register(c => new TableOperationManager<Ld_Basics_Media>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Basics_Media>>().InstancePerLifetimeScope();
            builder.Register(c => new TableOperationManager<Ld_Basics_MediaInterface>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Basics_MediaInterface>>().InstancePerLifetimeScope();
            builder.Register(c => new TableOperationManager<Ld_Basics_MediaMember>(c.Resolve<IBLL.Log.ITableService>(), c.Resolve<IBLL.Log.ITableDetailsService>(), c.Resolve<IBLL.Log.ITableOperationService>())).As<ITableOperationManager<Ld_Basics_MediaMember>>().InstancePerLifetimeScope();

            #endregion


            #region 正式注册容器 DAL
            builder.Register(c => new DAL.Sys.AccessCorsHostDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Sys.IAccessCorsHostDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Sys.CodeDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Sys.ICodeDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Sys.ConfigDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Sys.IConfigDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Sys.FunctionDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Sys.IFunctionDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Sys.RoleDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Sys.IRoleDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Sys.OperatorDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Sys.IOperatorDAL>().InstancePerLifetimeScope();

            builder.Register(c => new DAL.Sys.InterfaceAccountDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Sys.IInterfaceAccountDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Sys.InterfaceAccessWhiteListDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Sys.IInterfaceAccessWhiteListDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Sys.InterfaceAccessTokenDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Sys.IInterfaceAccessTokenDAL>().InstancePerLifetimeScope();

            builder.Register(c => new DAL.Log.TableDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Log.ITableDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Log.TableDetailsDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Log.ITableDetailsDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Log.TableOperationDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Log.ITableOperationDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Log.ErrorRecordDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Log.IErrorRecordDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Log.LoginRecordDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Log.ILoginRecordDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Log.VisitorRecordDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Log.IVisitorRecordDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Log.WebApiAccessRecordDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Log.IWebApiAccessRecordDAL>().InstancePerLifetimeScope();

            builder.Register(c => new DAL.Institution.CompanyDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Institution.ICompanyDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Institution.DepartmentDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Institution.IDepartmentDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Institution.PositionDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Institution.IPositionDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Institution.StoreDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Institution.IStoreDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Institution.StaffDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Institution.IStaffDAL>().InstancePerLifetimeScope();

            builder.Register(c => new DAL.Member.RankDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Member.IRankDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Member.AccountDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Member.IAccountDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Member.AccessTokenDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Member.IAccessTokenDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Member.LoginLogsDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Member.ILoginLogsDAL>().InstancePerLifetimeScope();

            builder.Register(c => new DAL.Service.MessageBoardDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Service.IMessageBoardDAL>().InstancePerLifetimeScope();

            builder.Register(c => new DAL.Basics.MediaDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Basics.IMediaDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Basics.MediaInterfaceDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Basics.IMediaInterfaceDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Basics.MediaMemberDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Basics.IMediaMemberDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Basics.VMediaDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Basics.IVMediaDAL>().InstancePerLifetimeScope();

            builder.Register(c => new DAL.Info.NoticeCategoryDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Info.INoticeCategoryDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Info.NoticeDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Info.INoticeDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Info.BlockDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Info.IBlockDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Info.ClassDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Info.IClassDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Info.PageDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Info.IPageDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Info.ArticeDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Info.IArticeDAL>().InstancePerLifetimeScope();

            builder.Register(c => new DAL.Extend.SearchKeywordDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Extend.ISearchKeywordDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Extend.AdvertisementDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Extend.IAdvertisementDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Extend.AdvertisementDetailsDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Extend.IAdvertisementDetailsDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Extend.LinkDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Extend.ILinkDAL>().InstancePerLifetimeScope();
            builder.Register(c => new DAL.Extend.LinkGroupDAL(c.Resolve<LdCmsDbEntitiesContext>())).As<IDAL.Extend.ILinkGroupDAL>().InstancePerLifetimeScope();

            #endregion


            #region 正式注册容器 BLL
            builder.Register(c => new BLL.Sys.AccessCorsHostService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Sys.IAccessCorsHostDAL>())).As<IBLL.Sys.IAccessCorsHostService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Sys.CodeService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Sys.ICodeDAL>())).As<IBLL.Sys.ICodeService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Sys.ConfigService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Sys.IConfigDAL>())).As<IBLL.Sys.IConfigService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Sys.FunctionService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Sys.IFunctionDAL>())).As<IBLL.Sys.IFunctionService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Sys.RoleService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Sys.IRoleDAL>())).As<IBLL.Sys.IRoleService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Sys.OperatorService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Sys.IOperatorDAL>())).As<IBLL.Sys.IOperatorService>().InstancePerLifetimeScope();

            builder.Register(c => new BLL.Sys.InterfaceAccountService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Sys.IInterfaceAccountDAL>())).As<IBLL.Sys.IInterfaceAccountService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Sys.InterfaceAccessWhiteListService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Sys.IInterfaceAccessWhiteListDAL>())).As<IBLL.Sys.IInterfaceAccessWhiteListService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Sys.InterfaceAccessTokenService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Sys.IInterfaceAccessTokenDAL>())).As<IBLL.Sys.IInterfaceAccessTokenService>().InstancePerLifetimeScope();

            builder.Register(c => new BLL.Log.TableService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Log.ITableDAL>())).As<IBLL.Log.ITableService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Log.TableDetailsService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Log.ITableDetailsDAL>())).As<IBLL.Log.ITableDetailsService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Log.TableOperationService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Log.ITableOperationDAL>())).As<IBLL.Log.ITableOperationService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Log.ErrorRecordService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Log.IErrorRecordDAL>())).As<IBLL.Log.IErrorRecordService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Log.LoginRecordService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Log.ILoginRecordDAL>())).As<IBLL.Log.ILoginRecordService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Log.VisitorRecordService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Log.IVisitorRecordDAL>())).As<IBLL.Log.IVisitorRecordService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Log.WebApiAccessRecordService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Log.IWebApiAccessRecordDAL>())).As<IBLL.Log.IWebApiAccessRecordService>().InstancePerLifetimeScope();

            builder.Register(c => new BLL.Institution.CompanyService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Institution.ICompanyDAL>())).As<IBLL.Institution.ICompanyService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Institution.DepartmentService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Institution.IDepartmentDAL>())).As<IBLL.Institution.IDepartmentService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Institution.PositionService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Institution.IPositionDAL>())).As<IBLL.Institution.IPositionService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Institution.StoreService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Institution.IStoreDAL>())).As<IBLL.Institution.IStoreService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Institution.StaffService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Institution.IStaffDAL>())).As<IBLL.Institution.IStaffService>().InstancePerLifetimeScope();

            builder.Register(c => new BLL.Member.RankService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Member.IRankDAL>())).As<IBLL.Member.IRankService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Member.AccountService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Member.IAccountDAL>())).As<IBLL.Member.IAccountService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Member.AccessTokenService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Member.IAccessTokenDAL>())).As<IBLL.Member.IAccessTokenService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Member.LoginLogsService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Member.ILoginLogsDAL>())).As<IBLL.Member.ILoginLogsService>().InstancePerLifetimeScope();

            builder.Register(c => new BLL.Service.MessageBoardService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Service.IMessageBoardDAL>())).As<IBLL.Service.IMessageBoardService>().InstancePerLifetimeScope();

            builder.Register(c => new BLL.Basics.MediaService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Basics.IMediaDAL>())).As<IBLL.Basics.IMediaService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Basics.MediaInterfaceService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Basics.IMediaInterfaceDAL>())).As<IBLL.Basics.IMediaInterfaceService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Basics.MediaMemberService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Basics.IMediaMemberDAL>())).As<IBLL.Basics.IMediaMemberService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Basics.VMediaService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Basics.IVMediaDAL>())).As<IBLL.Basics.IVMediaService>().InstancePerLifetimeScope();

            builder.Register(c => new BLL.Info.NoticeCategoryService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Info.INoticeCategoryDAL>())).As<IBLL.Info.INoticeCategoryService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Info.NoticeService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Info.INoticeDAL>())).As<IBLL.Info.INoticeService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Info.BlockService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Info.IBlockDAL>())).As<IBLL.Info.IBlockService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Info.ClassService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Info.IClassDAL>())).As<IBLL.Info.IClassService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Info.PageService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Info.IPageDAL>())).As<IBLL.Info.IPageService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Info.ArticeService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Info.IArticeDAL>())).As<IBLL.Info.IArticeService>().InstancePerLifetimeScope();


            builder.Register(c => new BLL.Extend.SearchKeywordService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Extend.ISearchKeywordDAL>())).As<IBLL.Extend.ISearchKeywordService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Extend.AdvertisementService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Extend.IAdvertisementDAL>())).As<IBLL.Extend.IAdvertisementService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Extend.AdvertisementDetailsService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Extend.IAdvertisementDetailsDAL>())).As<IBLL.Extend.IAdvertisementDetailsService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Extend.LinkService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Extend.ILinkDAL>())).As<IBLL.Extend.ILinkService>().InstancePerLifetimeScope();
            builder.Register(c => new BLL.Extend.LinkGroupService(c.Resolve<LdCmsDbEntitiesContext>(), c.Resolve<IDAL.Extend.ILinkGroupDAL>())).As<IBLL.Extend.ILinkGroupService>().InstancePerLifetimeScope();

            #endregion


        }
    }
}
