using System;
using System.Windows;

namespace Memorq.Views.Dialogs
{
    public partial class InputDialog : Window
    {
        public InputDialog(string labelText, string title)
        {
            InitializeComponent();
            this.Label.Content = labelText;
            this.Title = title;
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
