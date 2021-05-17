using Memorq.Infrastructure;
using Memorq.Models;
using Memorq.Services;
using Memorq.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public ICommand InsertDBtestButtonCommand => new RelayCommand(_ => {
            Category category = new Category()
            {
                Name = CategoryToInsert
            };

            if (category.Name == null) category.Name = "default";
            _categoryProvider.InsertCategory(category);
        });

        public ICommand ReadDBtestButtonCommand => new RelayCommand(_ => CategoriesList = _categoryProvider.GetCategories());
    }
}

//Using of string resource from Dictionary.xaml resource dictionary:
//string localizedMessage = (string)Application.Current.FindResource("NewBtn_right");
