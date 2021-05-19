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
        private List<Item> _itemList;

        public List<Category> CategoriesList
        {
            get => _categoriesList;
            set
            {
                _categoriesList = value;
                //OnPropertyChanged(nameof(CategoriesList));
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

        public CategoryManagerViewModel(ICategoryProvider categoryProvider, IItemProvider itemProvider, IWindowFactory windowFactory)
        {
            _categoryProvider = categoryProvider;
            _itemProvider = itemProvider;
            _windowFactory = windowFactory;

            CategoriesList = _categoryProvider.GetCategories();
        }
    }
}
