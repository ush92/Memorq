using System.Windows;

namespace Memorq.Views.Dialogs
{
    public partial class InputDialog : Window
    {
        public InputDialog(string labelText, string title)
        {
            InitializeComponent();
            this.Title = title;
            this.Label.Content = labelText;
            this.VerificationLabel.Content = string.Empty;
        }

        private void OKBtnClick(object sender, RoutedEventArgs e)
        {
            if(this.TextBox.Text.Trim().Equals(string.Empty))
            {
                this.VerificationLabel.Content = (string)Application.Current.FindResource("MsgCategoryNoName");
                this.TextBox.Text = string.Empty;
                this.TextBox.Focus();
            }
            else
            {
                if (this.TextBox.Text.Trim().Length > 255)
                {
                    this.VerificationLabel.Content = (string)Application.Current.FindResource("MsgCategoryTooLongName");
                    this.TextBox.Text = string.Empty;
                    this.TextBox.Focus();
                }
                else
                {
                    DialogResult = true;
                }               
            }       
        }

        public string Answer
        {
            get { return this.TextBox.Text.Trim(); }
        }
    }
}