using Memorq.Infrastructure;
using Memorq.Views;
using System.Windows;
using System.Windows.Input;

namespace Memorq.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IWindowFactory _windowFactory;

        public MainWindowViewModel(IWindowFactory windowFactory)
        {
            _windowFactory = windowFactory;
        }

        #region RightPanelButtons

        public ICommand OpenDebugWindowCommand => new RelayCommand(_ => {
            var debugWindow = _windowFactory.CreateWindow<DebugWindow>();
            debugWindow.ShowDialog();
        });

        #endregion

        #region MainMenu

        public ICommand MenuFileExitCommand => new RelayCommand(_ => Application.Current.MainWindow.Close());

        public ICommand MenuHelpAboutCommand => new RelayCommand(_ => _windowFactory.CreateWindow<About>().ShowDialog());

        #endregion
    }
}
