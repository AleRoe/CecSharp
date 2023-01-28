namespace AleRoe.CecSharp.Extensions
{
    internal static class IntExtensions
    {
        public static bool InRange(this int value, int min, int max)
        {
            return value >= min && value <= max;
        }
    }
}