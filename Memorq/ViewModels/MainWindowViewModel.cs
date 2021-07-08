using Memorq.Core;
using Memorq.Infrastructure;
using Memorq.Models;
using Memorq.Services;
using Memorq.Views;
using Memorq.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
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

        private Random random = new();

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
        private Visibility _forceMode;
        public Visibility ForceMode
        {
            get => _forceMode;
            set
            {
                _forceMode = value;
                OnPropertyChanged(nameof(ForceMode));
            }
        }
        private Visibility _gradeNewItemsMode;
        public Visibility GradeNewItemsMode
        {
            get => _gradeNewItemsMode;
            set
            {
                _gradeNewItemsMode = value;
                OnPropertyChanged(nameof(GradeNewItemsMode));
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

                OnPropertyChanged(nameof(AllItemsCount));
                OnPropertyChanged(nameof(NoGradeItemsCount));
                OnPropertyChanged(nameof(ToRepetitionItemsCount));
            }
        }
        public string DefaultCategoryName => DefaultCategory?.Name ?? _stringResourcesDictionary.GetResource("DefaultCategoryNotChosen");
        public bool IsDefaultCategoryChoosen => DefaultCategory != null;
        public int AllItemsCount
        {
            get
            {
                if (IsDefaultCategoryChoosen)
                {
                    return _itemService.GetItems(DefaultCategory.Id).Count;
                }
                else return 0;
            }
        }
        public int NoGradeItemsCount
        {
            get
            {
                if (IsDefaultCategoryChoosen)
                {
                    return _itemService.GetItemsWithoutGrade(DefaultCategory.Id).Count;
                }
                else return 0;
            }
        }
        public int ToRepetitionItemsCount
        {
            get
            {
                if (IsDefaultCategoryChoosen)
                {
                    return _itemService.GetItemsForTodayRepetition(DefaultCategory.Id).Count;
                }
                else return 0;
            }
        }

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

        private List<Item> _forceItemSet;
        public Item ForceCurrentItem { get; set; }
        private string _forceQuestion;
        public string ForceQuestion
        {
            get => _forceQuestion;
            set
            {
                _forceQuestion = value;
                OnPropertyChanged(nameof(ForceQuestion));
            }
        }
        private string _forceAnswer;
        public string ForceAnswer
        {
            get => _forceAnswer;
            set
            {
                _forceAnswer = value;
                OnPropertyChanged(nameof(ForceAnswer));
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

            _itemService.GetItemsForTodayRepetition(100);

            //MessageBox.Show(_itemService.GetItemsForTodayRepetition(DefaultCategory.Id).Count.ToString(),
            //    appName, MessageBoxButton.OK, MessageBoxImage.Information);
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
        public ICommand ShowSettingsCommand => new RelayCommand(_ => _windowFactory.CreateWindow<SettingsWindow>().ShowDialog());
        public ICommand ShowMarkDescriptionCommand => new RelayCommand(_ => _windowFactory.CreateWindow<MarkDescription>().ShowDialog());
        public ICommand ShowAboutCommand => new RelayCommand(_ => _windowFactory.CreateWindow<About>().ShowDialog());

        public ICommand ShowMainPanel => new RelayCommand(_ =>
        {
            MainViewMode = Visibility.Visible;

            AddItemMode = Visibility.Collapsed;
            ForceMode = Visibility.Collapsed;
            GradeNewItemsMode = Visibility.Collapsed;
            ResetPanels();
        });

        public ICommand ShowAddItemPanel => new RelayCommand(_ =>
        {
            AddItemMode = Visibility.Visible;

            MainViewMode = Visibility.Collapsed;
            ForceMode = Visibility.Collapsed;
            GradeNewItemsMode = Visibility.Collapsed;
            ResetPanels();
        });


        public ICommand CheckIfItemReadyToAdd => new RelayCommand(_ =>
            IsItemReadyToAdd = (!NewItemQuestion.Trim().Equals(string.Empty) && !NewItemAnswer.Trim().Equals(string.Empty))
        );

        public ICommand AddItemCommand => new RelayCommand<string>(grade =>
        {

            if (NewItemQuestion.Trim().Length > 255)
            {
                MessageBox.Show(_stringResourcesDictionary.GetResource("MsgQuestionTooLong"),
                    appName, MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

            if (NewItemAnswer.Trim().Length > 255)
            {
                MessageBox.Show(_stringResourcesDictionary.GetResource("MsgAnswerTooLong"),
                    appName, MessageBoxButton.OK, MessageBoxImage.Warning);

                return;
            }

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
        });

        public ICommand ShowForcePanel => new RelayCommand(_ =>
        {
            if (_itemService.GetItems(DefaultCategory.Id).Count == 0)
            {
                MessageBox.Show(_stringResourcesDictionary.GetResource("MsgNoItemsInCategory"),
                                appName, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                ForceMode = Visibility.Visible;

                MainViewMode = Visibility.Collapsed;
                AddItemMode = Visibility.Collapsed;
                GradeNewItemsMode = Visibility.Collapsed;
                ResetPanels();

                _forceItemSet = _itemService.GetItems(DefaultCategory.Id);
                ForcePanelNextItem.Execute(null);
            }
        });

        public ICommand ForcePanelShowTip => new RelayCommand(_ =>
        {
            if (ForceAnswer.Equals(string.Empty))
            {
                ForceAnswer = ForceCurrentItem.Answer[0].ToString();
            }
        });

        public ICommand ForcePanelShowAnswer => new RelayCommand(_ => ForceAnswer = ForceCurrentItem.Answer);

        public ICommand ForcePanelNextItem => new RelayCommand(_ =>
        {
            ForceCurrentItem = _forceItemSet.Skip(random.Next(0, _forceItemSet.Count)).FirstOrDefault();
            if (ForceCurrentItem != null)
            {
                _forceItemSet.Remove(ForceCurrentItem);
                ForceQuestion = ForceCurrentItem.Question;
                ForceAnswer = string.Empty;
            }
            else
            {
                MessageBox.Show(_stringResourcesDictionary.GetResource("MsgForceModeAllItemsPassed"),
                appName, MessageBoxButton.OK, MessageBoxImage.Information);

                ShowMainPanel.Execute(null);
            }
        });

        public ICommand ShowGradeNewItemsMode => new RelayCommand(_ =>
        {
            if (_itemService.GetItems(DefaultCategory.Id).Count == 0)
            {
                MessageBox.Show(_stringResourcesDictionary.GetResource("MsgNoItemsInCategory"),
                                appName, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                GradeNewItemsMode = Visibility.Visible;

                MainViewMode = Visibility.Collapsed;
                AddItemMode = Visibility.Collapsed;
                ForceMode = Visibility.Collapsed;
                ResetPanels();

                _forceItemSet = _itemService.GetItems(DefaultCategory.Id);
                ForcePanelNextItem.Execute(null);

                MessageBox.Show("grade new items",
                                appName, MessageBoxButton.OK, MessageBoxImage.Information);
            }
        });

        private void ResetPanels()
        {
            NewItemQuestion = string.Empty;
            NewItemAnswer = string.Empty;
            ForceQuestion = string.Empty;
            ForceAnswer = string.Empty;
            _forceItemSet = null;
            ForceCurrentItem = null;
            IsItemReadyToAdd = false;
        }
    }
}