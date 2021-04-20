using Memorq.Models;
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

namespace Memorq
{
    /// <summary>
    /// Interaction logic for DebugWindow.xaml
    /// </summary>
    public partial class DebugWindow : Window
    {
        List<Category> categories;
        public DebugWindow()
        {
            InitializeComponent();

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


/*       
       algorithm SM-2 is
input:  user grade q
       repetition number n
       easiness factor EF
       interval I
output: updated values of n, EF, and I

if q ≥ 3 (correct response) then
   if n = 0 then
       I ← 1
   else if n = 1 then
       I ← 6
   else
       I ← ⌈I × EF⌉
   end if
   EF ← EF + (0.1 − (5 − q) × (0.08 + (5 − q) × 0.02))
   if EF < 1.3 then
       EF ← 1.3
   end if
   increment n
else (incorrect response)
   n ← 0
   I ← 1
end if

return (n, EF, I)

*/
