using System;
using AleRoe.CecSharp.Model;

namespace AleRoe.CecSharp.Extensions
{
    public static class CecDeviceExtensions
    {
        /// <inheritdoc cref="Command.ReportAudioStatus"/>
        /// <param name="device">The device.</param>
        /// <param name="destination">The destination address.</param>
        /// <param name="status">The audio mute status.</param>
        /// <param name="value">The audio volume [0-127].</param>
        /// <returns>A <see cref="CecMessage"/> object representing the command.</returns>
        public static CecMessage ReportAudioStatus(this CecDevice device, LogicalAddress destination, AudioMuteStatus status, int value)
        {
            return CecMessageBuilder.ReportAudioStatus(device.LogicalAddress, destination, status, value);

        }

        /// <inheritdoc cref="Command.SetSystemAudioMode"/>
        /// <param name="device">The device.</param>
        /// <param name="destination">The destination address.</param>
        /// <param name="status">The system audio status.</param>
        /// <returns>A <see cref="CecMessage"/> object representing the command.</returns>
        public static CecMessage SetSystemAudioMode(this CecDevice device, LogicalAddress destination, SystemAudioStatus status)
        {
            return CecMessageBuilder.SetSystemAudioMode(device.LogicalAddress, destination, status);
        }

        /// <inheritdoc cref="Command.ReportPhysicalAddress"/>
        /// <param name="device">The device.</param>
        /// <returns>A <see cref="CecMessage"/> object representing the command.</returns>
        public static CecMessage ReportPhysicalAddress(this CecDevice device)
        {
            if (device.PhysicalAddress == PhysicalAddress.None)
                throw new InvalidOperationException("Physical address is not valid.");

            return CecMessageBuilder.ReportPhysicalAddress(device.LogicalAddress, device.DeviceType, device.PhysicalAddress);
        }

        /// <inheritdoc cref="Command.GivePhysicalAddress"/>
        /// <param name="device">The device.</param>
        /// <param name="destination">The destination address.</param>
        /// <returns>A <see cref="CecMessage"/> object representing the command.</returns>
        public static CecMessage GivePhysicalAddress(this CecDevice device, LogicalAddress destination)
        {
            return CecMessageBuilder.GivePhysicalAddress(device.LogicalAddress, destination);
        }

        /// <inheritdoc cref="Command.DeviceVendorId"/>
        /// <param name="device">The device.</param>
        /// <returns>A <see cref="CecMessage"/> object representing the command.</returns>
        public static CecMessage DeviceVendorId(this CecDevice device)
        {
            return CecMessageBuilder.DeviceVendorId(device.LogicalAddress, device.VendorId);
        }

        /// <inheritdoc cref="Command.SetOSDName"/>
        /// <param name="device">The device.</param>
        /// <param name="destination">The destination address.</param>
        /// <returns>A <see cref="CecMessage"/> object representing the command.</returns>
        public static CecMessage SetOsdName(this CecDevice device, LogicalAddress destination)
        {
            return CecMessageBuilder.SetOsdName(device.LogicalAddress, destination, device.OsdName);
        }

        /// <inheritdoc cref="Command.MenuStatus"/>
        /// <param name="device">The device.</param>
        /// <param name="destination">The destination address.</param>
        /// <param name="status">The menu status.</param>
        /// <returns>A <see cref="CecMessage"/> object representing the command.</returns>
        public static CecMessage MenuStatus(this CecDevice device, LogicalAddress destination, MenuStatus status)
        {
            return CecMessageBuilder.MenuStatus(device.LogicalAddress, destination, status);
        }

        /// <inheritdoc cref="Command.InactiveSource"/>
        /// <param name="device">The device.</param>
        /// <returns>A <see cref="CecMessage"/> object representing the command.</returns>
        /// <remarks>This command will set the <see cref="CecDevice.IsActiveSource"/> property to <c>false</c>.</remarks>
        public static CecMessage InactiveSource(this CecDevice device)
        {
            if (device.PhysicalAddress == PhysicalAddress.None)
                throw new InvalidOperationException("Physical address is not valid.");

            device.IsActiveSource = false;
            return CecMessageBuilder.InactiveSource(device.LogicalAddress, device.PhysicalAddress);
        }

        /// <inheritdoc cref="Command.ActiveSource"/>
        /// <param name="device">The device.</param>
        /// <returns>A <see cref="CecMessage"/> object representing the command.</returns>
        public static CecMessage ActiveSource(this CecDevice device)
        {
            if (device.PhysicalAddress == PhysicalAddress.None)
                throw new InvalidOperationException("Physical address is not valid.");

            device.IsActiveSource = true;
            return CecMessageBuilder.ActiveSource(device.LogicalAddress, device.PhysicalAddress);
        }

        /// <inheritdoc cref="Command.FeatureAbort"/>
        /// <param name="device">The device.</param>
        /// <param name="destination">The destination address.</param>
        /// <param name="opCode">The command being aborted.</param>
        /// <param name="reason">The abort reason.</param>
        /// <returns>A <see cref="CecMessage"/> object representing the command.</returns>
        public static CecMessage FeatureAbort(this CecDevice device, LogicalAddress destination, Command opCode, AbortReason reason)
        {
            return CecMessageBuilder.FeatureAbort(device.LogicalAddress, destination, opCode, reason);
        }

        /// <inheritdoc cref="Command.CecVersion"/>
        /// <param name="device">The device.</param>
        /// <param name="destination">The destination address.</param>
        /// <param name="version">The cec version.</param>
        /// <returns>A <see cref="CecMessage"/> object representing the command.</returns>
        public static CecMessage CecVersion(this CecDevice device, LogicalAddress destination, CecVersion version = Model.CecVersion.Version14)
        {
            return CecMessageBuilder.CecVersion(device.LogicalAddress, destination, version);
        }

        /// <inheritdoc cref="Command.ReportPowerStatus"/>
        /// <param name="device">The device.</param>
        /// <param name="destination">The destination address.</param>
        /// <param name="status">The power status.</param>
        /// <returns>A <see cref="CecMessage"/> object representing the command.</returns>
        public static CecMessage ReportPowerStatus(this CecDevice device, LogicalAddress destination, PowerStatus status)
        {
            return CecMessageBuilder.ReportPowerStatus(device.LogicalAddress, destination, status);
        }

        /// <inheritdoc cref="Command.ReportPowerStatus"/>
        /// <param name="device">The device.</param>
        /// <param name="address">The polling address.</param>
        /// <returns>A <see cref="CecMessage"/> object representing the command.</returns>
        public static CecMessage Polling(this CecDevice device, LogicalAddress address)
        {
            return CecMessageBuilder.Polling(address);
        }
    }
}