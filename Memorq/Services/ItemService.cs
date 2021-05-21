using Memorq.Models;
using SQLite;
using System.Collections.Generic;

namespace Memorq.Services
{
    public sealed class ItemService : IItemService
    {
        public List<Item> GetItems(int categoryId)
        {
            using SQLiteConnection connection = new SQLiteConnection(App.databasePath);
            connection.CreateTable<Item>();
            return connection.Table<Item>().Where(c => c.CategoryId.Equals(categoryId)).OrderBy(c => c.Question).ToList();
        }

        public void InsertItem(Item item)
        {
            using SQLiteConnection connection = new SQLiteConnection(App.databasePath);
            connection.CreateTable<Item>();
            connection.Insert(item);
        }

        public void DeleteItem(int id)
        {
            using SQLiteConnection connection = new SQLiteConnection(App.databasePath);
            connection.Table<Item>().Delete(i => i.Id == id);
        }
    }
}
