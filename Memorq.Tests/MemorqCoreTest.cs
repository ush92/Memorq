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
        [InlineData(1.7000000000000002, 1.96, 2.1799999999999997, 2.36, 2.5, 2.6)]
        public void NewItemFirstGradeSetsProperEFactor(double a, double b, double c, double d, double e, double f)
        {
            var newItem = new Item[6];
            MemorqCore memorqCore = new();
            for (int i = 0; i < 6; i++)
            {
                newItem[i] = CreateDefaultItem();
                memorqCore.UpdateItemStats(newItem[i], i);
            }

            newItem[0].EFactor.Should().Be(a);
            newItem[1].EFactor.Should().Be(b);
            newItem[2].EFactor.Should().Be(c);
            newItem[3].EFactor.Should().Be(d);
            newItem[4].EFactor.Should().Be(e);
            newItem[5].EFactor.Should().Be(f);
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

        [Fact]
        public void IfItemGetsThreeGrades3InRowThenIntervalEqualsThirteen()
        {
            var newItem = CreateDefaultItem();
            int grade = 3;

            MemorqCore memorqCore = new();
            memorqCore.UpdateItemStats(newItem, grade);
            memorqCore.UpdateItemStats(newItem, grade);
            memorqCore.UpdateItemStats(newItem, grade);

            newItem.Interval.Should().Be(13);
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
