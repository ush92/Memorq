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
            categoryManager.ShowDialog();

            var categoryManagerViewModel = (CategoryManagerViewModel)categoryManager.DataContext;
            DefaultCategory = categoryManagerViewModel.SelectedCategory;

            if (DefaultCategory == null)
            {
                UserSettings.Default.DefaultCategory = -1;
            }
            else
            {
                UserSettings.Default.DefaultCategory = categoryManagerViewModel.SelectedCategory.Id;
            }
            UserSettings.Default.Save();
        });

        public ICommand ShowImportExportCommand => new RelayCommand(_ =>
        {
            var importExport = _windowFactory.CreateWindow<ImportExport>();
            //to do: set category for new window
            importExport.ShowDialog();
         
        });

        public ICommand ShowMarkDescriptionCommand => new RelayCommand(_ => _windowFactory.CreateWindow<MarkDescription>().ShowDialog());
        public ICommand ShowAboutCommand => new RelayCommand(_ => _windowFactory.CreateWindow<About>().ShowDialog());
    }
}