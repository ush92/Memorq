using Memorq.ViewModels;
using System.Windows;

namespace Memorq.Views
{
    public partial class About : Window
    {
        public About(AboutViewModel aboutViewModel)
        {
            InitializeComponent();
            DataContext = aboutViewModel;
            aboutViewModel.OwnerWindow = this;
        }
    }
}
