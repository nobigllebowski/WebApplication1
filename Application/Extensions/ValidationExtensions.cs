namespace Application.Extensions
{
    public static class ValidationExtensions
    {
        public static void ThrowIfNull<T>(this T obj, string paramName)
        {
            ArgumentNullException.ThrowIfNull(obj, paramName);
        }
    }
}
