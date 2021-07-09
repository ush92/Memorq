using Memorq.Models;
using System;
using System.Collections.Generic;

namespace Memorq.Services
{
    public interface IItemService
    {
        List<Item> GetItems(int categoryId);
        List<Item> GetItemsWithoutGrade(int categoryId);
        List<Item> GetItemsForTodayRepetition(int categoryId);
        List<Item> GetItemsForRepetitionByDate(int categoryId, DateTime date);
        List<Item> GetHardItems(int categoryId);
        void InsertItem(Item item);
        void InsertItemsFromImport(List<Item> item, int categoryId);
        void DeleteItem(int id);
        void UpdateItem(Item item);
    }
}