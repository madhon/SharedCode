using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace System
{
    [DebuggerStepThrough]
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this String text)
        {
            return String.IsNullOrEmpty(text);
        }

        public static bool HasValue(this String text)
        {
            return !String.IsNullOrEmpty(text);
        }

        public static string AsFormat(this string s, params object[] args)
        {
            return String.Format(s, args);
        }

        public static string FormatWith(this string s, params object[] args)
        {
            return String.Format(s, args);
        }

        public static string Append(this string origStr, string newStr)
        {
            return string.Concat(origStr, newStr);
        }

        public static string Preppend(this string origStr, string newStr)
        {
            return string.Concat(newStr, origStr);
        }

        public static string Repeat(this string str, int times)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < times; i++)
                sb.Append(str);
            return sb.ToString();
        }
    }
}
