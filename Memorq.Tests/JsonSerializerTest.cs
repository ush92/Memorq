using AutoFixture;
using FluentAssertions;
using Memorq.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Xunit;

namespace Memorq.Tests
{
    public class JsonSerializerTest
    {
        private readonly Fixture fixture = new();

        [Fact]
        public void JsonSerializerWorksProperlyWithIgnoreJsonAtributes()
        {
            fixture.Customize<Item>(o => o
               .With(i => i.Question)
               .With(i => i.Answer)

               .Without(i => i.CategoryId)
               .Without(i => i.EFactor)
               .Without(i => i.Id)
               .Without(i => i.InsertDate)
               .Without(i => i.Interval)
               .Without(i => i.LastGrade)
               .Without(i => i.LastRepetitionDate)
               .Without(i => i.Repetition)
               );

            var listToSerialize = fixture.CreateMany<Item>().ToList();

            var jsonString = JsonSerializer.Serialize(listToSerialize);
            var deserializedList = JsonSerializer.Deserialize<List<Item>>(jsonString);

            deserializedList.Should().BeEquivalentTo(listToSerialize);
        }
    }
}
