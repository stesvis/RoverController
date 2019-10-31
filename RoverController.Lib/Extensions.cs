using System;
using System.Collections.Generic;
using System.Linq;

namespace RoverController.Lib
{
    public static class Extensions
    {
        #region String

        public static bool IsEmpty(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Returns a string with backslashes before characters that need to be quoted
        /// </summary>
        /// <param name="str">Text string need to be escape with slashes</param>
        public static string AddSlashes(this string str)
        {
            // List of characters handled: \000 null \010 backspace \011 horizontal tab \012 new line
            // \015 carriage return \032 substitute \042 double quote \047 single quote \134
            // backslash \140 grave accent

            string Result = str;

            try
            {
                Result = System.Text.RegularExpressions.Regex.Replace(str, @"[\000\010\011\012\015\032\042\047\134\140]", "\\$0");
            }
            catch (Exception Ex)
            {
                // handle any exception here
                Console.WriteLine(Ex.Message);
            }

            return Result;
        }

        /// <summary>
        /// Un-quotes a quoted string
        /// </summary>
        /// <param name="str">Text string need to be escape with slashes</param>
        public static string StripSlashes(this string str)
        {
            // List of characters handled: \000 null \010 backspace \011 horizontal tab \012 new line
            // \015 carriage return \032 substitute \042 double quote \047 single quote \134
            // backslash \140 grave accent

            string Result = str;

            try
            {
                Result = System.Text.RegularExpressions.Regex.Replace(str, @"(\\)([\000\010\011\012\015\032\042\047\134\140])", "$2");
            }
            catch (Exception Ex)
            {
                // handle any exception here
                Console.WriteLine(Ex.Message);
            }

            return Result;
        }

        public static string TrimStart(this string target, string trimString)
        {
            string result = target;
            while (result.StartsWith(trimString))
            {
                result = result.Substring(trimString.Length);
            }

            return result;
        }

        public static string TrimEnd(this string target, string trimString)
        {
            string result = target;
            while (result.EndsWith(trimString))
            {
                result = result.Substring(0, result.Length - trimString.Length);
            }

            return result;
        }

        #endregion String

        #region IEnumerable

        public static IEnumerable<T> Append<T>(this IEnumerable<T> source, params T[] item)
        {
            return source.Concat(item);
        }

        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> property)
        {
            return items.GroupBy(property).Select(x => x.First());
        }

        #endregion IEnumerable

        public static string GetFullMessage(this Exception ex)
        {
            return ex.InnerException == null
                 ? ex.Message
                 : ex.Message + " --> " + ex.InnerException.GetFullMessage();
        }
    }
}