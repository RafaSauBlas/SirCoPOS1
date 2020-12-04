using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SirCoPOS.BusinessLogic.Helpers
{
    public class Common
    {
        private const string TIMEZONE = "Central Standard Time (Mexico)";
        public static DateTime GetNow()
        {
            var now = DateTime.UtcNow;
            var tz = TimeZoneInfo.FindSystemTimeZoneById(TIMEZONE);
            var date = TimeZoneInfo.ConvertTimeFromUtc(now, tz);
            return date;
        }
        public static DateTime GetToday()
        {
            var now = GetNow();
            var today = new DateTime(now.Year, now.Month, now.Day);
            return today;
        }
    }
}
