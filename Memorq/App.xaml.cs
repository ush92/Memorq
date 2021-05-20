using Memorq.Infrastructure;
using Memorq.Models;
using Memorq.Services;
using Memorq.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SQLite;
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
        public IConfiguration Configuration { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            Configuration = ConfigurationFactory.GetConfiguration();

            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            ServiceProvider = serviceCollection.BuildServiceProvider();

            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Category>();
                connection.CreateTable<Item>();
            }

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.Configure<AppSettings>(Configuration.GetSection(nameof(AppSettings)));

            services.Scan(s => s.FromCallingAssembly()
                .AddClasses(c => c.AssignableToAny(typeof(Window), typeof(BaseViewModel)))
                .AsSelf()
                .WithTransientLifetime()); 

            services.AddTransient<ICategoryProvider, CategoryProvider>();
            services.AddTransient<IItemProvider, ItemProvider>();
            services.AddTransient<IWindowFactory, WindowFactory>();
        }
    }
}
