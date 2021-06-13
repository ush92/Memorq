using Memorq.Models;
using SQLite;
using System;
using System.Collections.Generic;

namespace Memorq.Services
{
    public sealed class ItemService : IItemService
    {
        public List<Item> GetItems(int categoryId)
        {
            using SQLiteConnection connection = new SQLiteConnection(App.databasePath);
            return connection.Table<Item>().Where(c => c.CategoryId.Equals(categoryId)).OrderBy(c => c.Question).ToList();
        }

        public List<Item> GetItemsWithoutGrade(int categoryId)
        {
            using SQLiteConnection connection = new SQLiteConnection(App.databasePath);
            return connection.Table<Item>()
                .Where(c => c.CategoryId.Equals(categoryId) && c.LastGrade == null)
                .ToList();
        }

        public List<Item> GetItemsForTodayRepetition(int categoryId)
        {
            var today = DateTime.Today;
            using SQLiteConnection connection = new SQLiteConnection(App.databasePath);
            return connection.Table<Item>()
                .Where(c => c.CategoryId.Equals(categoryId))
                .Where(c => c.Repetition == 0 ||
                      (c.LastRepetitionDate.HasValue && c.LastRepetitionDate.Value.AddDays(c.Interval) <= today))
                .ToList();
        }

        public Item GetRandomItem(int categoryId)
        {
            var random = new Random();
            using SQLiteConnection connection = new SQLiteConnection(App.databasePath);
            int count = connection.Table<Item>().Where(c => c.CategoryId.Equals(categoryId)).Count();
            return connection.Table<Item>().Where(c => c.CategoryId.Equals(categoryId)).Skip(random.Next(0, count)).FirstOrDefault();
        }

        public void InsertItem(Item item)
        {
            using SQLiteConnection connection = new SQLiteConnection(App.databasePath);
            connection.Insert(item);
        }

        public void InsertItemsFromImport(List<Item> items, int categoryId)
        {
            using SQLiteConnection connection = new SQLiteConnection(App.databasePath);
            connection.RunInTransaction(() =>
            {
                foreach (var item in items)
                {
                    item.CategoryId = categoryId;
                    item.Repetition = 0;
                    item.EFactor = 2.5;
                    item.Interval = 0;
                    item.LastGrade = null;
                    item.InsertDate = DateTime.Now;
                    item.LastRepetitionDate = null;

                    connection.Insert(item);
                }
            });
        }

        public void UpdateItem(Item item)
        {
            using SQLiteConnection connection = new SQLiteConnection(App.databasePath);
            connection.Update(item);
        }

        public void DeleteItem(int id)
        {
            using SQLiteConnection connection = new SQLiteConnection(App.databasePath);
            connection.Table<Item>().Delete(i => i.Id == id);
        }
    }
}