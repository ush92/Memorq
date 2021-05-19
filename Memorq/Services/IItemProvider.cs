using Memorq.Models;
using System.Collections.Generic;

namespace Memorq.Services
{
    public interface IItemProvider
    {
        List<Item> GetItems(int categoryId);

        void InsertItem(Item item);
    }
}
