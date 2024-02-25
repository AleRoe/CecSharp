using System;
using System.Globalization;
using System.Linq;
using System.Text;
using AleRoe.CecSharp.Model;
using MoreLinq;

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
                Command.SetOSDName => $"{command} - Name: {value.Parameters.ToASCIIString()}",
                Command.ReportPhysicalAddress => $"{command} - Address: {value.Parameters.Take(2).ToArray().ToPhysicalAddressString()} Type: {Enum.GetName<DeviceType>((DeviceType)value.Parameters[2])}",
                Command.ReportPowerStatus => $"{command} - Status: {Enum.GetName<PowerStatus>((PowerStatus)value.Parameters[0])}",
                Command.DeviceVendorId => $"{command} - Id: {int.Parse(BitConverter.ToString(value.Parameters).Replace("-",""), NumberStyles.HexNumber)}",
                Command.CecVersion => $"{command} - Version: {Enum.GetName<CecVersion>((CecVersion)value.Parameters[0])}",
                _ => command,
            };
        }

        private static string ToPhysicalAddressString(this byte[] value)
        {
            return BitConverter.ToString(value).Replace("-","").ToCharArray().ToDelimitedString("."); 
        }
    }
}