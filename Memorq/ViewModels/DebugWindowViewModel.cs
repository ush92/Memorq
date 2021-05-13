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
using Task.Commands;

namespace Memorq.ViewModels
{
    public class DebugWindowViewModel : INotifyPropertyChanged
    {
        private readonly ICategoryProvider _categoryProvider;

        public ObservableCollection<UIElement> DebugWindowControls { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;


        //public DebugWindowViewModel(ICategoryProvider categoryProvider)
        //{
        //    DebugWindowControls = new ObservableCollection<UIElement> { new DebugWindow() };
        //    _categoryProvider = categoryProvider;
        //}


        public ICommand InsertDBtestButtonCommand => new CommandHandler(CanExecute, OnCategoriesLoad);

        public ICommand ReadDBtestButtonCommand => new CommandHandler(CanExecute, OnCategorySave);



        private bool CanExecute(object parameter) => true;

        private void OnCategoriesLoad(object parameter)
        {

        }

        private void OnCategorySave(object parameter)
        {

        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //public void OnDebugWindowLoaded(object sender, RoutedEventArgs e)
        //{
        //    DebugWindowControls.Add(new WorkplanListView());
        //}

    }
}
