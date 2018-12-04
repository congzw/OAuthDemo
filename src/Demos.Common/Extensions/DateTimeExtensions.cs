using System;

// ReSharper disable once CheckNamespace
namespace Demos.Common
{
    public static class DateTimeExtensions
    {
        /// <summary>
        /// 获取精确到小时的时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetHourDate(this DateTime dateTime)
        {
            var hourDate = dateTime.Date.AddHours(dateTime.Hour);
            return hourDate;
        }

        /// <summary>
        /// 获取精确到天的时间
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetDayDate(this DateTime dateTime)
        {
            var hourDate = dateTime.Date;
            return hourDate;
        }

        /// <summary>
        /// 默认的时间，2000，1，1
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetDefaultDate(this DateTime dateTime)
        {
            var result = new DateTime(2000, 1, 1);
            return result;
        }

        /// <summary>
        /// 默认的未来时间，2100，1，1
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime GetDefaultFurtureDate(this DateTime dateTime)
        {
            var result = new DateTime(2100, 1, 1);
            return result;
        }

        /// <summary>
        /// 获取所在周的第一天
        /// 默认一周的第一天是周一
        /// </summary>
        /// <param name="currentDateTime"></param>
        /// <param name="firstDayOfWeek"></param>
        /// <param name="clearTimePart">是否清除时间部分</param>
        /// <returns></returns>
        public static DateTime StartOfWeek(this DateTime currentDateTime, DayOfWeek firstDayOfWeek = DayOfWeek.Monday, bool clearTimePart = false)
        {
            var startDateTime = currentDateTime;
            if (clearTimePart)
            {
                startDateTime = DateTime.Parse(startDateTime.ToShortDateString());
            }
            while (startDateTime.DayOfWeek != firstDayOfWeek)
            {
                startDateTime = startDateTime.AddDays(-1);
            }
            return startDateTime;
        }

        /// <summary>
        /// 获取所在周的最后天
        /// 默认一周的第一天是周一
        /// </summary>
        /// <param name="currentDateTime"></param>
        /// <param name="firstDayOfWeek"></param>
        /// <param name="clearTimePart">是否清除时间部分</param>
        /// <returns></returns>
        public static DateTime EndOfWeek(this DateTime currentDateTime, DayOfWeek firstDayOfWeek = DayOfWeek.Monday, bool clearTimePart = false)
        {
            return currentDateTime.StartOfWeek(firstDayOfWeek, clearTimePart).AddDays(6);
        }

        /// <summary>
        /// 格式化时间
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToFormat(this DateTime datetime, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return datetime.ToString(format);
        }

        /// <summary>
        /// 格式化日期
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToDateFormat(this DateTime datetime, string format = "yyyy-MM-dd")
        {
            return datetime.ToString(format);
        }
    }
}
