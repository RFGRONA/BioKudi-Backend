namespace Biokudi_Backend.Application.Utilities
{
    public static class DateUtility
    {
        public static DateTime DateNowColombia()
        {
            TimeZoneInfo timeZoneColombia = TimeZoneInfo.FindSystemTimeZoneById("SA Pacific Standard Time");
            return TimeZoneInfo.ConvertTime(DateTime.Now, timeZoneColombia);
        }
    }
}
