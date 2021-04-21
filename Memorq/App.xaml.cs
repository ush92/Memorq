using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Memorq.Views;

namespace Memorq
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string databasePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "memorq.db");
    }
}
