using System.Globalization;

namespace Data.Services
{
    public static class DateConverterForData
    {
        public static readonly CultureInfo cultures = new("en-IN");

        public static DateTime GetSystemDateTime()
        {
            return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById("India Standard Time"));
        }

        public static DateTime? ToDateTime(string? datetime)
        {
            if (String.IsNullOrEmpty(datetime))
            {
                return null;
            }

            return Convert.ToDateTime(datetime, cultures);
        }

        public static DateTime ToDateTimeWithoutNull(string datetime)
        {
            return Convert.ToDateTime(datetime, cultures);
        }

        public static string ToStringDateTime(DateTime? datetime)
        {
            if (datetime == null)
            {
                return "";
            }

            return Convert.ToDateTime(datetime, cultures).Date.ToString("dd-MM-yyyy");
        }
    }
}