using Memorq.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Memorq.Views
{
    public partial class CategoryManager : Window
    {
        public CategoryManager(CategoryManagerViewModel categoryManagerViewModel)
        {
            InitializeComponent();
            DataContext = categoryManagerViewModel;
            categoryManagerViewModel.OwnerWindow = this;
        }

        private void ChooseCategoryBtnClick(object sender, RoutedEventArgs e)
        {
            if (CategoryListView.SelectedIndex > -1)
            {
                DialogResult = true;
            }
            else
            {
                MessageBox.Show((string)Application.Current.FindResource("MsgCategoryNotChosen"), "Memorq", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}

