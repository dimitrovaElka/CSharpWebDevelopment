namespace SIS.HTTP.Extensions
{
    using System;

    public static class StringExtensions
    {
        public static string Capitalize(this String str)
        {
            if (string.IsNullOrEmpty(str))
            {
                throw new ArgumentException($"{nameof(str)} cannot be null");
            }
            string result = str.Substring(0, 1).ToUpper() + str.Substring(1).ToLower();

            return result;
        }
    }
}
