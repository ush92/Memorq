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
            List<Category> categories;

            using (SQLiteConnection connection = new SQLiteConnection(App.databasePath))
            {
                connection.CreateTable<Category>();
                categories = connection.Table<Category>().ToList().OrderBy(c => c.Name).ToList();
            }

            return categories;
        }

        public void InsertCategory(Category category)
        {
            using SQLiteConnection connection = new SQLiteConnection(App.databasePath);
            connection.CreateTable<Category>();
            connection.Insert(category);
        }
    }
}
