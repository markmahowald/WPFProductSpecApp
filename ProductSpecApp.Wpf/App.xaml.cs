using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductSpecApp.Wpf.ViewModels;
using ProductSpecApp.Wpf.ViewModels.DisplayViewModels;
using ProductSpecificationApp.Data.EntityFrameworkClasses;
using ProductSpecificationApp.Data.Repositories;
using System;
using System.Windows;

namespace ProductSpecApp.Wpf
{
    public partial class App : Application
    {
        private IHost _host;

        public App()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    // Register your services, repositories, and view models here
                    services.AddDbContext<ProductSpecificationDbContext>();
                    services.AddSingleton<IProductSpecAppRepo, ProductRepository>();
                    services.AddSingleton<MainWindowViewModel>();
                    services.AddTransient<ProductDisplayViewModel>();
                    // Add other services and ViewModels as needed
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = new MainWindow
            {
                DataContext = _host.Services.GetRequiredService<MainWindowViewModel>()            
            };
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();
            base.OnExit(e);
        }
    }
}
