using Memorq.Models;
using SQLite;
using System.Collections.Generic;

namespace Memorq.Services
{
    public sealed class ItemProvider : IItemProvider
    {
        public List<Item> GetItems(int categoryId)
        {
            List<Item> items;

            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Item>();
                items = connection.Table<Item>().Where(c => c.CategoryId.Equals(categoryId)).OrderBy(c => c.Question).ToList();
            }

            return items;
        }

        public void InsertItem(Item item)
        {
            using SQLiteConnection connection = new SQLiteConnection(App.databasePath);
            connection.CreateTable<Item>();
            connection.Insert(item);
        }
    }
}
