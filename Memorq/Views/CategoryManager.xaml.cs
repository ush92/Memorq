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
            DialogResult = true;
        }

        private void CategoryListViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryListView.SelectedIndex.Equals(-1))
            {
                ChooseCategoryBtn.IsEnabled = false;
                DeleteCategoryBtn.IsEnabled = false;
                ChangeCategoryNameBtn.IsEnabled = false;
            }
            else
            {
                ChooseCategoryBtn.IsEnabled = true;
                DeleteCategoryBtn.IsEnabled = true;
                ChangeCategoryNameBtn.IsEnabled = true;
            }
        }

        private void ItemListViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ItemListView.SelectedIndex.Equals(-1))
            {
                EditItemBtn.IsEnabled = false;
                DeleteItemBtn.IsEnabled = false;
            }
            else
            {
                EditItemBtn.IsEnabled = true;
                DeleteItemBtn.IsEnabled = true;
            }
        }
    }
}

