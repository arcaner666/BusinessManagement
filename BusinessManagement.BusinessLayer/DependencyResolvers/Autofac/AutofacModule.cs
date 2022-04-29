using Autofac;
using Autofac.Extras.DynamicProxy;
using BusinessManagement.BusinessLayer.Abstract;
using BusinessManagement.BusinessLayer.Concrete;
using BusinessManagement.BusinessLayer.CrossCuttingConcerns.Logging;
using BusinessManagement.BusinessLayer.Utilities.Interceptors;
using BusinessManagement.BusinessLayer.Utilities.Security.Cryptography;
using BusinessManagement.BusinessLayer.Utilities.Security.JWT;
using BusinessManagement.DataAccessLayer.Abstract;
using BusinessManagement.DataAccessLayer.Concrete.Dapper.MicrosoftSqlServer;
using Castle.DynamicProxy;

namespace BusinessManagement.BusinessLayer.DependencyResolvers.Autofac;

public class AutofacModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<AccountBl>().As<IAccountBl>().SingleInstance();
        builder.RegisterType<AccountExtBl>().As<IAccountExtBl>().SingleInstance();
        builder.RegisterType<DpMsAccountDal>().As<IAccountDal>().SingleInstance();

        builder.RegisterType<AccountGroupBl>().As<IAccountGroupBl>().SingleInstance();
        builder.RegisterType<DpMsAccountGroupDal>().As<IAccountGroupDal>().SingleInstance();

        builder.RegisterType<AccountTypeBl>().As<IAccountTypeBl>().SingleInstance();
        builder.RegisterType<DpMsAccountTypeDal>().As<IAccountTypeDal>().SingleInstance();

        builder.RegisterType<ApartmentBl>().As<IApartmentBl>().SingleInstance();
        builder.RegisterType<ApartmentExtBl>().As<IApartmentExtBl>().SingleInstance();
        builder.RegisterType<DpMsApartmentDal>().As<IApartmentDal>().SingleInstance();

        builder.RegisterType<AuthorizationBl>().As<IAuthorizationBl>().SingleInstance();

        builder.RegisterType<BranchBl>().As<IBranchBl>().SingleInstance();
        builder.RegisterType<BranchExtBl>().As<IBranchExtBl>().SingleInstance();
        builder.RegisterType<DpMsBranchDal>().As<IBranchDal>().SingleInstance();

        builder.RegisterType<BusinessBl>().As<IBusinessBl>().SingleInstance();
        builder.RegisterType<DpMsBusinessDal>().As<IBusinessDal>().SingleInstance();

        builder.RegisterType<CashBl>().As<ICashBl>().SingleInstance();
        builder.RegisterType<CashExtBl>().As<ICashExtBl>().SingleInstance();
        builder.RegisterType<DpMsCashDal>().As<ICashDal>().SingleInstance();

        builder.RegisterType<CityBl>().As<ICityBl>().SingleInstance();
        builder.RegisterType<DpMsCityDal>().As<ICityDal>().SingleInstance();

        builder.RegisterType<CurrencyBl>().As<ICurrencyBl>().SingleInstance();
        builder.RegisterType<DpMsCurrencyDal>().As<ICurrencyDal>().SingleInstance();

        builder.RegisterType<DistrictBl>().As<IDistrictBl>().SingleInstance();
        builder.RegisterType<DpMsDistrictDal>().As<IDistrictDal>().SingleInstance();

        builder.RegisterType<FlatBl>().As<IFlatBl>().SingleInstance();
        builder.RegisterType<FlatExtBl>().As<IFlatExtBl>().SingleInstance();
        builder.RegisterType<DpMsFlatDal>().As<IFlatDal>().SingleInstance();

        builder.RegisterType<FullAddressBl>().As<IFullAddressBl>().SingleInstance();
        builder.RegisterType<DpMsFullAddressDal>().As<IFullAddressDal>().SingleInstance();

        builder.RegisterType<EmployeeBl>().As<IEmployeeBl>().SingleInstance();
        builder.RegisterType<EmployeeExtBl>().As<IEmployeeExtBl>().SingleInstance();
        builder.RegisterType<DpMsEmployeeDal>().As<IEmployeeDal>().SingleInstance();

        builder.RegisterType<EmployeeTypeBl>().As<IEmployeeTypeBl>().SingleInstance();
        builder.RegisterType<DpMsEmployeeTypeDal>().As<IEmployeeTypeDal>().SingleInstance();

        builder.RegisterType<HouseOwnerBl>().As<IHouseOwnerBl>().SingleInstance();
        builder.RegisterType<HouseOwnerExtBl>().As<IHouseOwnerExtBl>().SingleInstance();
        builder.RegisterType<DpMsHouseOwnerDal>().As<IHouseOwnerDal>().SingleInstance();

        builder.RegisterType<JwtHelper>().As<ITokenService>();

        builder.RegisterType<KeyService>().As<IKeyService>();

        builder.RegisterType<LoggerManager>().As<ILoggerManager>().SingleInstance();

        builder.RegisterType<ManagerBl>().As<IManagerBl>().SingleInstance();
        builder.RegisterType<DpMsManagerDal>().As<IManagerDal>().SingleInstance();

        builder.RegisterType<OperationClaimBl>().As<IOperationClaimBl>().SingleInstance();
        builder.RegisterType<DpMsOperationClaimDal>().As<IOperationClaimDal>().SingleInstance();

        builder.RegisterType<SectionBl>().As<ISectionBl>().SingleInstance();
        builder.RegisterType<SectionExtBl>().As<ISectionExtBl>().SingleInstance();
        builder.RegisterType<DpMsSectionDal>().As<ISectionDal>().SingleInstance();

        builder.RegisterType<SectionGroupBl>().As<ISectionGroupBl>().SingleInstance();
        builder.RegisterType<DpMsSectionGroupDal>().As<ISectionGroupDal>().SingleInstance();

        builder.RegisterType<SystemUserBl>().As<ISystemUserBl>().SingleInstance();
        builder.RegisterType<DpMsSystemUserDal>().As<ISystemUserDal>().SingleInstance();

        builder.RegisterType<SystemUserClaimBl>().As<ISystemUserClaimBl>().SingleInstance();
        builder.RegisterType<DpMsSystemUserClaimDal>().As<ISystemUserClaimDal>().SingleInstance();

        builder.RegisterType<TenantBl>().As<ITenantBl>().SingleInstance();
        builder.RegisterType<DpMsTenantDal>().As<ITenantDal>().SingleInstance();

        var assembly = System.Reflection.Assembly.GetExecutingAssembly();

        builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
            .EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();
    }
}
