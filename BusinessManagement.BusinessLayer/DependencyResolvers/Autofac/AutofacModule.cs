using Autofac;
using Autofac.Extras.DynamicProxy;
using BusinessManagement.BusinessLayer.Utilities.Interceptors;
using Castle.DynamicProxy;

namespace BusinessManagement.BusinessLayer.DependencyResolvers.Autofac
{
    public class AutofacModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<AccountBl>().As<IAccountBl>().SingleInstance();
            //builder.RegisterType<DpMsAccountDal>().As<IAccountDal>().SingleInstance();

            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
                {
                    Selector = new AspectInterceptorSelector()
                }).SingleInstance();
        }
    }
}
