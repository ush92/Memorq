using Memorq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Memorq.Core
{
    public class SM2
    {
        private int nextRepetition = 0;
        private double nextEfactor = 0;
        private int nextInterval = 0;

        public Item UpdateItemStats(Item item, int grade)
        {
            if (grade < 0) grade = 0;
            else if (grade > 5) grade = 5;

            if (grade >= 3)
            {
                if (item.Repetition == 0)
                {
                    nextInterval = 1;
                    nextRepetition = 1;
                }
                else if (item.Repetition == 1)
                {
                    nextInterval = 6;
                    nextRepetition = 2;
                }
                else
                {
                    nextInterval = (int)Math.Round(item.Interval * item.EFactor);
                    nextRepetition = item.Repetition + 1;
                }
            }
            else
            {
                nextInterval = 1;
                nextRepetition = 0;
            }

            nextEfactor = item.EFactor + (0.1 - (5 - grade) * (0.08 + (5 - grade) * 0.02));

            if (nextEfactor < 1.3) nextEfactor = 1.3;

            item.Repetition = nextRepetition;
            item.EFactor = nextEfactor;
            item.Interval = nextInterval;
            item.LastGrade = grade;
            item.LastRepetitionDate = DateTime.Now;

            return item;
        }
    }
}