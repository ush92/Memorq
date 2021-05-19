using Memorq.Infrastructure;
using Memorq.Models;
using Memorq.Services;
using System.Collections.Generic;

namespace Memorq.ViewModels
{
    public class CategoryManagerViewModel : BaseViewModel
    {
        private readonly ICategoryProvider _categoryProvider;
        private readonly IItemProvider _itemProvider;
        private readonly IWindowFactory _windowFactory;

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

        public CategoryManagerViewModel(ICategoryProvider categoryProvider, IItemProvider itemProvider, IWindowFactory windowFactory)
        {
            _categoryProvider = categoryProvider;
            _itemProvider = itemProvider;
            _windowFactory = windowFactory;

            CategoriesList = _categoryProvider.GetCategories();
        }
    }
}
