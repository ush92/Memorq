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

        #region Visibility properties

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
        private Visibility _gradeNewItemAnswerPanel;
        public Visibility GradeNewItemAnswerPanel
        {
            get => _gradeNewItemAnswerPanel;
            set
            {
                _gradeNewItemAnswerPanel = value;
                OnPropertyChanged(nameof(GradeNewItemAnswerPanel));
            }
        }
        private Visibility _gradeNewItemGradesPanel;
        public Visibility GradeNewItemGradesPanel
        {
            get => _gradeNewItemGradesPanel;
            set
            {
                _gradeNewItemGradesPanel = value;
                OnPropertyChanged(nameof(GradeNewItemGradesPanel));
            }
        }

        private Visibility _learnMode;
        public Visibility LearnMode
        {
            get => _learnMode;
            set
            {
                _learnMode = value;
                OnPropertyChanged(nameof(LearnMode));
            }
        }
        private Visibility _learnModeAnswerPanel;
        public Visibility LearnModeAnswerPanel
        {
            get => _learnModeAnswerPanel;
            set
            {
                _learnModeAnswerPanel = value;
                OnPropertyChanged(nameof(LearnModeAnswerPanel));
            }
        }
        private Visibility _learnModeGradesPanel;
        public Visibility LearnModeGradesPanel
        {
            get => _learnModeGradesPanel;
            set
            {
                _learnModeGradesPanel = value;
                OnPropertyChanged(nameof(LearnModeGradesPanel));
            }
        }

        #endregion

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

        private List<Item> _itemsToGradeSet;
        public Item ToGradeCurrentItem { get; set; }
        private string _toGradeQuestion;
        public string ToGradeQuestion
        {
            get => _toGradeQuestion;
            set
            {
                _toGradeQuestion = value;
                OnPropertyChanged(nameof(ToGradeQuestion));
            }
        }
        private string _toGradeAnswer;
        public string ToGradeAnswer
        {
            get => _toGradeAnswer;
            set
            {
                _toGradeAnswer = value;
                OnPropertyChanged(nameof(ToGradeAnswer));
            }
        }

        private List<Item> _learnSet;
        public Item LearnCurrentItem { get; set; }
        private string _learnQuestion;
        public string LearnQuestion
        {
            get => _learnQuestion;
            set
            {
                _learnQuestion = value;
                OnPropertyChanged(nameof(LearnQuestion));
            }
        }
        private string _learnAnswer;
        public string LearnAnswer
        {
            get => _learnAnswer;
            set
            {
                _learnAnswer = value;
                OnPropertyChanged(nameof(LearnAnswer));
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

            RefreshCategory();
            UserSettings.Default.Save();
        });
        public ICommand ShowImportExportCommand => new RelayCommand(_ =>
        {
            var importExport = _windowFactory.CreateWindow<ImportExport>();
            var importExportViewModel = (ImportExportViewModel)importExport.DataContext;
            importExportViewModel.DefaultCategoryId = DefaultCategory.Id;
            importExportViewModel.ItemList = _itemService.GetItems(DefaultCategory.Id);

            importExport.ShowDialog();
            RefreshCategory();
        });
        public ICommand ShowScheduleCommand => new RelayCommand(_ =>
        {
            var scheduleDialog = _windowFactory.CreateWindow<Schedule>();
            var scheduleViewModel = (ScheduleViewModel)scheduleDialog.DataContext;
            var allItems = _itemService.GetItems(DefaultCategory.Id);
            var scheduleList = new List<ScheduleDay>();

            for(int i = 1; i<= 30; i++)
            {
                var scheduleDay = new ScheduleDay()
                {
                    Date = DateTime.Today.AddDays(i).ToString("dd.MM.yyyy"),
                    Count = _itemService.GetItemsForRepetitionByDate(DefaultCategory.Id, DateTime.Today.AddDays(i)).Count
                };
                scheduleList.Add(scheduleDay);
            }

            scheduleViewModel.ItemList = scheduleList;
            scheduleDialog.ShowDialog();
        });
        public ICommand ShowHardItemsCommand => new RelayCommand(_ => 
        {
            var hardItemsDialog = _windowFactory.CreateWindow<HardItems>();
            var hardItemsViewModel = (HardItemsViewModel)hardItemsDialog.DataContext;
            hardItemsViewModel.ItemList = _itemService.GetHardItems(DefaultCategory.Id);
            hardItemsDialog.ShowDialog();
        });
        public ICommand ShowSettingsCommand => new RelayCommand(_ => _windowFactory.CreateWindow<SettingsWindow>().ShowDialog());
        public ICommand ShowMarkDescriptionCommand => new RelayCommand(_ => _windowFactory.CreateWindow<MarkDescription>().ShowDialog());
        public ICommand ShowAboutCommand => new RelayCommand(_ => _windowFactory.CreateWindow<About>().ShowDialog());
        public ICommand ShowMainPanel => new RelayCommand(_ =>
        {
            CollapsePanels();
            ResetPanels();
            MainViewMode = Visibility.Visible;
        });

        public ICommand ShowAddItemPanel => new RelayCommand(_ =>
        {
            CollapsePanels();
            ResetPanels();
            AddItemMode = Visibility.Visible;
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
                CollapsePanels();
                ResetPanels();
                ForceMode = Visibility.Visible;

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

        public ICommand ShowGradeNewItemsPanel => new RelayCommand(_ =>
        {
            if (_itemService.GetItemsWithoutGrade(DefaultCategory.Id).Count == 0)
            {
                MessageBox.Show(_stringResourcesDictionary.GetResource("MsgNoItemsToGradeInCategory"),
                                appName, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                CollapsePanels();
                ResetPanels();
                GradeNewItemsMode = Visibility.Visible;
                GradeNewItemAnswerPanel = Visibility.Visible;

                _itemsToGradeSet = _itemService.GetItemsWithoutGrade(DefaultCategory.Id);
                GradeNewItemsNextItem.Execute(null);
            }
        });
        public ICommand GradeNewItemsNextItem => new RelayCommand(_ =>
        {
            ToGradeCurrentItem = _itemsToGradeSet.Skip(random.Next(0, _itemsToGradeSet.Count)).FirstOrDefault();
            if (ToGradeCurrentItem != null)
            {
                _itemsToGradeSet.Remove(ToGradeCurrentItem);
                ToGradeQuestion = ToGradeCurrentItem.Question;
                ToGradeAnswer = string.Empty;

                GradeNewItemAnswerPanel = Visibility.Visible;
                GradeNewItemGradesPanel = Visibility.Collapsed;
            }
            else
            {
                MessageBox.Show(_stringResourcesDictionary.GetResource("MsgItemsToGradeAllItemsPassed"),
                appName, MessageBoxButton.OK, MessageBoxImage.Information);

                ShowMainPanel.Execute(null);
            }
        });
        public ICommand GradeNewItemsShowAnswer => new RelayCommand(_ =>
        {
            ToGradeAnswer = ToGradeCurrentItem.Answer;
            GradeNewItemAnswerPanel = Visibility.Collapsed;
            GradeNewItemGradesPanel = Visibility.Visible;
        });
        public ICommand GradeNewItemsShowTip => new RelayCommand(_ =>
        {
            if (ToGradeAnswer.Equals(string.Empty))
            {
                ToGradeAnswer = ToGradeCurrentItem.Answer[0].ToString();
            }
        });
        public ICommand GradeNewItemCommand => new RelayCommand<string>(grade =>
        {
            _memorqCore.UpdateItemStats(ToGradeCurrentItem, Int32.Parse(grade));
            _itemService.UpdateItem(ToGradeCurrentItem);
            RefreshCategory();
            GradeNewItemsNextItem.Execute(null);
        });

        public ICommand ShowLearnPanel => new RelayCommand(_ =>
        {
            if (_itemService.GetItems(DefaultCategory.Id).Count == 0)
            {
                MessageBox.Show(_stringResourcesDictionary.GetResource("MsgNoItemsInCategory"),
                                appName, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (_itemService.GetItemsForTodayRepetition(DefaultCategory.Id).Count == 0)
            {
                MessageBox.Show(_stringResourcesDictionary.GetResource("MsgLearnModeAllItemsPassed"),
                                appName, MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                CollapsePanels();
                ResetPanels();
                LearnMode = Visibility.Visible;
                LearnModeAnswerPanel = Visibility.Visible;

                _learnSet = _itemService.GetItemsForTodayRepetition(DefaultCategory.Id);
                LearnNextItem.Execute(null);
            }
        });
        public ICommand LearnNextItem => new RelayCommand(_ =>
        {
            LearnCurrentItem = _learnSet.Skip(random.Next(0, _learnSet.Count)).FirstOrDefault();
            if (LearnCurrentItem != null)
            {
                _learnSet.Remove(LearnCurrentItem);
                LearnQuestion = LearnCurrentItem.Question;
                LearnAnswer = string.Empty;

                LearnModeAnswerPanel = Visibility.Visible;
                LearnModeGradesPanel = Visibility.Collapsed;
            }
            else
            {
                _learnSet = _itemService.GetItemsForTodayRepetition(DefaultCategory.Id);
                LearnCurrentItem = _learnSet.Skip(random.Next(0, _learnSet.Count)).FirstOrDefault();
                if (LearnCurrentItem != null)
                {
                    _learnSet.Remove(LearnCurrentItem);
                    LearnQuestion = LearnCurrentItem.Question;
                    LearnAnswer = string.Empty;

                    LearnModeAnswerPanel = Visibility.Visible;
                    LearnModeGradesPanel = Visibility.Collapsed;
                }
                else
                {
                    MessageBox.Show(_stringResourcesDictionary.GetResource("MsgLearnModeAllItemsPassed"),
                                appName, MessageBoxButton.OK, MessageBoxImage.Information);
                    ShowMainPanel.Execute(null);
                }
            }
        });
        public ICommand LearnModeShowAnswer => new RelayCommand(_ =>
        {
            LearnAnswer = LearnCurrentItem.Answer;
            LearnModeAnswerPanel = Visibility.Collapsed;
            LearnModeGradesPanel = Visibility.Visible;
        });
        public ICommand LearnModeShowTip => new RelayCommand(_ =>
        {
            if (LearnAnswer.Equals(string.Empty))
            {
                LearnAnswer = LearnCurrentItem.Answer[0].ToString();
            }
        });
        public ICommand LearnModeGradeCommand => new RelayCommand<string>(grade =>
        {
            _memorqCore.UpdateItemStats(LearnCurrentItem, Int32.Parse(grade));
            _itemService.UpdateItem(LearnCurrentItem);
            RefreshCategory();
            LearnNextItem.Execute(null);
        });

        private void ResetPanels()
        {
            RefreshCategory();
            NewItemQuestion = string.Empty;
            NewItemAnswer = string.Empty;
            IsItemReadyToAdd = false;

            ForceQuestion = string.Empty;
            ForceAnswer = string.Empty;
            _forceItemSet = null;
            ForceCurrentItem = null;

            ToGradeQuestion = string.Empty;
            ToGradeAnswer = string.Empty;
            _itemsToGradeSet = null;
            ToGradeCurrentItem = null;

            LearnAnswer = string.Empty;
            LearnQuestion = string.Empty;
            _learnSet = null;
            LearnCurrentItem = null;
        }
        private void RefreshCategory()
        {
            DefaultCategory = _categoryService.GetCategory(UserSettings.Default.DefaultCategory);
        }
        private void CollapsePanels()
        {
            MainViewMode = Visibility.Collapsed;
            AddItemMode = Visibility.Collapsed;
            ForceMode = Visibility.Collapsed;

            GradeNewItemsMode = Visibility.Collapsed;
            GradeNewItemAnswerPanel = Visibility.Collapsed;
            GradeNewItemGradesPanel = Visibility.Collapsed;

            LearnMode = Visibility.Collapsed;
            LearnModeAnswerPanel = Visibility.Collapsed;
            LearnModeGradesPanel = Visibility.Collapsed;
        }
    }
}