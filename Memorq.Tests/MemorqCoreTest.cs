using AutoFixture;
using FluentAssertions;
using Memorq.Core;
using Memorq.Models;
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

        [Theory]
        [InlineData(0, 1.7000000000000002)]
        [InlineData(1, 1.96)]
        [InlineData(2, 2.1799999999999997)]
        [InlineData(3, 2.36)]
        [InlineData(4, 2.5)]
        [InlineData(5, 2.6)]
        public void NewItemFirstGradeSetsExpectedEFactor(int grade, double efactor)
        {
            var newItem = CreateDefaultItem();
            MemorqCore memorqCore = new();
            memorqCore.UpdateItemStats(newItem, grade);

            newItem.EFactor.Should().Be(efactor);
        }

        [Fact]
        public void EFactorCannotBeLessThan1_3()
        {
            var newItem = CreateDefaultItem();
            int grade = 1;

            MemorqCore memorqCore = new();
            for (int i = 0; i < 10; i++)
            {
                memorqCore.UpdateItemStats(newItem, grade);
            }

            newItem.EFactor.Should().Be(1.3);
        }

        [Fact]
        public void FirstIntervalsAreAlwaysTheSame()
        {
            var newItem1 = CreateDefaultItem();
            int grade1 = 5;

            var newItem2 = CreateDefaultItem();
            int grade2 = 0;

            MemorqCore memorqCore = new();
            memorqCore.UpdateItemStats(newItem1, grade1);
            memorqCore.UpdateItemStats(newItem2, grade2);

            newItem1.Interval.Should().Be(newItem2.Interval);
        }

        [Fact]
        public void IfItemGetsThreePositiveGradesInRowThenRepetitionEqualsThree()
        {
            var newItem = CreateDefaultItem();
            int grade0 = 3;
            int grade1 = 4;
            int grade2 = 5;

            MemorqCore memorqCore = new();
            memorqCore.UpdateItemStats(newItem, grade0);
            memorqCore.UpdateItemStats(newItem, grade1);
            memorqCore.UpdateItemStats(newItem, grade2);

            newItem.Repetition.Should().Be(3);
        }

        [Fact]
        public void IfTwoItemsFirstGradesAreDifferentAndNextGradesAreTheSameThenTheirIntervalsAreDifferent()
        {
            var newItem1 = CreateDefaultItem();
            int grade1 = 5;

            var newItem2 = CreateDefaultItem();
            int grade2 = 0;

            MemorqCore memorqCore = new();
            memorqCore.UpdateItemStats(newItem1, grade1);
            memorqCore.UpdateItemStats(newItem2, grade2);

            int nextGrade = 3;

            memorqCore.UpdateItemStats(newItem1, nextGrade);
            memorqCore.UpdateItemStats(newItem2, nextGrade);

            newItem1.Interval.Should().NotBe(newItem2.Interval);
        }

        [Fact]
        public void IntervalCannotBeGreaterThan712()
        {
            var newItem = CreateDefaultItem();
            int grade = 5;

            MemorqCore memorqCore = new();
            for (int i = 0; i < 10; i++)
            {
                memorqCore.UpdateItemStats(newItem, grade);
            }

            newItem.Interval.Should().Be(712);
        }

        [Fact]
        public void IfItemGetsThreeNegativeGradesInRowThenRepetitionEqualsZero()
        {
            var newItem = CreateDefaultItem();
            int grade0 = 0;
            int grade1 = 1;
            int grade2 = 2;

            MemorqCore memorqCore = new();
            memorqCore.UpdateItemStats(newItem, grade0);
            memorqCore.UpdateItemStats(newItem, grade1);
            memorqCore.UpdateItemStats(newItem, grade2);

            newItem.Repetition.Should().Be(0);
        }

        [Theory]
        [InlineData(0, 0, 0, 0, 0, 1)]
        [InlineData(0, 0, 0, 0, 5, 1)]
        [InlineData(0, 0, 0, 3, 3, 6)]
        [InlineData(3, 3, 1, 3, 5, 6)]
        [InlineData(1, 2, 3, 4, 5, 9)]
        [InlineData(2, 4, 3, 5, 4, 26)]
        [InlineData(3, 3, 3, 3, 3, 52)]
        [InlineData(4, 3, 4, 4, 4, 78)]
        [InlineData(4, 4, 4, 4, 4, 95)]
        [InlineData(5, 5, 3, 5, 5, 109)]
        [InlineData(5, 5, 5, 3, 5, 120)]
        [InlineData(5, 5, 5, 5, 5, 131)]
        public void ItemGradedFiveTimesHasExpectedInterval(int grade1, int grade2, int grade3, int grade4, int grade5, int interval)
        {
            var newItem = CreateDefaultItem();
            MemorqCore memorqCore = new();
            memorqCore.UpdateItemStats(newItem, grade1);
            memorqCore.UpdateItemStats(newItem, grade2);
            memorqCore.UpdateItemStats(newItem, grade3);
            memorqCore.UpdateItemStats(newItem, grade4);
            memorqCore.UpdateItemStats(newItem, grade5);

            newItem.Interval.Should().Be(interval);
        }

        [Fact]
        public void IfItemGetsThreePositiveGradesInRowAndNextGradeIsNegativeThenRepetitionEqualsZero()
        {
            var newItem = CreateDefaultItem();
            int grade0 = 3;
            int grade1 = 4;
            int grade2 = 5;

            int grade3 = 0;

            MemorqCore memorqCore = new();
            memorqCore.UpdateItemStats(newItem, grade0);
            memorqCore.UpdateItemStats(newItem, grade1);
            memorqCore.UpdateItemStats(newItem, grade2);
            memorqCore.UpdateItemStats(newItem, grade3);

            newItem.Repetition.Should().Be(0);
        }
    }
}