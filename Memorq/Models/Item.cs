using SQLite;
using System;

namespace Memorq.Models
{
    public sealed class Item
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed, NotNull]
        public int CategoryId { get; set; }

        [NotNull]
        public string Question { get; set; }

        [NotNull]
        public string Answer { get; set; }

        [NotNull]
        public int Repetition { get; set; }

        [NotNull]
        public double EFactor { get; set; }

        [NotNull]
        public int Interval { get; set; }

        public int LastGrade { get; set; }

        [NotNull]
        public DateTime InsertDate { get; set; }

        [NotNull]
        public DateTime LastRepetitionDate { get; set; }
    }
}
