using FluentAssertions;
using Memorq.Infrastructure;
using Memorq.Services;
using Memorq.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Memorq.Tests.ViewModels
{
    public class MainWindowViewModelTests
    {
        private readonly Mock<ICategoryService> _categoryService = new();
        private readonly Mock<IItemService> _itemService = new();
        private readonly Mock<IWindowFactory> _windowFactory = new();
        private readonly Mock<IStringResourcesDictionary> _stringResourcesDictionary = new();

        [Fact]
        public void IfNoDefaultCategoryThenIsDefaultCategoryChoosenIsFalse()
        {
            var mainWindowViewModel = new MainWindowViewModel(_categoryService.Object, _itemService.Object, _windowFactory.Object, _stringResourcesDictionary.Object);
            mainWindowViewModel.DefaultCategory = null;
            mainWindowViewModel.IsDefaultCategoryChoosen.Should().BeFalse();
        }

        [Fact]
        public void IfDefaultCategoryThenIsDefaultCategoryChoosenIsTrue()
        {
            var mainWindowViewModel = new MainWindowViewModel(_categoryService.Object, _itemService.Object, _windowFactory.Object, _stringResourcesDictionary.Object);
            mainWindowViewModel.DefaultCategory = new();
            mainWindowViewModel.IsDefaultCategoryChoosen.Should().BeTrue();
        }

        [Fact]
        public void IfNoDefaultCategoryThenDefaultCategoryNameHasDictionaryValueDefaultCategoryNotChosen()
        {
            var mainWindowViewModel = new MainWindowViewModel(_categoryService.Object, _itemService.Object, _windowFactory.Object, _stringResourcesDictionary.Object);
            mainWindowViewModel.DefaultCategory = null;
            mainWindowViewModel.DefaultCategoryName.Should().Be(_stringResourcesDictionary.Object.GetResource("DefaultCategoryNotChosen"));
        }

        [Fact]
        public void IfDefaultCategoryThenDefaultCategoryNameIsTheSame()
        {
            var mainWindowViewModel = new MainWindowViewModel(_categoryService.Object, _itemService.Object, _windowFactory.Object, _stringResourcesDictionary.Object);
            mainWindowViewModel.DefaultCategory = new();
            mainWindowViewModel.DefaultCategoryName.Should().Be(mainWindowViewModel.DefaultCategory.Name);
        }

        //[Fact]
        //public void ShowCategoryManagerCommandShouldSetDefaultCategory()
        //{
        //    //_windowFactory.Setup()
        //    var mainWindowViewModel = new MainWindowViewModel(_categoryService.Object, _itemService.Object, _windowFactory.Object, _stringResourcesDictionary.Object);
        //    mainWindowViewModel.ShowCategoryManagerCommand.Execute(null);


        //    mainWindowViewModel.DefaultCategory.Should().Be(10);

        //}
    }
}
