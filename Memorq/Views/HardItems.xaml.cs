using Memorq.ViewModels;
using System.Windows;

namespace Memorq.Views
{
    public partial class HardItems : Window
    {
        public HardItems(HardItemsViewModel hardItemsViewModel)
        {
            InitializeComponent();
            DataContext = hardItemsViewModel;
            hardItemsViewModel.OwnerWindow = this;
        }
    }
}