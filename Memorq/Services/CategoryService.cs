using Memorq.Models;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace Memorq.Services
{
    public sealed class CategoryService : ICategoryService
    {
        public List<Category> GetCategories()
        {
            using SQLiteConnection connection = new SQLiteConnection(App.databasePath);
            connection.CreateTable<Category>();
            return connection.Table<Category>().ToList().OrderBy(c => c.Name).ToList();
        }

        public Category GetCategory(string name)
        {
            using SQLiteConnection connection = new SQLiteConnection(App.databasePath);
            connection.CreateTable<Category>();
            return connection.Table<Category>().FirstOrDefault(c => c.Name.Equals(name));
        }

        public Category GetCategory(int id)
        {
            using SQLiteConnection connection = new SQLiteConnection(App.databasePath);
            connection.CreateTable<Category>();
            return connection.Table<Category>().FirstOrDefault(c => c.Id.Equals(id));
        }

        public void InsertCategory(Category category)
        {
            using SQLiteConnection connection = new SQLiteConnection(App.databasePath);
            connection.CreateTable<Category>();
            connection.Insert(category);
        }

        public void UpdateCategory(Category category)
        {
            using SQLiteConnection connection = new SQLiteConnection(App.databasePath);
            connection.CreateTable<Category>();
            connection.Update(category);
        }

        public void DeleteCategory(int id)
        {
            using SQLiteConnection connection = new SQLiteConnection(App.databasePath);
            connection.Table<Item>().Delete(c => c.CategoryId == id);
            connection.Table<Category>().Delete(c => c.Id == id);
        }
    }
}
