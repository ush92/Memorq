using SQLite;
using System.Collections.Generic;

namespace Memorq.Models
{
    public sealed class Category
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        [Unique, NotNull]
        public string Name { get; set; }
    }
}
