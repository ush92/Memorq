using Memorq.ViewModels;
using System.Windows;

namespace Memorq.Views
{
    public partial class DebugWindow : Window
    {
        public DebugWindow(DebugWindowViewModel debugWindowViewModel)
        {
            InitializeComponent();
            DataContext = debugWindowViewModel;
            debugWindowViewModel.OwnerWindow = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
