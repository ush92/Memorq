using System;

namespace Memorq.ViewModels
{
    public class ScheduleDay
    {
        public string Date;
        public int Count;

        public override string ToString()
        {
            return this.Date + "          " + this.Count;
        }
    }
}