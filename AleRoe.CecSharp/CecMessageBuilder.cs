using System;
using System.Linq;
using AleRoe.CecSharp.Extensions;
using AleRoe.CecSharp.Model;

namespace AleRoe.CecSharp
{
    /// <summary>
    /// Helper methods to facilitate creation of CecMessages
    /// </summary>
    public static class CecMessageBuilder
    {
        /// <inheritdoc cref="Command.GivePhysicalAddress"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="destination">The <c>CecMessage</c> destination.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        public static CecMessage GivePhysicalAddress(LogicalAddress source, LogicalAddress destination)
        {
            return new CecMessage(source, destination, Command.GivePhysicalAddress);
        }

        /// <inheritdoc cref="Command.ReportPhysicalAddress"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="deviceType">Type of the device.</param>
        /// <param name="physicalAddress">The physical address.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        public static CecMessage ReportPhysicalAddress(LogicalAddress source, DeviceType deviceType, PhysicalAddress physicalAddress)
        {
            var args = ByteArrayHelper.ToByteArray(physicalAddress.Address)
                .Concat(ByteArrayHelper.ToByteArray(deviceType)).ToArray();
            return new CecMessage(source, LogicalAddress.Unregistered, Command.ReportPhysicalAddress, args);
        }

        /// <inheritdoc cref="Command.ReportAudioStatus"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="destination">The <c>CecMessage</c> destination.</param>
        /// <param name="status">The status.</param>
        /// <param name="value">The value.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        /// <exception cref="System.ArgumentException">Value must be between 0 and 127 (inclusive). - value</exception>
        public static CecMessage ReportAudioStatus(LogicalAddress source, LogicalAddress destination, AudioMuteStatus status, int value)
        {
            if (!value.InRange(0, 127))
                throw new ArgumentException("Value must be between 0 and 127 (inclusive).", nameof(value));

            var args = Convert.ToByte(status) | Convert.ToByte(value);
            return new CecMessage(source, destination, Command.ReportAudioStatus, ByteArrayHelper.ToByteArray(args, "X2"));
        }

        /// <inheritdoc cref="Command.SetSystemAudioMode"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="destination">The <c>CecMessage</c> destination.</param>
        /// <param name="status">The system audio status.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        public static CecMessage SetSystemAudioMode(LogicalAddress source, LogicalAddress destination, SystemAudioStatus status)
        {
            return new CecMessage(source, destination, Command.SetSystemAudioMode,
                ByteArrayHelper.ToByteArray(Convert.ToByte(status), "X2"));
        }

        /// <inheritdoc cref="Command.DeviceVendorId"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        public static CecMessage DeviceVendorId(LogicalAddress source, int vendorId)
        {
            return new CecMessage(source, LogicalAddress.Unregistered, Command.DeviceVendorId,
                ByteArrayHelper.ToByteArray(vendorId, "X6"));
        }

        /// <inheritdoc cref="Command.SetSystemAudioMode"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="destination">The <c>CecMessage</c> destination.</param>
        /// <param name="osdName">Name OSD name of the device.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        public static CecMessage SetOsdName(LogicalAddress source, LogicalAddress destination, string osdName)
        {
            return new CecMessage(source, destination, Command.SetOSDName, ByteArrayHelper.ToByteArray(osdName));
        }

        /// <inheritdoc cref="Command.MenuStatus"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="destination">The <c>CecMessage</c> destination.</param>
        /// <param name="state">The state.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        public static CecMessage MenuStatus(LogicalAddress source, LogicalAddress destination, MenuStatus state)
        {
            return new CecMessage(source, destination, Command.MenuStatus, ByteArrayHelper.ToByteArray(state));
        }

        /// <inheritdoc cref="Command.InactiveSource"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="physicalAddress">The physical address.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        public static CecMessage InactiveSource(LogicalAddress source, PhysicalAddress physicalAddress)
        {
            return new CecMessage(source, LogicalAddress.TV, Command.InactiveSource,
                ByteArrayHelper.ToByteArray(physicalAddress.Address));
        }

        /// <inheritdoc cref="Command.ActiveSource"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="physicalAddress">The physical address.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        public static CecMessage ActiveSource(LogicalAddress source, PhysicalAddress physicalAddress)
        {
            return new CecMessage(source, LogicalAddress.Unregistered, Command.ActiveSource,
                ByteArrayHelper.ToByteArray(physicalAddress.Address));
        }

        /// <inheritdoc cref="Command.FeatureAbort"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="destination">The <c>CecMessage</c> destination.</param>
        /// <param name="opCode">The <c>Command</c> which is aborted.</param>
        /// <param name="reason">A <c>AbortReason</c> value.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        public static CecMessage FeatureAbort(LogicalAddress source, LogicalAddress destination, Command opCode, AbortReason reason)
        {
            var args = ByteArrayHelper.ToByteArray(opCode)
                .Concat(ByteArrayHelper.ToByteArray(reason)).ToArray();
            return new CecMessage(source, destination, Command.FeatureAbort, args);
        }

        /// <inheritdoc cref="Command.CecVersion"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="destination">The <c>CecMessage</c> destination.</param>
        /// <param name="version">The <c>CecVersion</c> value.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        public static CecMessage CecVersion(LogicalAddress source, LogicalAddress destination, CecVersion version)
        {
            return new CecMessage(source, destination, Command.CecVersion,
                ByteArrayHelper.ToByteArray(version));
        }

        /// <inheritdoc cref="Command.CecVersion"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="destination">The <c>CecMessage</c> destination.</param>
        /// <param name="status">The <c>PowerStatus</c> value.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        public static CecMessage ReportPowerStatus(LogicalAddress source, LogicalAddress destination, PowerStatus status)
        {
            return new CecMessage(source, destination, Command.ReportPowerStatus,
                ByteArrayHelper.ToByteArray(status));
        }

        /// <summary>
        /// Used to perform polling.
        /// </summary>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        public static CecMessage Polling(LogicalAddress source)
        {
            return new CecMessage(source, source);
        }
    }
}