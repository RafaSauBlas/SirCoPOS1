using CommonServiceLocator;
using Ninject;
using Ninject.Web.Common.WebHost;
using Ninject.Extensions.Conventions;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;
using Elmah.Contrib.WebApi;

namespace SirCoPOS.Web
{
    public class Global : NinjectHttpApplication
    {        
        protected override void OnApplicationStarted()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            GlobalConfiguration.Configure(WebApiConfig.Register);

            WebApiTestClient.WebApiTestClientHttpMessageHandler.RegisterRouteForTestClient(GlobalConfiguration.Configuration);

            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.Filters.Add(new ElmahHandleErrorApiAttribute());

            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
        public Global()
        {
            this.Error += Global_Error;
        }
        private void Global_Error(object sender, EventArgs e)
        {
            var ex = this.Server.GetLastError();
            if (ex == null)
                return;

            //var log = ServiceLocator.Current.GetInstance<ILogger>();
            //log.Fatal(ex);
            Common.Logger.Fatal(ex, "Web");
        }
        protected override IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            RegisterServices(kernel);
            ServiceLocator.SetLocatorProvider(() => new Common.Extensions.NinjectServiceLocator(kernel));
            return kernel;
        }

        private void RegisterServices(StandardKernel kernel)
        {
            kernel.Bind(x =>
            {
                x.From(
                    "SirCoPOS.Common",
                    "SirCoPOS.BusinessLogic")
                 .SelectAllClasses()
                 .BindDefaultInterface();
            });
            kernel.Bind<ILogger>().ToMethod(i => LogManager.GetCurrentClassLogger());

            kernel.Bind<Common.ServiceContracts.ICreditoValeService>().ToMethod(i => new System.ServiceModel.ChannelFactory<Common.ServiceContracts.ICreditoValeService>("*").CreateChannel());            
        }
    }
}