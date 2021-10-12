using Ninject;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Deployment.Application;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Markup;

namespace SirCoPOS.Win
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        Actualizacion.SirCoPOSUpdate SPU = new Actualizacion.SirCoPOSUpdate();
        public App()
        {
            SPU.actualizasirco();
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
        }
        private Mutex _mutex;
        protected override void OnExit(ExitEventArgs e)
        {            
            base.OnExit(e);
            try
            {
                _mutex.ReleaseMutex();
            }
            catch { }
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            bool isnew;
            _mutex = new Mutex(true, String.Intern("SirCoPOS"), out isnew);

            //if (!isnew || !_mutex.WaitOne(0))
            //{
            //    _mutex.Close();
            //    //Microsoft.VisualBasic.Interaction.MsgBox("Application instance is already running!");
            //    MessageBox.Show("La instrancia de la aplicación ya se está ejecutando!!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            //    Shutdown();
            //    return;
            //}

            var culture = new CultureInfo("es-MX");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            FrameworkElement.LanguageProperty.OverrideMetadata(
                typeof(FrameworkElement),
                new FrameworkPropertyMetadata(
                    XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

            /*
             var gi = new GenericIdentity(w.UserName, "Framework");
                        var gp = new GenericPrincipal(gi, roles);
                        Thread.CurrentPrincipal = gp;

                        AppDomain.CurrentDomain.SetThreadPrincipal(gp);
             */

            var sl = Helpers.NinjectBootstrapper.Current;

            var config = new Common.MappingConfig();
            config.Export<Mappings.Mapping>();            

            var mef = Utilities.Helpers.Singleton<Helpers.PlugInServiceLocator>.Instance;
            mef.Export(Assembly.GetExecutingAssembly());
            if (ApplicationDeployment.IsNetworkDeployed)
            {
                //var del = System.IO.Directory.GetFiles(@"SirCoPOS.Client.*");
                //foreach (var item in del)
                //{
                //    System.IO.File.Delete(item);
                //}

                var sa = new Helpers.SyncAgent();
                var success = sa.Sync();
                if (!success)
                {
                    MessageBox.Show("FileSync: ERROR - archivo en uso.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    this.Shutdown();
                    return;
                }

                mef.Export(@"sync\SirCoPOS.Client");
                config.Export(@"sync\SirCoPOS.Client");
            }
            else
            {
                mef.Export("SirCoPOS.Client");
                config.Export("SirCoPOS.Client");
            }

            var mapper = config.Create();
            Helpers.NinjectBootstrapper.Kernel.Bind<AutoMapper.IMapper>().ToConstant(mapper);

            RegisterExceptionHandlers();
        }

        private void RegisterExceptionHandlers()
        {                        
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            Application.Current.Dispatcher.UnhandledExceptionFilter += Dispatcher_UnhandledExceptionFilter; ;
            TaskScheduler.UnobservedTaskException += TaskScheduler_UnobservedTaskException; ;
        }

        private void TaskScheduler_UnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            LogError("TaskScheduler_UnobservedTaskException", e.Exception);
        }

        private void Dispatcher_UnhandledExceptionFilter(object sender, System.Windows.Threading.DispatcherUnhandledExceptionFilterEventArgs e)
        {
            LogError("Dispatcher_UnhandledExceptionFilter", e.Exception);
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            LogError("Current_DispatcherUnhandledException", e.Exception);
        }        
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var ex = e.ExceptionObject as Exception;
            this.LogError("CurrentDomain_UnhandledException", ex);
        }
        private void LogError(string msg, Exception ex)
        {
            var log = CommonServiceLocator.ServiceLocator.Current.GetInstance<ILogger>();
            
            log.Fatal(ex, msg);
        }
    }
}
