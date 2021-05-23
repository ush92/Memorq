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
        private readonly ICategoryService _categoryService;
        private readonly IItemService _itemService;
        private readonly IWindowFactory _windowFactory;
        private readonly IStringResourcesDictionary _stringResourcesDictionary;

        private Category _defaultCategory;
        public Category DefaultCategory
        {
            get => _defaultCategory;
            set
            {
                _defaultCategory = value;
                OnPropertyChanged(nameof(DefaultCategory));
                OnPropertyChanged(nameof(DefaultCategoryName));
                OnPropertyChanged(nameof(IsDefaultCategoryChoosen));
            }
        }
        public string DefaultCategoryName => DefaultCategory?.Name ?? _stringResourcesDictionary.GetResource("DefaultCategoryNotChosen");
        public bool IsDefaultCategoryChoosen => DefaultCategory != null;

        private void InitDefaultCategory()
        {
            DefaultCategory = _categoryService.GetCategory(UserSettings.Default.DefaultCategory);
            if (DefaultCategory == null)
            {
                UserSettings.Default.DefaultCategory = -1;
                UserSettings.Default.Save();
            }
        }

        public MainWindowViewModel(ICategoryService categoryService, IItemService itemService,
                                   IWindowFactory windowFactory, IStringResourcesDictionary stringResourcesDictionary)
        {
            _categoryService = categoryService;
            _itemService = itemService;
            _windowFactory = windowFactory;
            _stringResourcesDictionary = stringResourcesDictionary;

            InitDefaultCategory();
        }

        public ICommand ShowCategoryManagerCommand => new RelayCommand(_ =>
        {
            var categoryManager = _windowFactory.CreateWindow<CategoryManager>();
            var categoryManagerViewModel = (CategoryManagerViewModel)categoryManager.DataContext;

            if (categoryManager.ShowDialog() == true)
            {
                DefaultCategory = _categoryService.GetCategory(categoryManagerViewModel.SelectedCategory.Id);
                UserSettings.Default.DefaultCategory = categoryManagerViewModel.SelectedCategory.Id;
            }
            else
            {
                if (DefaultCategory != null)
                {
                    DefaultCategory = _categoryService.GetCategory(DefaultCategory.Id);
                }
                else
                {
                    UserSettings.Default.DefaultCategory = -1;
                }
            }
            UserSettings.Default.Save();
        });

        public ICommand ShowImportExportCommand => new RelayCommand(_ =>
        {
            var importExport = _windowFactory.CreateWindow<ImportExport>();
            var importExportViewModel = (ImportExportViewModel)importExport.DataContext;
            importExportViewModel.DefaultCategoryId = DefaultCategory.Id;
            importExportViewModel.ItemList = _itemService.GetItems(DefaultCategory.Id);

            importExport.ShowDialog();         
        });

        public ICommand ShowMarkDescriptionCommand => new RelayCommand(_ => _windowFactory.CreateWindow<MarkDescription>().ShowDialog());
        public ICommand ShowAboutCommand => new RelayCommand(_ => _windowFactory.CreateWindow<About>().ShowDialog());
    }
}