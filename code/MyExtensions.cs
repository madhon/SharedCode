using System;
using System.Collections.Generic;

namespace System
{
    public static class ExtensionMethods
    {
		
        public static int Count<T>(this IEnumerable<T> source)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            ICollection<T> col = source as ICollection<T>;
            if (col != null)
                return col.Count;

            int count = 0;
            using (IEnumerator<T> enumerator = source.GetEnumerator())
                while (enumerator.MoveNext())
                    count++;

            return count;
        }
        
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> enumerable)
        {
           return new HashSet<T>(enumerable);
        }
        

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return list == null || !list.Any();
        }

        public static bool IsNullOrEmpty(this String text)
        {
            return String.IsNullOrEmpty(text);
        }
        
        public static string Slice(this string s, int start, int end)
        {
            return s.Substring(start, end - start);
        }
        
        public static string ToBase64(this string s)
        {
            var b = Encoding.Default.GetBytes(s);
            return Convert.ToBase64String(b);
        }
        
        public static string FromBase64(this string s)
        {
            var b = Convert.FromBase64String(s);
            return Encoding.Default.GetString(b);
        }
        
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T element in source)
            {
                action(element);
            }
        }
		
		 public static string NullTrim(this string s)
        {
            if (s == null)
            {
                return null;
            }
 
            s = s.Trim();
            return s == String.Empty ? null : s;
        }
 
        public static bool IsSet(this string s)
        {
            return s.NullTrim() != null;
        }
 
        public static string AsFormat(
            this string s, params object[] args)
        {
            return String.Format(s, args);
        }
		
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

public static bool IsWeekday(this DateTime date)
		{
			return !IsWeekend(date);
		}

public static bool IsWeekend(this DateTime date)
		{
			return date.DayOfWeek == DayOfWeek.Sunday
			       || date.DayOfWeek == DayOfWeek.Saturday;
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

        public static string ToDateString(this DateTime date)
        {
            return date.ToString("dddd, dd MMMM yyyy");
        }

    }
}