using System;
using AleRoe.CecSharp.Model;

namespace AleRoe.CecSharp.Extensions
{
    public static class HdmiCecDeviceExtensions
    {
        /// <inheritdoc cref="Command.ReportAudioStatus"/>
        /// <param name="destination">The destination address.</param>
        /// <param name="status">The audio mute status.</param>
        /// <param name="value">The audio volume [0-127].</param>
        /// <exception cref="ArgumentException"></exception>
        /// <returns></returns>
        public static CecMessage ReportAudioStatus(this CecDevice @this, LogicalAddress destination, AudioMuteStatus status, int value)
        {
            return CecMessageBuilder.ReportAudioStatus(@this.LogicalAddress, destination, status, value);

        }

        /// <inheritdoc cref="Command.SetSystemAudioMode"/>
        /// <param name="destination">The destination address.</param>
        /// <param name="status">The system audio status.</param>
        /// <returns></returns>
        public static CecMessage SetSystemAudioMode(this CecDevice @this, LogicalAddress destination, SystemAudioStatus status)
        {
            return CecMessageBuilder.SetSystemAudioMode(@this.LogicalAddress, destination, status);
        }

        /// <summary>
        /// Returns a <see cref="CecMessage"/> broadcast used to report the physical address of this device.
        /// </summary>
        public static CecMessage ReportPhysicalAddress(this CecDevice @this)
        {
            if (@this.PhysicalAddress == PhysicalAddress.None)
                throw new InvalidOperationException("Physical address is not valid.");

            return CecMessageBuilder.ReportPhysicalAddress(@this.LogicalAddress, @this.DeviceType,
                @this.PhysicalAddress);
        }

        /// <summary>
        /// Returns a <see cref="CecMessage"/> used to the physical address of a device (directly addressed).
        /// </summary>
        public static CecMessage GivePhysicalAddress(this CecDevice @this, LogicalAddress destination)
        {
            return CecMessageBuilder.GivePhysicalAddress(@this.LogicalAddress, destination);
        }

        /// <summary>
        /// Returns a <see cref="CecMessage"/> broadcast used to report the vendor id of this device.
        /// </summary>
        public static CecMessage DeviceVendorId(this CecDevice @this)
        {
            return CecMessageBuilder.DeviceVendorId(@this.LogicalAddress, @this.VendorId);
        }

        /// <inheritdoc cref="Command.SetOSDName"/>
        /// <param name="destination">The destination address.</param>
        /// <returns></returns>
        public static CecMessage SetOsdName(this CecDevice @this, LogicalAddress destination)
        {
            return CecMessageBuilder.SetOsdName(@this.LogicalAddress, destination, @this.OsdName);
        }

        
        public static CecMessage MenuStatus(this CecDevice @this, LogicalAddress destination, MenuStatus status)
        {
            return CecMessageBuilder.MenuStatus(@this.LogicalAddress, destination, status);
        }

        public static CecMessage InactiveSource(this CecDevice @this)
        {
            if (@this.PhysicalAddress == PhysicalAddress.None)
                throw new InvalidOperationException("Physical address is not valid.");

            @this.IsActiveSource = false;
            return CecMessageBuilder.InactiveSource(@this.LogicalAddress, @this.PhysicalAddress);
        }

        public static CecMessage ActiveSource(this CecDevice @this)
        {
            if (@this.PhysicalAddress == PhysicalAddress.None)
                throw new InvalidOperationException("Physical address is not valid.");

            @this.IsActiveSource = true;
            return CecMessageBuilder.ActiveSource(@this.LogicalAddress, @this.PhysicalAddress);
        }

        /// <summary>
        /// Returns a <see cref="CecMessage"/> broadcast used to report feature abort.
        /// </summary>
        public static CecMessage FeatureAbort(this CecDevice @this, LogicalAddress destination, Command opcode, AbortReason reason)
        {
            return CecMessageBuilder.FeatureAbort(@this.LogicalAddress, destination, opcode, reason);
        }

        /// <summary>
        /// Returns a <see cref="CecMessage"/> broadcast used to report the CEC version of this dveice.
        /// </summary>
        public static CecMessage CecVersion(this CecDevice @this, LogicalAddress destination)
        {
            return CecMessageBuilder.CecVersion(@this.LogicalAddress, destination, Model.CecVersion.Version14);
        }

        public static CecMessage ReportPowerStatus(this CecDevice @this, LogicalAddress destination, PowerStatus status)
        {
            return CecMessageBuilder.ReportPowerStatus(@this.LogicalAddress, destination, status);
        }

        public static CecMessage Polling(this CecDevice @this, LogicalAddress address)
        {
            return CecMessageBuilder.Polling(address);
        }
    }
}