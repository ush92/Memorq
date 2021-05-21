using Memorq.Infrastructure;
using Memorq.Models;
using Memorq.Services;
using Memorq.Views.Dialogs;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;

namespace Memorq.ViewModels
{
    public class CategoryManagerViewModel : BaseViewModel
    {
        private readonly ICategoryService _categoryProvider;
        private readonly IItemService _itemProvider;
        private readonly IWindowFactory _windowFactory;

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

        public CategoryManagerViewModel(ICategoryService categoryProvider, IItemService itemProvider, IWindowFactory windowFactory)
        {
            _categoryProvider = categoryProvider;
            _itemProvider = itemProvider;
            _windowFactory = windowFactory;

            CategoriesList = _categoryProvider.GetCategories();
        }

        public ICommand UpdateItemsBySelectCategoryCommand => new RelayCommand<Category>(c =>
        {
            SelectedCategory = c;
            if (SelectedCategory != null)
            {
                ItemList = _itemProvider.GetItems(SelectedCategory.Id);         
            }
        });

        public ICommand SelectItemCommand => new RelayCommand<Item>(i =>
        {
            SelectedItem = i;
        });

        public ICommand AddNewCategoryCommand => new RelayCommand(_ =>
        {
            string newCategoryName;
            bool isNameProperOrCancelled = false;

            while (isNameProperOrCancelled == false)
            {
                newCategoryDialog = new InputDialog(GetDictResource("MsgEnterNameOfNewCategory"));
                if (newCategoryDialog.ShowDialog() == false)
                {
                    break;
                }

                newCategoryName = newCategoryDialog.Answer;
                if (!newCategoryName.Equals(string.Empty))
                {
                    if (_categoryProvider.GetCategory(newCategoryName) == null)
                    {
                        _categoryProvider.InsertCategory(new Category() { Name = newCategoryName });

                        CategoriesList = _categoryProvider.GetCategories();
                        isNameProperOrCancelled = true;

                        ItemList = null;
                        SelectedCategory = null;
                    }
                    else
                    {
                        MessageBox.Show(GetDictResource("MsgCategoryDuplicate"), appName, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show(GetDictResource("MsgCategoryNoName"), appName, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        });

        public ICommand ChangeCategoryNameCommand => new RelayCommand(_ =>
        {
            string newCategoryName;
            bool isNameProperOrCancelled = false;

            while (isNameProperOrCancelled == false)
            {
                newCategoryDialog = new InputDialog(GetDictResource("MsgEnterNameOfNewCategory"));
                if (newCategoryDialog.ShowDialog() == false)
                {
                    break;
                }

                newCategoryName = newCategoryDialog.Answer;
                if (!newCategoryName.Equals(string.Empty))
                {
                    if (_categoryProvider.GetCategory(newCategoryName) == null)
                    {
                        Category updatedCategory = SelectedCategory;
                        updatedCategory.Name = newCategoryName;
                        _categoryProvider.UpdateCategory(updatedCategory);

                        CategoriesList = _categoryProvider.GetCategories();
                        isNameProperOrCancelled = true;
                    }
                    else
                    {
                        MessageBox.Show(GetDictResource("MsgCategoryDuplicate"), appName, MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                else
                {
                    MessageBox.Show(GetDictResource("MsgCategoryNoName"), appName, MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        });

        public ICommand DeleteCategoryCommand => new RelayCommand(_ =>
        {
            if (MessageBox.Show(GetDictResource("MsgCategoryDelete"),
                appName, MessageBoxButton.YesNo, MessageBoxImage.Warning).Equals(MessageBoxResult.Yes))
            {
                _categoryProvider.DeleteCategory(SelectedCategory.Id);
                ItemList = null;
                SelectedCategory = null;
                CategoriesList = _categoryProvider.GetCategories();
            }
        });

        public ICommand DeleteItemCommand => new RelayCommand(_ =>
        {
            if (MessageBox.Show(GetDictResource("MsgItemDelete"),
                appName, MessageBoxButton.YesNo, MessageBoxImage.Warning).Equals(MessageBoxResult.Yes))
            {
                _itemProvider.DeleteItem(SelectedItem.Id);
                ItemList = _itemProvider.GetItems(SelectedCategory.Id);
            }
        });
    }
}
