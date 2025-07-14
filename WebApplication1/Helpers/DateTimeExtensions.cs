namespace WebApplication1.Helpers
{
    public static class DateTimeExtensions
    {
        public static string ToRelativeTime(this DateTime dateTime)
        {
            var timeSpan = DateTime.UtcNow - dateTime;

            if (timeSpan.TotalSeconds < 60)
                return "только что";

            if (timeSpan.TotalMinutes < 60)
                return $"{(int)timeSpan.TotalMinutes} мин. назад";

            if (timeSpan.TotalHours < 24)
                return $"{(int)timeSpan.TotalHours} ч. назад";

            if (timeSpan.TotalDays < 30)
                return $"{(int)timeSpan.TotalDays} дн. назад";

            if (timeSpan.TotalDays < 365)
                return $"{(int)(timeSpan.TotalDays / 30)} мес. назад";

            return $"{(int)(timeSpan.TotalDays / 365)} г. назад";
        }
    }
}
