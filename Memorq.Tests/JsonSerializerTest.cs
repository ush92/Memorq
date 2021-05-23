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
        private readonly Fixture fixture = new Fixture();

        [Fact]
        public void JsonSerializerWorksProperly()
        {
            var listToSerialize = fixture.CreateMany<Item>().ToList();
            var jsonString = JsonSerializer.Serialize(listToSerialize);
            var deserializedList = JsonSerializer.Deserialize<List<Item>>(jsonString);

            deserializedList.Should().BeEquivalentTo(listToSerialize);
        }
    }
}
