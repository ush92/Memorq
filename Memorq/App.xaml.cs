using Memorq.Infrastructure;
using Memorq.Services;
using Memorq.Views;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

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
            services.Scan(s => s.FromCallingAssembly()
                .AddClasses(c => c.AssignableToAny(typeof(Window), typeof(BaseViewModel)))
                .AsSelf()
                .WithTransientLifetime()); 

            services.AddTransient<ICategoryProvider, CategoryProvider>();
            services.AddTransient<IWindowFactory, WindowFactory>();
        }
    }
}
