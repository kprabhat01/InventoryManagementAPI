using AutoMapper;
using IL.Service.Core.AuthenticationService;
using IL.Service.Core.CommonService;
using IL.Service.Core.InwardService;
using IL.Service.Core.ItemService;
using IL.Service.Core.JobService;
using IL.Service.Core.LoggerService;
using IL.Service.Core.OutwardService;
using IL.Service.Core.PRService;
using IL.Service.Core.ReportService;
using IL.Service.Core.UserManagerService;
using Inventory.App_Code;
using System.Web.Http;
using Unity;
using Unity.Lifetime;
using Unity.WebApi;

namespace Inventory
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperBootstrap());
            });
            container.RegisterInstance<IMapper>(config.CreateMapper());
            container.RegisterType<IUserManagerService, UserManagerService>(new PerResolveLifetimeManager());
            container.RegisterType<IAuthenticationService, AuthenticationService>(new PerResolveLifetimeManager());
            container.RegisterType<ICommonService, CommonService>(new PerResolveLifetimeManager());
            container.RegisterType<IItemService, ItemService>(new PerResolveLifetimeManager());
            container.RegisterType<IPRService, PRService>(new PerResolveLifetimeManager());
            container.RegisterType<ILoggerService, LoggerService>(new PerResolveLifetimeManager());
            container.RegisterType<IOutwardService, OutwardService>(new PerResolveLifetimeManager());
            container.RegisterType<IMaterialInwards, MaterialInwards>(new PerResolveLifetimeManager());
            container.RegisterType<IJobService, JobService>();
            container.RegisterType<IReportService, ReportService>();
            var job = container.Resolve<IJobService>();

            job.RunService();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);
        }
    }
}