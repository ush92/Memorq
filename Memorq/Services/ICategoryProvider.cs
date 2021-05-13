using Memorq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorq.Services
{
    public interface ICategoryProvider
    {
        List<Category> GetCategories();

        void InsertCategory(Category category);
    }
}
