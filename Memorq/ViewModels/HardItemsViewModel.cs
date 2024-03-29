﻿using Memorq.Infrastructure;
using Memorq.Models;
using Memorq.Services;
using System.Collections.Generic;

namespace Memorq.ViewModels
{
    public class HardItemsViewModel : BaseViewModel
    {
        private readonly IItemService _itemService;
        private readonly IStringResourcesDictionary _stringResourcesDictionary;

        private List<Item> _itemList;
        public List<Item> ItemList
        {
            get => _itemList;
            set
            {
                _itemList = value;
                OnPropertyChanged(nameof(ItemList));
            }
        }

        public HardItemsViewModel(IItemService itemService, IStringResourcesDictionary stringResourcesDictionary)
        {
            _itemService = itemService;
            _stringResourcesDictionary = stringResourcesDictionary;
        }
    }
}