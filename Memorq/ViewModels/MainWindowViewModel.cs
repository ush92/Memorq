using Memorq.Infrastructure;
using Memorq.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Task.Commands;

namespace Memorq.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private DebugWindow _debugWindow;

        public MainWindowViewModel(DebugWindow debugWindow)
        {
            _debugWindow = debugWindow;
        }


        #region RightPanelButtons

        public ICommand OpenDebugWindowCommand => new RelayCommand(_ => _debugWindow.ShowDialog());

        #endregion

        #region MainMenu

        //private void MenuFileExitItem_Click(object sender, RoutedEventArgs e)
        //{
        //    this.Close();
        //}

        //private void MenuHelpAboutItem_Click(object sender, RoutedEventArgs e)
        //{
        //    aboutWindow = new About();
        //    aboutWindow.ShowDialog();
        //}

        #endregion


    }
}
