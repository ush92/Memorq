using Memorq.Core;
using Memorq.Infrastructure;
using Memorq.Models;
using Memorq.Services;
using Memorq.Views;
using Memorq.Views.Dialogs;
using System;
using System.Windows;
using System.Windows.Input;

namespace Memorq.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly ICategoryService _categoryService;
        private readonly IItemService _itemService;
        private readonly IWindowFactory _windowFactory;
        private readonly IStringResourcesDictionary _stringResourcesDictionary;
        private readonly IMemorqCore _memorqCore;

        private Visibility _mainViewMode;
        public Visibility MainViewMode
        {
            get => _mainViewMode;
            set
            {
                _mainViewMode = value;
                OnPropertyChanged(nameof(MainViewMode));
            }
        }
        private Visibility _addItemMode;
        public Visibility AddItemMode
        {
            get => _addItemMode;
            set
            {
                _addItemMode = value;
                OnPropertyChanged(nameof(AddItemMode));
            }
        }

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

        private string _newItemQuestion;
        public string NewItemQuestion
        {
            get => _newItemQuestion;
            set
            {
                _newItemQuestion = value;
                OnPropertyChanged(nameof(NewItemQuestion));
            }
        }
        private string _newItemAnswer;
        public string NewItemAnswer
        {
            get => _newItemAnswer;
            set
            {
                _newItemAnswer = value;
                OnPropertyChanged(nameof(NewItemAnswer));
            }
        }

        private bool _isItemReadyToAdd;
        public bool IsItemReadyToAdd
        {
            get => _isItemReadyToAdd;
            set
            {
                _isItemReadyToAdd = value;
                OnPropertyChanged(nameof(IsItemReadyToAdd));
            }
        }

        private void InitDefaultCategory()
        {
            DefaultCategory = _categoryService.GetCategory(UserSettings.Default.DefaultCategory);
            if (DefaultCategory == null)
            {
                UserSettings.Default.DefaultCategory = -1;
                UserSettings.Default.Save();
            }
        }

        public MainWindowViewModel(ICategoryService categoryService, IItemService itemService, IMemorqCore memorqCore,
                                   IWindowFactory windowFactory, IStringResourcesDictionary stringResourcesDictionary)
        {
            _categoryService = categoryService;
            _itemService = itemService;
            _windowFactory = windowFactory;
            _stringResourcesDictionary = stringResourcesDictionary;
            _memorqCore = memorqCore;

            InitDefaultCategory();
            ShowMainPanel.Execute(null);
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

            if (DefaultCategory == null) ShowMainPanel.Execute(null);

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

        public ICommand ShowMainPanel => new RelayCommand(_ => {
            MainViewMode = Visibility.Visible;
            AddItemMode = Visibility.Collapsed;
            ResetPanels();
        });

        public ICommand ShowAddItemPanel => new RelayCommand(_ =>
        {
            AddItemMode = Visibility.Visible;
            MainViewMode = Visibility.Collapsed;
            ResetPanels();
        });

        public ICommand CheckIfItemReadyToAdd => new RelayCommand(_ =>
            IsItemReadyToAdd = (!NewItemQuestion.Trim().Equals(string.Empty) && !NewItemAnswer.Trim().Equals(string.Empty))
        );

        public ICommand AddItemCommand => new RelayCommand<string>(grade => {

            if(true) //todo: add question/answer verification
            {
                Item itemToAdd = new Item
                {
                    CategoryId = DefaultCategory.Id,
                    Question = NewItemQuestion.Trim(),
                    Answer = NewItemAnswer.Trim(),
                    InsertDate = DateTime.Now,
                    Repetition = 0,
                    EFactor = 2.5,
                    Interval = 0
                };

                _memorqCore.UpdateItemStats(itemToAdd, Int32.Parse(grade));
                _itemService.InsertItem(itemToAdd);

                ResetPanels();
            }
        });

        private void ResetPanels()
        {
            NewItemQuestion = string.Empty;
            NewItemAnswer = string.Empty;
            IsItemReadyToAdd = false;
        }
    }
}