using System;
namespace klevr.Helpers
{
    public static class DateTimeHelper
    {
        public static bool checkIfDateIsToday(DateTime date)
        {
            var today = DateTime.Now;
            return date.Year == today.Year && date.DayOfYear == today.DayOfYear;
        }
    }
}
