using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace System
{
    [DebuggerStepThrough]
    public class EasterHolidayCalculator : IHolidayCalculator
    {
        public IEnumerable<DateTime> GetHolidays(int year)
        {
            var g = year % 19;
            var i = (19 * g + 15) % 30;
            var j = (year + (year / 4) + i) % 7;
            var c = (year / 100);
            var h = (c - c / 4 - ((8 * c + 13) / 25) + 19 * g + 15) % 30;
            i = h - (h / 28) * (1 - (29 / (h + 1)) * ((21 - g) / 11));
            j = (year + (year / 4) + i + 2 - c + c / 4) % 7;
            var l = i - j;

            var month = 3 + ((l + 40) / 44);
            var day = l + 28 - 31 * (month / 4);

            return new DateTime[] { new DateTime(year, month, day) };
        }
    }
}
