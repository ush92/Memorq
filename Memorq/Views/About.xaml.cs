using Memorq.ViewModels;
using System.Windows;

namespace Memorq.Views
{
    public partial class About : Window
    {
        public About(AboutWindowViewModel aboutWindowViewModel)
        {
            InitializeComponent();
            DataContext = aboutWindowViewModel;
            aboutWindowViewModel.OwnerWindow = this;
        }
    }
}
