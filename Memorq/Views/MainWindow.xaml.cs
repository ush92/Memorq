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

            TooltipLabel.Content = (string)Application.Current.FindResource("DefaultTooltip");
        }


        #region Tooltips

        private void LearnBtn_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("BtnLearnTooltip");
        }
        private void LearnBtn_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("DefaultTooltip");
        }

        private void AddBtn_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("BtnAddTooltip");
        }
        private void AddBtn_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("DefaultTooltip");
        }

        private void NewBtn_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("BtnNewTooltip");
        }
        private void NewBtn_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("DefaultTooltip");
        }

        private void ForceBtn_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("BtnForceTooltip");
        }
        private void ForceBtn_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("DefaultTooltip");
        }

        #endregion
    }
}