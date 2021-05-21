using Memorq.Models;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace Memorq.Services
{
    public sealed class CategoryProvider : ICategoryProvider
    {
        public List<Category> GetCategories()
        {
            var categories = new List<Category>();

            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Category>();
                categories = connection.Table<Category>().ToList().OrderBy(c => c.Name).ToList();
            }

            return categories;
        }

        public Category GetCategory(string name)
        {
            Category category = null;
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Category>();
                category = connection.Table<Category>().FirstOrDefault(c => c.Name.Equals(name));
            }
            return category;
        }

        public Category GetCategory(int id)
        {
            Category category = null;
            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Category>();
                category = connection.Table<Category>().FirstOrDefault(c => c.Id.Equals(id));
            }
            return category;
        }

        public void InsertCategory(Category category)
        {
            using SQLiteConnection connection = new SQLiteConnection(App.databasePath);
            connection.CreateTable<Category>();
            connection.Insert(category);
        }
        public void DeleteCategory(int id)
        {
            using SQLiteConnection connection = new SQLiteConnection(App.databasePath);
            connection.Table<Item>().Delete(c => c.CategoryId == id);
            connection.Table<Category>().Delete(c => c.Id == id);
        }
    }
}
