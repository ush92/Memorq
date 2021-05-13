using Memorq.Models;
using Memorq.ViewModels;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Memorq.Views
{
    /// <summary>
    /// Interaction logic for DebugWindow.xaml
    /// </summary>
    public partial class DebugWindow : Window
    {
        List<Category> categories;
        public DebugWindow(DebugWindowViewModel debugWindowViewModel)
        {
            InitializeComponent();

            this.DataContext = debugWindowViewModel;

            categories = new List<Category>();
        }

        private void insertDBtestBtn_Click(object sender, RoutedEventArgs e)
        {
            Category category = new Category()
            {
                Name = insertDBtxt.Text
            };

            if (category.Name.Equals(string.Empty)) category.Name = "default";

            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Category>();
                connection.Insert(category);
            }
        }

        private void readDBbtn_Click(object sender, RoutedEventArgs e)
        {
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Category>();
                categories = (connection.Table<Category>().ToList()).OrderBy(c => c.Name).ToList();
            }

            if (categories != null)
            {
                categoriesListView.ItemsSource = categories;
            }

            // Programmatic use of string resource from Dictionary.xaml resource dictionary
            string localizedMessage = (string)Application.Current.FindResource("NewBtn_right");
            MessageBox.Show(localizedMessage);
        }
    }
}
