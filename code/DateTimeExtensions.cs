using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace System
{
    [DebuggerStepThrough]
    public static class DateTimeExtensions
    {
        public static DateTime AddWeekdays(this DateTime date, int days)
        {
            var sign = days < 0 ? -1 : 1;
            var unsignedDays = Math.Abs(days);
            var weekdaysAdded = 0;
            while (weekdaysAdded < unsignedDays)
            {
                date = date.AddDays(sign);
                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                    weekdaysAdded++;
            }
            return date;
        }

        public static DateTime SetTime(this DateTime date, int hour)
        {
            return date.SetTime(hour, 0, 0, 0);
        }

        public static DateTime SetTime(this DateTime date, int hour, int minute)
        {
            return date.SetTime(hour, minute, 0, 0);
        }

        public static DateTime SetTime(this DateTime date, int hour, int minute, int second)
        {
            return date.SetTime(hour, minute, second, 0);
        }

        public static DateTime SetTime(this DateTime date, int hour, int minute, int second, int millisecond)
        {
            return new DateTime(date.Year, date.Month, date.Day, hour, minute, second, millisecond);
        }

        public static DateTime FirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }

        public static DateTime LastDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }

        public static bool IsWorkDay(this DateTime date, IEnumerable<IHolidayCalculator> holydayCalculators)
        {
            if (date.IsWeekend())
                return false;

            if (holydayCalculators != null)
            {
                foreach (var calculator in holydayCalculators)
                {
                    var holidays = calculator.GetHolidays(date.Year);
                    if (holidays.Contains(date.Date))
                        return false;
                }
            }

            return true;
        }

        public static bool IsWeekend(this DateTime date)
        {
            return date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday;
        }

        public static bool IsWeekday(this DateTime date)
        {
            return !IsWeekend(date);
        }

        public static DateTime GetNextWeekDay(this DateTime date)
        {
            if (date.DayOfWeek == DayOfWeek.Friday)
            {
                return date.AddDays(3);
            }
            if (date.DayOfWeek == DayOfWeek.Saturday)
            {
                return date.AddDays(2);
            }
            return date.AddDays(1);
        }

        public static MonthYear ToMonthYear(this DateTime date)
        {
            return new MonthYear(date.Month, date.Year);
        }

        public static DayMonth ToDayMonth(this DateTime date)
        {
            return new DayMonth(date.Day, date.Month);
        }


        public static int Age(this DateTime dateOfBirth)
        {
            if (DateTime.Today.Month < dateOfBirth.Month ||
                DateTime.Today.Month == dateOfBirth.Month &&
                DateTime.Today.Day < dateOfBirth.Day)
            {
                return DateTime.Today.Year - dateOfBirth.Year - 1;
            }
            return DateTime.Today.Year - dateOfBirth.Year;
        }

        public static string ToPrettyDate(this DateTime date)
        {
            var timeSince = DateTime.Now.Subtract(date);
            if (timeSince.TotalMilliseconds < 1) return "not yet";
            if (timeSince.TotalMinutes < 1) return "just now";
            if (timeSince.TotalMinutes < 2) return "1 minute ago";
            if (timeSince.TotalMinutes < 60) return string.Format("{0} minutes ago", timeSince.Minutes);
            if (timeSince.TotalMinutes < 120) return "1 hour ago";
            if (timeSince.TotalHours < 24) return string.Format("{0} hours ago", timeSince.Hours);
            if (timeSince.TotalDays == 1) return "yesterday";
            if (timeSince.TotalDays < 7) return string.Format("{0} day(s) ago", timeSince.Days);
            if (timeSince.TotalDays < 14) return "last week";
            if (timeSince.TotalDays < 21) return "2 weeks ago";
            if (timeSince.TotalDays < 28) return "3 weeks ago";
            if (timeSince.TotalDays < 60) return "last month";
            if (timeSince.TotalDays < 365) return string.Format("{0} months ago", Math.Round(timeSince.TotalDays / 30));
            if (timeSince.TotalDays < 730) return "last year";
            return string.Format("{0} years ago", Math.Round(timeSince.TotalDays / 365));
        }
    }
}