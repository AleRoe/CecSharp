using System.Text;
using AleRoe.CecSharp.Model;

namespace AleRoe.CecSharp.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="CecMessage"/>
    /// </summary>
    public static class CecMessageExtensions
    {
        /// <summary>
        /// Converts the given <see cref="CecMessage"/> to a HDMI-CEC frame.
        /// </summary>
        /// <param name="value">The <see cref="CecMessage"/> value.</param>
        /// <returns>A <see cref="System.String"/> value representing the HDMI-CEC frame.</returns>
        public static string ToCec(this CecMessage value)
        {
            var builder = new StringBuilder();
            builder.Append(value.Source.ToString("X").Substring(1, 1));
            builder.Append(value.Destination.ToString("X").Substring(1, 1));

            if (value.Command != Command.None)
                builder.AppendFormat($":{value.Command:X}");

            if (value.Parameters != null)
                builder.AppendFormat($":{value.Parameters.ToHex()}");

            return builder.ToString();
        }

        /// <summary>
        /// Converts to verbose.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public static string ToVerbose(this CecMessage value)
        {
            var command = value.Command.ToString();
            return value.Command switch
            {
                Command.None => "Polling",
                Command.SetOSDName => $"{command} - OSD Name: {value.Parameters.ToASCIIString()}",
                _ => command,
            };
        }
    }
}