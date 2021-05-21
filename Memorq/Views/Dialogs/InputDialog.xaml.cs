using System;
using System.Windows;

namespace Memorq.Views.Dialogs
{
    public partial class InputDialog : Window
    {
        public InputDialog(string labelText)
        {
            InitializeComponent();
            this.Label.Content = labelText;
        }

        private void OKBtnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        public string Answer
        {
            get { return this.TextBox.Text; }
        }
    }
}
