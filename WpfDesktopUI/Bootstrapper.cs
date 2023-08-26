using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Windows;
using WpfDesktopUI.ViewModels;

namespace WpfDesktopUI
{
    
    public class Bootstrapper : BootstrapperBase
    {

        private readonly SimpleContainer _container = new();

        [SupportedOSPlatform("windows")]
        public Bootstrapper()
        {
            Initialize();
            LogManager.GetLog = type => new DebugLog(type);
        }

        [SupportedOSPlatform("windows")]
        protected override void OnStartup(object sender, StartupEventArgs e)
        {
            // Launch ShellViewModel.xaml instead of classic MainWindow.xaml
            DisplayRootViewForAsync<ShellViewModel>();
        }

        [SupportedOSPlatform("windows")]
        protected override void Configure()
        {
            _container.Instance(_container);

            _container
                .Singleton<IWindowManager, WindowManager>()
                .Singleton<IEventAggregator, EventAggregator>();

            GetType().Assembly
                     .GetTypes()
                     .Where(type => type.IsClass && type.Name.EndsWith("ViewModel"))
                     .ToList()
                     .ForEach(viewmodelType => _container.RegisterSingleton(
                                               viewmodelType,
                                               viewmodelType.ToString(),
                                               viewmodelType));
        }

        protected override object GetInstance(Type service, string key)
        {
            return _container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return _container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            _container.BuildUp(instance);
        }

    }
}
