using Memorq.Infrastructure;
using Memorq.Views;
using Memorq.Views.Dialogs;
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
                var categoryManagerViewModel = (CategoryManagerViewModel)categoryManager.DataContext;

                UserSettings.Default.DefaultCategory = categoryManagerViewModel.SelectedCategory.Id;
                UserSettings.Default.Save();
            }
        });

        public ICommand ShowAboutCommand => new RelayCommand(_ => _windowFactory.CreateWindow<About>().ShowDialog());
    }
}