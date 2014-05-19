using System;
using System.Diagnostics;

namespace System
{
    [DebuggerStepThrough]
    public struct DayMonth : IComparable<DayMonth>
    {
        public int Day { get; private set; }
        public int Month { get; private set; }

        // Used internally to test february dates
        private const int exampleLeapYear = 2000;

        public DayMonth(int day, int month)
            : this()
        {
            new DateTime(exampleLeapYear, month, day);

            this.Day = day;
            this.Month = month;
        }

        public DateTime GetDate(int year)
        {
            return new DateTime(year, Month, Day);
        }

        public int CompareTo(DayMonth other)
        {
            return this.GetHashCode() - other.GetHashCode();
        }

        public override string ToString()
        {
            return GetDate(exampleLeapYear).ToString("dd/MM");
        }

        public override bool Equals(object obj)
        {
            return this == (DayMonth)obj;
        }

        public override int GetHashCode()
        {
            return Month * 31 + Day;
        }

        #region " Operators "

        public static bool operator ==(DayMonth a, DayMonth b)
        {
            return a.GetHashCode() == b.GetHashCode();
        }

        public static bool operator !=(DayMonth a, DayMonth b)
        {
            return a.GetHashCode() != b.GetHashCode();
        }

        public static bool operator <(DayMonth a, DayMonth b)
        {
            return a.GetHashCode() < b.GetHashCode();
        }

        public static bool operator >(DayMonth a, DayMonth b)
        {
            return a.GetHashCode() > b.GetHashCode();
        }

        public static bool operator <=(DayMonth a, DayMonth b)
        {
            return a.GetHashCode() <= b.GetHashCode();
        }

        public static bool operator >=(DayMonth a, DayMonth b)
        {
            return a.GetHashCode() >= b.GetHashCode();
        }

        public static bool operator ==(DayMonth dayMonth, DateTime date)
        {
            return date.Day == dayMonth.Day && date.Month == dayMonth.Month;
        }

        public static bool operator !=(DayMonth dayMonth, DateTime date)
        {
            return !(dayMonth == date);
        }

        public static bool operator ==(DateTime date, DayMonth dayMonth)
        {
            return (dayMonth == date);
        }

        public static bool operator !=(DateTime date, DayMonth dayMonth)
        {
            return !(dayMonth == date);
        }

        #endregion
    }
}
