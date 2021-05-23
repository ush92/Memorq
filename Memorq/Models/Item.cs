using SQLite;
using System;
using System.Text.Json.Serialization;

namespace Memorq.Models
{
    public sealed class Item
    {
        [PrimaryKey, AutoIncrement, JsonIgnore]
        public int Id { get; set; }

        [Indexed, NotNull, JsonIgnore]
        public int CategoryId { get; set; }

        [NotNull]
        public string Question { get; set; }

        [NotNull]
        public string Answer { get; set; }

        [NotNull, JsonIgnore]
   
        public int Repetition { get; set; }

        [NotNull, JsonIgnore]
        public double EFactor { get; set; }

        [NotNull, JsonIgnore]

        public int Interval { get; set; }

        [JsonIgnore]
        public int? LastGrade { get; set; }

        [NotNull, JsonIgnore]

        public DateTime InsertDate { get; set; }

        [JsonIgnore]
        public DateTime? LastRepetitionDate { get; set; }
    }
}