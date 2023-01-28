using System;
using System.Linq;
using System.Text;
using MoreLinq;

namespace AleRoe.CecSharp.Extensions
{
    internal static class HexExtensions
    {
        public static string ToHex(this string value, char delimiter = ':')
        {
            var bytes = Encoding.ASCII.GetBytes(value);
            var hexString = BitConverter.ToString(bytes);
            return hexString.Replace("-", delimiter.ToString());
        }

        public static string ToHex(this int value, char delimiter = ':', string format = "X2")
        {
            var hexValue = value.ToString(format);
            return hexValue.Batch(2).Select(x => string.Join("", x)).ToDelimitedString(delimiter.ToString());
        }

        public static string ToHex(this byte[] value)
        {
            var hexString = BitConverter.ToString(value);
            return hexString.Replace("-", ":");
        }

        public static string ToASCIIString(this byte[] value)
        {
            if (value != null) return Encoding.ASCII.GetString(value);

            return string.Empty;
        }
    }
}