using Memorq.Infrastructure;
using Memorq.Views;
using Microsoft.Extensions.Options;
using System.Windows;
using System.Windows.Input;

namespace Memorq.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IWindowFactory _windowFactory;
        private readonly IOptions<AppSettings> _options;

        public MainWindowViewModel(IWindowFactory windowFactory, IOptions<AppSettings> options)
        {
            _windowFactory = windowFactory;
            _options = options;
        }

        public ICommand ShowCategoryManagerCommand => new RelayCommand(_ =>
        {
            var categoryManager = _windowFactory.CreateWindow<CategoryManager>();

            if (categoryManager.ShowDialog() == true)
            {
                var categoryManagerViewModel = (CategoryManagerViewModel)categoryManager.DataContext;
                MessageBox.Show(categoryManagerViewModel.SelectedCategory.Name + " " + _options.Value.StringSetting);
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
