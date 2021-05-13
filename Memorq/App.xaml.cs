using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Memorq.Services;
using Memorq.ViewModels;
using Memorq.Views;
using Microsoft.Extensions.DependencyInjection;

namespace Memorq
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string databasePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "memorq.db");

        public IServiceProvider ServiceProvider { get; private set; }


        protected override void OnStartup(StartupEventArgs e)
        {
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<MainWindow>();
            services.AddTransient<MainWindowViewModel>();

            services.AddTransient<DebugWindow>();
            services.AddTransient<DebugWindowViewModel>();

            services.AddTransient<ICategoryProvider, CategoryProvider>();
        }
    }
}
