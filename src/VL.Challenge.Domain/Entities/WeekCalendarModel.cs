using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VL.Challenge.Domain.Entities
{
    public class WeekCalendarModel
    {
        public DayOfWeek CurrentDayOfWeek { get; set; }
        public int CurrentDayIndex { get; set; }
        public List<DateTime> WeekDays { get; set; }
        public WeekCalendarModel()
        {
            DateTime today = DateTime.Today;

            CurrentDayOfWeek = DateTime.Now.DayOfWeek;
            CurrentDayIndex = (int) Enum.Parse(typeof(DayOfWeek), CurrentDayOfWeek.ToString());
            WeekDays = new List<DateTime>();
            WeekDays.Insert(CurrentDayIndex, today);


            for (int weekDayIndex = 0; weekDayIndex < 7; weekDayIndex++)
            {
                if (CurrentDayIndex > weekDayIndex)
                {
                    WeekDays.Insert(weekDayIndex, today.AddDays(-(CurrentDayIndex - weekDayIndex)));
                }
                else if (CurrentDayIndex < weekDayIndex)
                {
                    WeekDays.Insert(weekDayIndex, today.AddDays(+(weekDayIndex - CurrentDayIndex)));
                }
            }
        }
    }
}
