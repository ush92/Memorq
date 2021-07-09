using Memorq.Infrastructure;
using Memorq.Models;
using Memorq.Services;
using System.Collections.Generic;

namespace Memorq.ViewModels
{
    public class ScheduleViewModel : BaseViewModel
    {
        private readonly IItemService _itemService;
        private readonly IStringResourcesDictionary _stringResourcesDictionary;

        private List<ScheduleDay> _itemList;
        public List<ScheduleDay> ItemList
        {
            get => _itemList;
            set
            {
                _itemList = value;
                OnPropertyChanged(nameof(ItemList));
            }
        }

        public ScheduleViewModel(IItemService itemService, IStringResourcesDictionary stringResourcesDictionary)
        {
            _itemService = itemService;
            _stringResourcesDictionary = stringResourcesDictionary;
        }
    }
}