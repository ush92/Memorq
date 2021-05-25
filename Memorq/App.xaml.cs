using Memorq.Core;
using Memorq.Infrastructure;
using Memorq.Models;
using Memorq.Services;
using Memorq.Views;
using Microsoft.Extensions.DependencyInjection;
using SQLite;
using System;
using System.Windows;

namespace Memorq
{
    public partial class App : Application
    {
        public static string databasePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "memorq.db");

        public IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
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
            services.Scan(s => s.FromCallingAssembly()
                .AddClasses(c => c.AssignableToAny(typeof(Window), typeof(BaseViewModel)))
                .AsSelf()
                .WithTransientLifetime());

            services.AddTransient<IStringResourcesDictionary, StringResourcesDictionary>();
            services.AddTransient<ICategoryService, CategoryService>();
            services.AddTransient<IItemService, ItemService>();
            services.AddTransient<IWindowFactory, WindowFactory>();
            services.AddTransient<IMemorqCore, MemorqCore>();
        }
    }
}