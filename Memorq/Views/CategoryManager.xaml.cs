using Memorq.ViewModels;
using System.Windows;

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
    }
}

