using System.Windows;

namespace Memorq.Views.Dialogs
{
    public partial class InputDialog : Window
    {
        public InputDialog()
        {
            InitializeComponent();
        }

        private void OKBtnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
