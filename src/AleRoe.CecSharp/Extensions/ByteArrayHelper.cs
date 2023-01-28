using System;
using System.Linq;
using System.Text;
using MoreLinq;

namespace AleRoe.CecSharp.Extensions
{
    internal static class ByteArrayHelper
    {
        public static byte[] ToByteArray(int value, string format = "X2")
        {
            var hexValue = value.ToString(format);
            var result = hexValue.Batch(2)
                .Select(x => string.Join("", x))
                .Select(x => Convert.ToByte(x, 16)).ToArray();
            return result;
        }

        public static byte[] ToByteArray(string value)
        {
            return Encoding.ASCII.GetBytes(value);
        }

        public static byte[] ToByteArray(bool value)
        {
            return BitConverter.GetBytes(value);
        }

        public static byte[] ToByteArray((byte, byte) value)
        {
            var result = new[] {value.Item1, value.Item2};
            return result;
        }

        public static byte[] ToByteArray(Enum value)
        {
            var result = new[] {Convert.ToByte(value)};
            return result;
        }

        public static byte[] Parse(string delimitedValue, char delimiter = ':')
        {
            var bytes = delimitedValue.Split(delimiter);
            return bytes.Select(x => Convert.ToByte(x, 16)).ToArray();
        }
    }
}