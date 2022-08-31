using System;

namespace LMS.Infrastructure.Utils
{
    public class DatetimeUtils
    {
        public static DateTime GetVietnamTime()
        {
            // default is UTC
            try
            {
                var universalTime = DateTime.UtcNow;

                var vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("North Asia Standard Time");
                return TimeZoneInfo.ConvertTimeFromUtc(universalTime, vietnamTimeZone);
            }
            catch (System.Exception)
            {
                return DateTime.Now;
            }
        }
        public static DateTime UnixTimeStampToDateTime(long unixTimeStamp)
        {
            var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToLocalTime();
            return dateTimeVal;
        }
    }
}