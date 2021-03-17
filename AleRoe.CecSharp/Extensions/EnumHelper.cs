using System;

namespace AleRoe.CecSharp.Extensions
{
    internal static class EnumHelper
    {
        public static TResult ParseExact<TResult>(string value) where TResult : struct
        {
            if (!typeof(TResult).IsEnum) throw new ArgumentException("Given Type is not an Enum.", nameof(TResult));

            if (!Enum.TryParse(value, out TResult result))
                throw new ArgumentException("Value cannot be parsed to Enum.");

            return result;
        }

        public static TResult ParseExact<TResult>(string value, TResult defaultValue) where TResult : struct
        {
            if (!typeof(TResult).IsEnum) throw new ArgumentException("Given Type is not an Enum.", nameof(TResult));

            if (!Enum.TryParse(value, out TResult result))
                result = defaultValue;

            return result;
        }

        public static TResult ParseExact<TResult>(object value) where TResult : struct
        {
            return ParseExact<TResult>(value.ToString());
        }

        public static TResult ParseExact<TResult>(object value, TResult defaultValue) where TResult : struct
        {
            return ParseExact(value.ToString(), defaultValue);
        }
    }
}