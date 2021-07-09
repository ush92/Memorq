using AutoFixture;
using FluentAssertions;
using Memorq.Core;
using Memorq.Infrastructure;
using Memorq.Models;
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

        private readonly Fixture fixture = new();

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
        public void IfForceModeTipButtonIsClickedAndAnswerIsHiddenThenFirstLetterOfAnswerAppearsInAnswerField()
        {
            var mainWindowViewModel = new MainWindowViewModel(_categoryService.Object, _itemService.Object, _memorqCore.Object,
                                                              _windowFactory.Object, _stringResourcesDictionary.Object);

            mainWindowViewModel.ForceCurrentItem = fixture.Create<Item>();
            mainWindowViewModel.ForceAnswer = string.Empty;
            mainWindowViewModel.ForcePanelShowTip.Execute(null);
            mainWindowViewModel.ForceAnswer.Should().Be(mainWindowViewModel.ForceCurrentItem.Answer[0].ToString());
        }

        [Fact]
        public void IfForceModeTipButtonIsClickedAndAnswerIsShownThenAnswerFieldIsNotChanged()
        {
            var mainWindowViewModel = new MainWindowViewModel(_categoryService.Object, _itemService.Object, _memorqCore.Object,
                                                              _windowFactory.Object, _stringResourcesDictionary.Object);

            mainWindowViewModel.ForceCurrentItem = fixture.Create<Item>();
            mainWindowViewModel.ForceAnswer = mainWindowViewModel.ForceCurrentItem.Answer;
            mainWindowViewModel.ForcePanelShowTip.Execute(null);
            mainWindowViewModel.ForceAnswer.Should().Be(mainWindowViewModel.ForceCurrentItem.Answer);
        }

        [Fact]
        public void IfForceModeShowAnswerButtonIsClickedThenAnswerIsVisible()
        {
            var mainWindowViewModel = new MainWindowViewModel(_categoryService.Object, _itemService.Object, _memorqCore.Object,
                                                              _windowFactory.Object, _stringResourcesDictionary.Object);

            mainWindowViewModel.ForceCurrentItem = fixture.Create<Item>();
            mainWindowViewModel.ForceAnswer = string.Empty;
            mainWindowViewModel.ForcePanelShowAnswer.Execute(null);
            mainWindowViewModel.ForceAnswer.Should().Be(mainWindowViewModel.ForceCurrentItem.Answer);
        }

        [Fact]
        public void IfGradeNewItemShowAnswerButtonIsClickedThenGradePanelIsVisible()
        {
            var mainWindowViewModel = new MainWindowViewModel(_categoryService.Object, _itemService.Object, _memorqCore.Object,
                                                              _windowFactory.Object, _stringResourcesDictionary.Object);

            mainWindowViewModel.ToGradeCurrentItem = fixture.Create<Item>();
            mainWindowViewModel.GradeNewItemGradesPanel = Visibility.Collapsed;
            mainWindowViewModel.GradeNewItemsShowAnswer.Execute(null);
            mainWindowViewModel.GradeNewItemGradesPanel.Should().Be(Visibility.Visible);
        }

        [Fact]
        public void IfGradeNewItemShowAnswerButtonIsClickedThenAnswerIsVisible()
        {
            var mainWindowViewModel = new MainWindowViewModel(_categoryService.Object, _itemService.Object, _memorqCore.Object,
                                                              _windowFactory.Object, _stringResourcesDictionary.Object);

            mainWindowViewModel.ToGradeCurrentItem = fixture.Create<Item>();
            mainWindowViewModel.ToGradeAnswer = string.Empty;
            mainWindowViewModel.GradeNewItemsShowAnswer.Execute(null);
            mainWindowViewModel.ToGradeAnswer.Should().Be(mainWindowViewModel.ToGradeCurrentItem.Answer);
        }
    }
}
