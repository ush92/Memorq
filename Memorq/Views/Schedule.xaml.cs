using Memorq.ViewModels;
using System.Windows;

namespace Memorq.Views
{
    public partial class Schedule : Window
    {
        public Schedule(ScheduleViewModel scheduleViewModel)
        {
            InitializeComponent();
            DataContext = scheduleViewModel;
            scheduleViewModel.OwnerWindow = this;
        }
    }
}
