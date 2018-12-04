using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ReSharper disable once CheckNamespace
namespace Demos.Common
{
    public static class StringExtensions
    {
        public static bool NbEquals(this string value, string value2, StringComparison stringComparison = StringComparison.OrdinalIgnoreCase, bool trimSpaceBeforeCompare = true)
        {
            if (value == null && value2 == null)
            {
                return true;
            }

            if (value == null)
            {
                return false;
            }

            if (value2 == null)
            {
                return false;
            }

            if (trimSpaceBeforeCompare)
            {
                return value.Trim().Equals(value2.Trim(), stringComparison);
            }

            return value.Equals(value2, stringComparison);
        }

        public static bool NbContains(this string source, string toCheck, StringComparison comp = StringComparison.OrdinalIgnoreCase)
        {
            if (source == null)
            {
                return false;
            }
            return source.IndexOf(toCheck, comp) >= 0;
        }

        public static bool NbContains(this IEnumerable<string> source, string toCheck, StringComparison comp = StringComparison.OrdinalIgnoreCase)
        {
            if (source == null)
            {
                return false;
            }
            return source.Any(s => s.NbEquals(toCheck));
        }
        /// <summary>
        /// [A,B] => "A,B"
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public static string ToSingleString(this IEnumerable<string> values)
        {
            var arrs = values as string[] ?? values.ToArray();
            if (values == null || !arrs.Any())
            {
                return string.Empty;
            }
            return string.Join(",", arrs);
        }
        
        /// <summary>
        /// 截取等宽中英文字符串
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="length">要截取的中文字符长度</param>
        /// <param name="appendStr">截取后后追加的字符串</param>
        /// <returns>截取后的字符串</returns>
        public static string CutStr(this string str, int length, string appendStr = "")
        {
            if (str == null) return string.Empty;

            var len = length * 2;
            int aequilateLength = 0, cutLength = 0;
            var encoding = Encoding.GetEncoding("gb2312");

            var cutStr = str;
            var strLength = cutStr.Length;
            byte[] bytes;
            for (int i = 0; i < strLength; i++)
            {
                bytes = encoding.GetBytes(cutStr.Substring(i, 1));
                if (bytes.Length >= 2)//不是英文
                    aequilateLength += 2;
                else
                    aequilateLength++;

                if (aequilateLength <= len) cutLength += 1;

                if (aequilateLength > len)
                    return cutStr.Substring(0, cutLength) + appendStr;
            }
            return cutStr;
        }
        public static string CutStrDefault(this string str)
        {
            //todo read from config
            int length = 20;
            string appendStr = "...";
            return CutStr(str, length, appendStr);
        }
        public static string CutStrShort(this string str)
        {
            //todo read from config
            int length = 10;
            string appendStr = "...";
            return CutStr(str, length, appendStr);
        }
        public static string CutStrLong(this string str)
        {
            //todo read from config
            int length = 100;
            string appendStr = "...";
            return CutStr(str, length, appendStr);
        }
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}
