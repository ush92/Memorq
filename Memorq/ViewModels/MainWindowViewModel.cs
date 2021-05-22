using Memorq.Infrastructure;
using Memorq.Models;
using Memorq.Services;
using Memorq.Views;
using Memorq.Views.Dialogs;
using System.Windows.Input;

namespace Memorq.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly ICategoryService _categoryProvider;
        private readonly IItemService _itemProvider;
        private readonly IWindowFactory _windowFactory;

        private Category _defaultCategory;
        private string _defaultCategoryName;

        public Category DefaultCategory
        {
            get => _defaultCategory;
            set
            {
                _defaultCategory = value;
                OnPropertyChanged(nameof(DefaultCategory));
            }
        }

        public string DefaultCategoryName
        {
            get => _defaultCategoryName;
            set
            {
                _defaultCategoryName = value;
                OnPropertyChanged(nameof(DefaultCategoryName));
            }
        }

        private void InitDefaultCategory()
        {
            DefaultCategory = _categoryProvider.GetCategory(UserSettings.Default.DefaultCategory);
            if (DefaultCategory == null)
            {
                DefaultCategoryName = GetDictResource("DefaultCategoryNotChosen");

                UserSettings.Default.DefaultCategory = -1;
                UserSettings.Default.Save();
            }
            else
            {
                DefaultCategoryName = string.Format("{0}: {1}", GetDictResource("Category"), DefaultCategory.Name);
            }
        }

        public MainWindowViewModel(ICategoryService categoryProvider, IItemService itemProvider, IWindowFactory windowFactory)
        {
            _categoryProvider = categoryProvider;
            _itemProvider = itemProvider;
            _windowFactory = windowFactory;

            InitDefaultCategory();
        }

        public ICommand ShowCategoryManagerCommand => new RelayCommand(_ =>
        {
            var categoryManager = _windowFactory.CreateWindow<CategoryManager>();

            if (categoryManager.ShowDialog() == true)
            {
                var categoryManagerViewModel = (CategoryManagerViewModel)categoryManager.DataContext;

                DefaultCategory = categoryManagerViewModel.SelectedCategory;
                DefaultCategoryName = string.Format("{0}: {1}", GetDictResource("Category"), DefaultCategory.Name);

                UserSettings.Default.DefaultCategory = categoryManagerViewModel.SelectedCategory.Id;
                UserSettings.Default.Save();
            }
        });

        public ICommand ShowAboutCommand => new RelayCommand(_ => _windowFactory.CreateWindow<About>().ShowDialog());
    }
}