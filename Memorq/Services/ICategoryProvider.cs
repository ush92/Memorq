using Memorq.Models;
using System.Collections.Generic;

namespace Memorq.Services
{
    public interface ICategoryProvider
    {
        List<Category> GetCategories();

        void InsertCategory(Category category);
    }
}
