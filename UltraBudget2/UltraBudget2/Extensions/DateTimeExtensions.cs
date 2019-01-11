using System;

namespace UltraBudget2.Extensions
{
    public static class DateTimeExtensions
    {
        public static DateTime GetFirstDayOfMonth(this DateTime value)
        {
            return new DateTime(value.Year, value.Month, 1);
        }

        public static DateTime BudgetIdToDatetime(string id)
        {
            var year = int.Parse(id.Substring(id.Length - 6, 4));
            var month = int.Parse(id.Substring(id.Length - 2));

            return new DateTime(year, month, 1);
        }
    }
}
