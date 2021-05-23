using Memorq.Infrastructure;
using Memorq.Models;
using Memorq.Services;
using Memorq.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Memorq.ViewModels
{
    public class CategoryManagerViewModel : BaseViewModel
    {
        private readonly ICategoryService _categoryService;
        private readonly IItemService _itemService;
        private readonly IWindowFactory _windowFactory;
        private readonly IStringResourcesDictionary _stringResourcesDictionary;

        InputDialog newCategoryDialog;

        private List<Category> _categoriesList;
        private Category _selectedCategory;
        private List<Item> _itemList;
        private Item _selectedItem;

        public List<Category> CategoriesList
        {
            get => _categoriesList;
            set
            {
                _categoriesList = value;
                OnPropertyChanged(nameof(CategoriesList));
            }
        }
        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }
        public List<Item> ItemList
        {
            get => _itemList;
            set
            {
                _itemList = value;
                OnPropertyChanged(nameof(ItemList));
            }
        }
        public Item SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }

        public CategoryManagerViewModel(ICategoryService categoryService, IItemService itemService,
                                        IWindowFactory windowFactory, IStringResourcesDictionary stringResourcesDictionary)
        {
            _categoryService = categoryService;
            _itemService = itemService;
            _windowFactory = windowFactory;
            _stringResourcesDictionary = stringResourcesDictionary;

            CategoriesList = _categoryService.GetCategories();
        }

        public ICommand UpdateItemsGridBySelectCategoryCommand => new RelayCommand<Category>(c =>
        {
            SelectedCategory = c;
            if (SelectedCategory != null)
            {
                ItemList = _itemService.GetItems(SelectedCategory.Id);
            }
        });

        public ICommand SelectItemCommand => new RelayCommand<Item>(i =>
        {
            SelectedItem = i;
        });

        public ICommand AddNewCategoryCommand => new RelayCommand(_ =>
        {
            string newCategoryName;
            bool isNameProper = false;

            while (!isNameProper)
            {
                newCategoryDialog = new InputDialog(_stringResourcesDictionary.GetResource("MsgEnterNameOfNewCategory"),
                                                    _stringResourcesDictionary.GetResource("NewCategory"));

                if (newCategoryDialog.ShowDialog() == false)
                {
                    break;
                }

                newCategoryName = newCategoryDialog.Answer;

                if (_categoryService.GetCategory(newCategoryName) == null)
                {
                    _categoryService.InsertCategory(new Category() { Name = newCategoryName });

                    CategoriesList = _categoryService.GetCategories();
                    isNameProper = true;

                    ItemList = null;
                    SelectedCategory = null;
                }
                else
                {
                    MessageBox.Show(_stringResourcesDictionary.GetResource("MsgCategoryDuplicate"),
                                    appName, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        });

        public ICommand UpdateCategoryNameCommand => new RelayCommand(_ =>
        {
            string newCategoryName;
            bool isNameProper = false;

            while (isNameProper == false)
            {
                newCategoryDialog = new InputDialog(_stringResourcesDictionary.GetResource("MsgEnterNameOfNewCategory"),
                                                    _stringResourcesDictionary.GetResource("ChangeCategoryName"));

                if (newCategoryDialog.ShowDialog() == false)
                {
                    break;
                }

                newCategoryName = newCategoryDialog.Answer;

                if (_categoryService.GetCategory(newCategoryName) == null)
                {
                    Category updatedCategory = SelectedCategory;
                    updatedCategory.Name = newCategoryName;
                    _categoryService.UpdateCategory(updatedCategory);

                    CategoriesList = _categoryService.GetCategories();
                    isNameProper = true;
                }
                else
                {
                    MessageBox.Show(_stringResourcesDictionary.GetResource("MsgCategoryDuplicate"),
                                    appName, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        });

        public ICommand DeleteCategoryCommand => new RelayCommand(_ =>
        {
            if (MessageBox.Show(_stringResourcesDictionary.GetResource("MsgCategoryDelete"),
                appName, MessageBoxButton.YesNo, MessageBoxImage.Warning).Equals(MessageBoxResult.Yes))
            {
                _categoryService.DeleteCategory(SelectedCategory.Id);
                ItemList = null;
                SelectedCategory = null;
                CategoriesList = _categoryService.GetCategories();
            }
        });

        public ICommand UpdateItemCommand => new RelayCommand(_ =>
        {
            var ctgMngEditItem = _windowFactory.CreateWindow<CtgMngEditItem>();
            ctgMngEditItem.QuestionTexBox.Text = SelectedItem.Question;
            ctgMngEditItem.AnswerTexBox.Text = SelectedItem.Answer;

            if (ctgMngEditItem.ShowDialog() == true)
            {
                Item updatedItem = SelectedItem;
                updatedItem.Question = ctgMngEditItem.QuestionTexBox.Text;
                updatedItem.Answer = ctgMngEditItem.AnswerTexBox.Text;
                if (ctgMngEditItem.ResetItemCb.IsChecked == true)
                {
                    updatedItem.EFactor = 2.5;
                    updatedItem.Interval = 0;
                    updatedItem.Repetition = 0;
                    updatedItem.LastGrade = 0;
                    updatedItem.LastRepetitionDate = DateTime.Now;
                }

                _itemService.UpdateItem(updatedItem);

                ItemList = _itemService.GetItems(SelectedCategory.Id);
            }
        });

        public ICommand DeleteItemCommand => new RelayCommand(_ =>
        {
            if (MessageBox.Show(_stringResourcesDictionary.GetResource("MsgItemDelete"),
                appName, MessageBoxButton.YesNo, MessageBoxImage.Warning).Equals(MessageBoxResult.Yes))
            {
                _itemService.DeleteItem(SelectedItem.Id);
                ItemList = _itemService.GetItems(SelectedCategory.Id);
            }
        });
    }
}