using Memorq.Classes.Forms;
using Memorq.Classes.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Memorq
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DebugWindow debugWindow = null;
        private About aboutWindow = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        #region RightPanelButtons

        private void DebugBtn_Click(object sender, RoutedEventArgs e)
        {
            debugWindow = new DebugWindow();
            debugWindow.ShowDialog();
        }

        #endregion

        #region MainMenu

        private void MenuFileExitItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuHelpAboutItem_Click(object sender, RoutedEventArgs e)
        {
            aboutWindow = new About();
            aboutWindow.ShowDialog();
        }

        #endregion
    }
}
