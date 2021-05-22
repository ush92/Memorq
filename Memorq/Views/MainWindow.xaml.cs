using Memorq.ViewModels;
using System.Windows;

namespace Memorq.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel mainWindowViewModel)
        {
            InitializeComponent();
            DataContext = mainWindowViewModel;
            mainWindowViewModel.OwnerWindow = this;
        }
    }
}