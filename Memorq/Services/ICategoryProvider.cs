using Memorq.Models;
using System.Collections.Generic;

namespace Memorq.Services
{
    public interface ICategoryProvider
    {
        List<Category> GetCategories();

        public Category GetCategory(string name);
        public Category GetCategory(int id);

        void InsertCategory(Category category);

        void DeleteCategory(int id);
    }
}
