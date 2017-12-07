using System;

namespace ProjectManagement.Utils.Extensions
{
    /// <summary>
    ///     Extends Enumerations and Objects to be transformed into an Enumeration
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        ///     Try to parse a value into an Enum and provides a Default in case of mismatch
        /// </summary>
        /// <typeparam name="TEnum">The Type of Enumeration to be returned</typeparam>
        /// <param name="value">The Value to be parsed into an Enumeration</param>
        /// <param name="defaultValue">The default value to be provided in case of missing parse</param>
        /// <returns>Return an Enumeration Value</returns>
        public static TEnum ToEnum<TEnum>(this string value, TEnum defaultValue) where TEnum : struct
        {
            if (string.IsNullOrEmpty(value))
            {
                return defaultValue;
            }

            return Enum.TryParse<TEnum>(value, true, out var result) ? result : defaultValue;
        }
    }
}