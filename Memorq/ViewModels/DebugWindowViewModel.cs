using Memorq.Infrastructure;
using Memorq.Models;
using Memorq.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Memorq.ViewModels
{
    public class DebugWindowViewModel : BaseViewModel
    {
        private readonly ICategoryProvider _categoryProvider;
        private readonly IWindowFactory _windowFactory;
        private List<Category> _categoriesList;
        private string _categoryToInsert;

        public List<Category> CategoriesList
        {
            get => _categoriesList;
            set
            {
                _categoriesList = value;
                OnPropertyChanged(nameof(CategoriesList));
            }
        }
        public string CategoryToInsert
        {
            get => _categoryToInsert;
            set
            {
                _categoryToInsert = value;
                OnPropertyChanged(nameof(CategoryToInsert));
            }
        }

        public DebugWindowViewModel(ICategoryProvider categoryProvider, IWindowFactory windowFactory)
        {
            _categoryProvider = categoryProvider;
            _windowFactory = windowFactory;
        }

        public ICommand InsertDBtestButtonCommand => new RelayCommand(_ =>
        {
            Category category = new Category()
            {
                Name = CategoryToInsert
            };

            if (category.Name != null)
            {
                if (!_categoryProvider.GetCategories().Any(c => c.Name == category.Name))
                {
                    _categoryProvider.InsertCategory(category);
                }
                else
                {
                    MessageBox.Show(GetDictResource("MsgCategoryDuplicate"), "Memorq", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            else
            {
                MessageBox.Show(GetDictResource("MsgCategoryNoName"), "Memorq", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        });

        public ICommand ReadDBtestButtonCommand => new RelayCommand(_ => CategoriesList = _categoryProvider.GetCategories());
    }
}