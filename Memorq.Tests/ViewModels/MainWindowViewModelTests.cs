using FluentAssertions;
using Memorq.Core;
using Memorq.Infrastructure;
using Memorq.Services;
using Memorq.ViewModels;
using Moq;
using System.Windows;
using Xunit;

namespace Memorq.Tests.ViewModels
{
    public class MainWindowViewModelTests
    {
        private readonly Mock<ICategoryService> _categoryService = new();
        private readonly Mock<IItemService> _itemService = new();
        private readonly Mock<IWindowFactory> _windowFactory = new();
        private readonly Mock<IStringResourcesDictionary> _stringResourcesDictionary = new();
        private readonly Mock<IMemorqCore> _memorqCore = new();

        [Fact]
        public void IfNoDefaultCategoryThenIsDefaultCategoryChoosenIsFalse()
        {
            var mainWindowViewModel = new MainWindowViewModel(_categoryService.Object, _itemService.Object, _memorqCore.Object,
                                                              _windowFactory.Object, _stringResourcesDictionary.Object);
            mainWindowViewModel.DefaultCategory = null;
            mainWindowViewModel.IsDefaultCategoryChoosen.Should().BeFalse();
        }

        [Fact]
        public void IfDefaultCategoryThenIsDefaultCategoryChoosenIsTrue()
        {
            var mainWindowViewModel = new MainWindowViewModel(_categoryService.Object, _itemService.Object, _memorqCore.Object,
                                                              _windowFactory.Object, _stringResourcesDictionary.Object);
            mainWindowViewModel.DefaultCategory = new();
            mainWindowViewModel.IsDefaultCategoryChoosen.Should().BeTrue();
        }

        [Fact]
        public void IfNoDefaultCategoryThenDefaultCategoryNameHasDictionaryValue()
        {
            var mainWindowViewModel = new MainWindowViewModel(_categoryService.Object, _itemService.Object, _memorqCore.Object,
                                                              _windowFactory.Object, _stringResourcesDictionary.Object);
            mainWindowViewModel.DefaultCategory = null;
            mainWindowViewModel.DefaultCategoryName.Should().Be(_stringResourcesDictionary.Object.GetResource("DefaultCategoryNotChosen"));
        }

        [Fact]
        public void IfDefaultCategoryThenDefaultCategoryNameIsTheSame()
        {
            var mainWindowViewModel = new MainWindowViewModel(_categoryService.Object, _itemService.Object, _memorqCore.Object,
                                                              _windowFactory.Object, _stringResourcesDictionary.Object);
            mainWindowViewModel.DefaultCategory = new();
            mainWindowViewModel.DefaultCategoryName.Should().Be(mainWindowViewModel.DefaultCategory.Name);
        }

        [Fact]
        public void IfNewItemQuestionIsNotFilledThenIsItemReadyToAddIsFalse()
        {
            var mainWindowViewModel = new MainWindowViewModel(_categoryService.Object, _itemService.Object, _memorqCore.Object,
                                                              _windowFactory.Object, _stringResourcesDictionary.Object);
            mainWindowViewModel.NewItemQuestion = string.Empty;
            mainWindowViewModel.NewItemAnswer = "answer";
            mainWindowViewModel.CheckIfItemReadyToAdd.Execute(null);
            mainWindowViewModel.IsItemReadyToAdd.Should().Be(false);
        }

        [Fact]
        public void IfNewItemAnswerIsNotFilledThenIsItemReadyToAddIsFalse()
        {
            var mainWindowViewModel = new MainWindowViewModel(_categoryService.Object, _itemService.Object, _memorqCore.Object,
                                                              _windowFactory.Object, _stringResourcesDictionary.Object);
            mainWindowViewModel.NewItemQuestion = "question";
            mainWindowViewModel.NewItemAnswer = string.Empty;
            mainWindowViewModel.CheckIfItemReadyToAdd.Execute(null);
            mainWindowViewModel.IsItemReadyToAdd.Should().Be(false);
        }

        [Fact]
        public void IfNewItemQuestionAndAnswerAreFilledThenIsItemReadyToAddIsTrue()
        {
            var mainWindowViewModel = new MainWindowViewModel(_categoryService.Object, _itemService.Object, _memorqCore.Object,
                                                              _windowFactory.Object, _stringResourcesDictionary.Object);
            mainWindowViewModel.NewItemQuestion = "question";
            mainWindowViewModel.NewItemAnswer = "answer";
            mainWindowViewModel.CheckIfItemReadyToAdd.Execute(null);
            mainWindowViewModel.IsItemReadyToAdd.Should().Be(true);
        }

        [Fact]
        public void IfShowMainPanelButtonIsClickedThenMainPanelIsVisible()
        {
            var mainWindowViewModel = new MainWindowViewModel(_categoryService.Object, _itemService.Object, _memorqCore.Object,
                                                              _windowFactory.Object, _stringResourcesDictionary.Object);

            mainWindowViewModel.MainViewMode = Visibility.Collapsed;
            mainWindowViewModel.ShowMainPanel.Execute(null);
            mainWindowViewModel.MainViewMode.Should().Be(Visibility.Visible);
        }

        [Fact]
        public void IfShowAddItemPanelButtonIsClickedThenAddItemPanelIsVisible()
        {
            var mainWindowViewModel = new MainWindowViewModel(_categoryService.Object, _itemService.Object, _memorqCore.Object,
                                                              _windowFactory.Object, _stringResourcesDictionary.Object);

            mainWindowViewModel.AddItemMode = Visibility.Collapsed;
            mainWindowViewModel.ShowAddItemPanel.Execute(null);
            mainWindowViewModel.AddItemMode.Should().Be(Visibility.Visible);
        }

        [Fact]
        public void IfShowForcePanelButtonIsClickedThenForcePanelIsVisible()
        {
            var mainWindowViewModel = new MainWindowViewModel(_categoryService.Object, _itemService.Object, _memorqCore.Object,
                                                              _windowFactory.Object, _stringResourcesDictionary.Object);

            mainWindowViewModel.ForceMode = Visibility.Collapsed;
            mainWindowViewModel.ShowForcePanel.Execute(null);
            mainWindowViewModel.ForceMode.Should().Be(Visibility.Visible);
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
