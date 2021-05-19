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

        public ICommand ShowCategoryManagerCommand => new RelayCommand(_ =>
        {
            var categoryManager = _windowFactory.CreateWindow<CategoryManager>();

            if (categoryManager.ShowDialog() == true)
            {
                //var categoryManagerViewModel = (CategoryManagerViewModel)categoryManager.DataContext;
                //todo: wybranie kategorii
            }
        });

        public ICommand ShowDebugWindowCommand => new RelayCommand(_ =>
        {
            var debugWindow = _windowFactory.CreateWindow<DebugWindow>();

            if (debugWindow.ShowDialog() == true)
            {
                var debugWindowViewModel = (DebugWindowViewModel)debugWindow.DataContext;
                MessageBox.Show(debugWindowViewModel.CategoryToInsert);
            }
        });

        public ICommand ShowAboutCommand => new RelayCommand(_ => _windowFactory.CreateWindow<About>().ShowDialog());
    }
}
