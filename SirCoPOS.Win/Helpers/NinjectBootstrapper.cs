using CommonServiceLocator;
using Ninject;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace SirCoPOS.Win.Helpers
{
    class NinjectBootstrapper
    {
        public static IKernel Kernel { get; private set; }
        static NinjectBootstrapper()
        {
            var kernel = new Ninject.StandardKernel();
            kernel.Settings.AllowNullInjection = true;
            RegisterServices(kernel);
            CommonServiceLocator.ServiceLocator.SetLocatorProvider(() => new Common.Extensions.NinjectServiceLocator(kernel));
            Kernel = kernel;
        }
        public static IServiceLocator Current 
        {
            get { 
                return CommonServiceLocator.ServiceLocator.Current; 
            } 
        }
        private static void RegisterServices(IKernel kernel)
        {
            //kernel.Load(Assembly.GetEntryAssembly());
            if (GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                kernel.Bind<Common.ServiceContracts.IPrintServiceAsync>().ToMethod(ctx => null);
                kernel.Bind<Common.ServiceContracts.IDataServiceAsync>().ToMethod(ctx => null);
                kernel.Bind<Common.ServiceContracts.IProcessServiceAsync>().ToMethod(ctx => null);
                kernel.Bind<Common.ServiceContracts.ICommonServiceAsync>().ToMethod(ctx => null);
                kernel.Bind<Common.ServiceContracts.IAdminServiceAsync>().ToMethod(ctx => null);
                kernel.Bind<Common.ServiceContracts.INoteServiceAsync>().ToMethod(ctx => null);

                //kernel.Bind<Client.ViewModels.Tabs.CajaViewModel>().To<DesignMode.ViewModels.CajaViewModel>();
            }
            else
            {
                kernel.Bind<Common.ServiceContracts.ISyncService>().ToMethod(ctx => {
                    return new System.ServiceModel.ChannelFactory<Common.ServiceContracts.ISyncService>("*").CreateChannel();
                });
                kernel.Bind<Common.ServiceContracts.IAdminServiceAsync>().ToMethod(ctx => {
                    return new System.ServiceModel.ChannelFactory<Common.ServiceContracts.IAdminServiceAsync>("*").CreateChannel();
                });
                kernel.Bind<Common.ServiceContracts.IPrintServiceAsync>().ToMethod(ctx => {
                    return new System.ServiceModel.ChannelFactory<Common.ServiceContracts.IPrintServiceAsync>("*").CreateChannel();
                });
                kernel.Bind<Common.ServiceContracts.ICommonServiceAsync>().ToMethod(ctx => {
                    return new System.ServiceModel.ChannelFactory<Common.ServiceContracts.ICommonServiceAsync>("*").CreateChannel();
                });
                kernel.Bind<Common.ServiceContracts.IDataServiceAsync>().ToMethod(ctx => {
                    return new System.ServiceModel.ChannelFactory<Common.ServiceContracts.IDataServiceAsync>("*").CreateChannel();
                });
                kernel.Bind<Common.ServiceContracts.INoteServiceAsync>().ToMethod(ctx => {
                    return new System.ServiceModel.ChannelFactory<Common.ServiceContracts.INoteServiceAsync>("*").CreateChannel();
                });
                kernel.Bind<Common.ServiceContracts.ICreditoValeServiceAsync>().ToMethod(ctx => {
                    return new System.ServiceModel.ChannelFactory<Common.ServiceContracts.ICreditoValeServiceAsync>("*").CreateChannel();
                });
                //kernel.Bind<Common.ServiceContracts.IServiceDuplex>().ToMethod(ctx => {
                //    var client = new System.ServiceModel.DuplexChannelFactory<Common.ServiceContracts.IServiceDuplex>(typeof(CallbackHandler), "*");
                //    client.Open();
                //    var callback = new CallbackHandler();
                //    var inst = new System.ServiceModel.InstanceContext(callback);
                //    var proxy = client.CreateChannel(inst);
                //    var com = (IClientChannel)proxy;
                //    com.Closed += (sender, e) =>
                //    {
                //        Console.WriteLine("channel closed");
                //    };
                //    com.Faulted += (sender, e) => {
                //        //executed
                //        Console.WriteLine("channel faulted");
                //    };
                //    return proxy;
                //});
            }
            kernel.Bind<Utilities.Interfaces.IImageView>().To<Helpers.ImageView>();
            kernel.Bind<Utilities.Interfaces.IClienteView>().To<Helpers.ClienteView>();
            kernel.Bind<Utilities.Interfaces.ILocalStorage>().To<Helpers.LocalStorage>();
            kernel.Bind<Utilities.Interfaces.IScanner>().To<Helpers.ScannerHelper>();
            kernel.Bind<Utilities.Interfaces.IReportViewer>().To<Helpers.ReportViewer>();
            kernel.Bind<Utilities.Models.Settings>().ToSelf().InSingletonScope();
            
            kernel.Bind<ILogger>().ToMethod(i => LogManager.GetCurrentClassLogger());            
        }
    }
}
