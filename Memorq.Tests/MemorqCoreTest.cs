using AutoFixture;
using FluentAssertions;
using Memorq.Core;
using Memorq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Memorq.Tests
{
    public class MemorqCoreTest
    {
        private readonly Fixture fixture = new();

        private Item CreateDefaultItem()
        {
            var newItem = fixture.Create<Item>();
            newItem.Repetition = 0;
            newItem.EFactor = 2.5;
            newItem.Interval = 0;
            return newItem;
        }

        [Fact]
        public void IfNewItemGradeIs5EFactorEquals2_6()
        {
            var newItem = CreateDefaultItem();
            int grade = 5;

            MemorqCore memorqCore = new();
            memorqCore.UpdateItemStats(newItem, grade);

            newItem.EFactor.Should().Be(2.6);
        }
    }
}
