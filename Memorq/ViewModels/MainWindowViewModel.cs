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
        public Category DefaultCategory
        {
            get => _defaultCategory;
            set
            {
                _defaultCategory = value;
                OnPropertyChanged(nameof(DefaultCategory));
            }
        }

        private string _defaultCategoryName;
        public string DefaultCategoryName
        {
            get => _defaultCategoryName;
            set
            {
                _defaultCategoryName = value;
                OnPropertyChanged(nameof(DefaultCategoryName));
            }
        }

        private bool _isDefaultCategoryChoosen;
        public bool IsDefaultCategoryChoosen
        {
            get => _isDefaultCategoryChoosen;
            set
            {
                _isDefaultCategoryChoosen = value;
                OnPropertyChanged(nameof(IsDefaultCategoryChoosen));
            }
        }

        private void InitDefaultCategory()
        {
            DefaultCategory = _categoryProvider.GetCategory(UserSettings.Default.DefaultCategory);
            if (DefaultCategory == null)
            {
                DefaultCategoryName = GetDictResource("DefaultCategoryNotChosen");
                IsDefaultCategoryChoosen = false;

                UserSettings.Default.DefaultCategory = -1;
                UserSettings.Default.Save();
            }
            else
            {
                DefaultCategoryName = string.Format("{0}: {1}", GetDictResource("Category"), DefaultCategory.Name);
                IsDefaultCategoryChoosen = true;
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
            categoryManager.ShowDialog();

            var categoryManagerViewModel = (CategoryManagerViewModel)categoryManager.DataContext;
            DefaultCategory = categoryManagerViewModel.SelectedCategory;

            if (DefaultCategory == null)
            {
                DefaultCategoryName = GetDictResource("DefaultCategoryNotChosen");
                IsDefaultCategoryChoosen = false;
                UserSettings.Default.DefaultCategory = -1;
            }
            else
            {
                DefaultCategoryName = string.Format("{0}: {1}", GetDictResource("Category"), DefaultCategory.Name);
                IsDefaultCategoryChoosen = true;
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