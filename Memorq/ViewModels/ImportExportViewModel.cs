using Memorq.Infrastructure;
using Memorq.Models;
using Memorq.Services;
using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Input;

namespace Memorq.ViewModels
{
    public class ImportExportViewModel : BaseViewModel
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

        public int DefaultCategoryId { get; set; }

        public ImportExportViewModel(IItemService itemService, IStringResourcesDictionary stringResourcesDictionary)
        {
            _itemService = itemService;
            _stringResourcesDictionary = stringResourcesDictionary;
        }

        public ICommand ImportCommand => new RelayCommand(_ =>
        {
            var openFileDialog = new OpenFileDialog
            {
                Filter = ".json files (*.json)|*.json",
                Title = _stringResourcesDictionary.GetResource("Import")
            };

            string json = string.Empty;

            if (openFileDialog.ShowDialog() == true)
            {
                try
                {
                    json = File.ReadAllText(openFileDialog.FileName);
                    var importedItems = JsonSerializer.Deserialize<List<Item>>(json);

                    _itemService.InsertItemsFromImport(importedItems, DefaultCategoryId);

                    MessageBox.Show(_stringResourcesDictionary.GetResource("MsgImportSuccess"),
                        appName, MessageBoxButton.OK, MessageBoxImage.Information);

                    ItemList = _itemService.GetItems(DefaultCategoryId);
                }
                catch
                {
                    MessageBox.Show(_stringResourcesDictionary.GetResource("MsgImportError"),
                        appName, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        });

        public ICommand ExportCommand => new RelayCommand(_ =>
        {
            var saveFileDialog = new SaveFileDialog
            {
                Filter = ".json files (*.json)|*.json",
                Title = _stringResourcesDictionary.GetResource("Export")
            };

            if (saveFileDialog.ShowDialog() == true)
            {
                try
                {
                    var itemsToExport = _itemService.GetItems(DefaultCategoryId);

                    File.WriteAllText(saveFileDialog.FileName, JsonSerializer.Serialize(itemsToExport));

                    MessageBox.Show(_stringResourcesDictionary.GetResource("MsgExportSuccess"),
                        appName, MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show(_stringResourcesDictionary.GetResource("MsgExportError"),
                        appName, MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        });
    }
}