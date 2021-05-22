using Memorq.Models;
using System.Collections.Generic;

namespace Memorq.Services
{
    public interface IItemService
    {
        List<Item> GetItems(int categoryId);

        void InsertItem(Item item);
        void DeleteItem(int id);
        void UpdateItem(Item item);
    }
}