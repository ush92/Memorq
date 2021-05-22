using System.Windows;

namespace Memorq.Views.Dialogs
{
    public partial class CtgMngEditItem : Window
    {
        public CtgMngEditItem()
        {
            InitializeComponent();

            QuestionTexBox.Text = string.Empty;
            AnswerTexBox.Text = string.Empty;
            QuestionVerification.Content = string.Empty;
            AnswerVerification.Content = string.Empty;

            QuestionTexBox.Focus();
        }

        private void OKBtnClick(object sender, RoutedEventArgs e)
        {
            bool isQuestionOK = true;
            bool isAnswerOK = true;
            
            if (QuestionTexBox.Text.Trim().Equals(string.Empty))
            {
                QuestionVerification.Content = (string)Application.Current.FindResource("MsgQuestionEmpty");
                isQuestionOK = false;
            }
 
            if (QuestionTexBox.Text.Trim().Length > 255)
            {
                QuestionVerification.Content = (string)Application.Current.FindResource("MsgQuestionTooLong");
                isQuestionOK = false;
            }

            if (AnswerTexBox.Text.Trim().Equals(string.Empty))
            {
                AnswerVerification.Content = (string)Application.Current.FindResource("MsgAnswerEmpty");
                isAnswerOK = false;
            }

            if (AnswerTexBox.Text.Trim().Length > 255)
            {
                AnswerVerification.Content = (string)Application.Current.FindResource("MsgAnswerTooLong");
                isAnswerOK = false;
            }

            if(!isQuestionOK)
            {
                QuestionTexBox.Text = string.Empty;
                QuestionTexBox.Focus();
            }
            else
            {
                QuestionVerification.Content = string.Empty;
            }

            if (!isAnswerOK)
            {
                AnswerTexBox.Text = string.Empty;
                AnswerTexBox.Focus();
            }
            else
            {
                AnswerVerification.Content = string.Empty;
            }

            if (isQuestionOK && isAnswerOK)
            {
                DialogResult = true;
            }
        }

        private void SwapQuestionAnswerBtnClick(object sender, RoutedEventArgs e)
        {
            string tmp = QuestionTexBox.Text;
            QuestionTexBox.Text = AnswerTexBox.Text;
            AnswerTexBox.Text = tmp;
        }
    }
}