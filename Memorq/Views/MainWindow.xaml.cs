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
        private void CategoryStatsLabel_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("CategoryStatsTooltip");
        }
        private void CategoryStatsLabel_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("DefaultTooltip");
        }
        private void EnterQuestionLabel_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("AddItemQuestionTooltip");
        }
        private void EnterQuestionLabel_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("DefaultAddItemTooltip");
        }
        private void NewItemQuestionTb_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("AddItemQuestionTooltip");
        }
        private void NewItemQuestionTb_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("DefaultAddItemTooltip");
        }
        private void EnterAnswerLabel_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("AddItemAnswerTooltip");
        }
        private void EnterAnswerLabel_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("DefaultAddItemTooltip");
        }
        private void NewItemAnswerTb_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("AddItemAnswerTooltip");
        }
        private void NewItemAnswerTb_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("DefaultAddItemTooltip");
        }
        private void AddItem_BackToMainViewBtn_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("BackToMainViewTooltip");
        }
        private void AddItem_BackToMainViewBtn_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("DefaultAddItemTooltip");
        }
        private void AddItemGrade5Btn_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("Mark5");
        }
        private void AddItemGrade5Btn_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("DefaultAddItemTooltip");
        }
        private void AddItemGrade4Btn_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("Mark4");
        }
        private void AddItemGrade4Btn_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("DefaultAddItemTooltip");
        }
        private void AddItemGrade3Btn_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("Mark3");
        }
        private void AddItemGrade3Btn_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("DefaultAddItemTooltip");
        }
        private void AddItemGrade2Btn_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("Mark2");
        }
        private void AddItemGrade2Btn_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("DefaultAddItemTooltip");
        }
        private void AddItemGrade1Btn_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("Mark1");
        }
        private void AddItemGrade1Btn_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("DefaultAddItemTooltip");
        }
        private void AddItemGrade0Btn_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("Mark0");
        }
        private void AddItemGrade0Btn_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            TooltipLabel.Content = (string)Application.Current.FindResource("DefaultAddItemTooltip");
        }

        #endregion
    }
}