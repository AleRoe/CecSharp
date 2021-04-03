using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
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

        /// <inheritdoc cref="Command.SetOSDName"/>
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
        /// <param name="state">The state.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        public static CecMessage MenuStatus(LogicalAddress source, MenuState state)
        {
            return new CecMessage(source, LogicalAddress.TV, Command.MenuStatus, ByteArrayHelper.ToByteArray(state));
        }

        /// <inheritdoc cref="Command.MenuRequest"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="destination">The <c>CecMessage</c> destination.</param>
        /// <param name="type">The menu request type.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        public static CecMessage MenuRequest(LogicalAddress destination, MenuRequestType  type)
        {
            return new CecMessage(LogicalAddress.TV, destination, Command.MenuRequest, ByteArrayHelper.ToByteArray(type));
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

        /// <inheritdoc cref="Command.ReportPowerStatus"/>
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

        /// <inheritdoc cref="Command.SetMenuLanguage"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="language">The ISO-639-2 language code.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="ArgumentNullException"></exception>
        public static CecMessage SetMenuLanguage(LogicalAddress source, [NotNull] string language)
        {
            if (string.IsNullOrEmpty(language))
                throw new ArgumentNullException(nameof(language));

            if (!IsValidLanguageCode(language))
                throw new ArgumentException("The language code is invalid", nameof(language));
            
            return new CecMessage(source, LogicalAddress.Unregistered, Command.SetMenuLanguage, ByteArrayHelper.ToByteArray(language));
        }

        /// <inheritdoc cref="Command.GetMenuLanguage"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="destination">The <c>CecMessage</c> destination.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        public static CecMessage GetMenuLanguage(LogicalAddress source, LogicalAddress destination)
        {
            return new CecMessage(source, destination, Command.GetMenuLanguage);
        }

        /// <inheritdoc cref="Command.SetOSDString"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="destination">The <c>CecMessage</c> destination.</param>
        /// <param name="displayControl">The display control value.</param>
        /// <param name="osdString">The osd string to display (max. 13 characters).</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public static CecMessage SetOSDString(LogicalAddress source, DisplayControl displayControl, [NotNull] string osdString)
        {
            if (string.IsNullOrEmpty(osdString))
                throw new ArgumentNullException(nameof(osdString));

            if (osdString.Length > 13)
                throw new ArgumentException("The string length may not exceed 13 characters.", nameof(osdString));
            
            var parameters = ByteArrayHelper.ToByteArray(displayControl)
                .Concat(ByteArrayHelper.ToByteArray(osdString)).ToArray();

            return new CecMessage(source, LogicalAddress.TV, Command.SetOSDString, parameters);
        }

        /// <inheritdoc cref="Command.GiveOsdName"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="destination">The <c>CecMessage</c> destination.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        public static CecMessage GiveOsdName(LogicalAddress source, LogicalAddress destination)
        {
            return new CecMessage(source, destination, Command.GiveOsdName);
        }

        /// <inheritdoc cref="Command.RequestActiveSource"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        public static CecMessage RequestActiveSource(LogicalAddress source)
        {
            return new CecMessage(source, LogicalAddress.Unregistered, Command.RequestActiveSource);
        }

        /// <inheritdoc cref="Command.SetStreamPath"/>
        /// <param name="physicalAddress">The physical address.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        public static CecMessage SetStreamPath(PhysicalAddress physicalAddress)
        {
            return new CecMessage(LogicalAddress.TV, LogicalAddress.Unregistered, Command.SetStreamPath,
                ByteArrayHelper.ToByteArray(physicalAddress.Address));
        }

        /// <inheritdoc cref="Command.RoutingChange"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="originalAddress">The original physical address.</param>
        /// <param name="newAddress">The new physical address.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        public static CecMessage RoutingChange(LogicalAddress source, PhysicalAddress originalAddress, PhysicalAddress newAddress)
        {
            var parameters = ByteArrayHelper.ToByteArray(originalAddress.Address)
                .Concat(ByteArrayHelper.ToByteArray(newAddress.Address)).ToArray();
            return new CecMessage(source, LogicalAddress.Unregistered, Command.RoutingChange, parameters);
        }

        /// <inheritdoc cref="Command.RoutingInformation"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="physicalAddress">The physical address.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        public static CecMessage RoutingInformation(LogicalAddress source, PhysicalAddress physicalAddress)
        {
            return new CecMessage(source, LogicalAddress.Unregistered, Command.RoutingInformation, ByteArrayHelper.ToByteArray(physicalAddress.Address));
        }

        /// <inheritdoc cref="Command.GiveDevicePowerStatus"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="destination">The <c>CecMessage</c> destination.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        public static CecMessage GiveDevicePowerStatus(LogicalAddress source, LogicalAddress destination)
        {
            return new CecMessage(source, destination, Command.GiveDevicePowerStatus);
        }

        /// <inheritdoc cref="Command.GiveDeviceVendorId"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="destination">The <c>CecMessage</c> destination.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        public static CecMessage GiveDeviceVendorId(LogicalAddress source, LogicalAddress destination)
        {
            return new CecMessage(source, destination, Command.GiveDeviceVendorId);
        }

        /// <inheritdoc cref="Command.VendorCommand"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="destination">The <c>CecMessage</c> destination.</param>
        /// <param name="data">The data.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static CecMessage VendorCommand(LogicalAddress source, LogicalAddress destination, byte[] data)
        {
            if (data.Length > 14 || data.Length == 0)
                throw new ArgumentException("Data must be between 1 and 14 bytes", nameof(data));
            
            return new CecMessage(source, destination, Command.VendorCommand, data);
        }

        /// <inheritdoc cref="Command.VendorCommand"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="destination">The <c>CecMessage</c> destination.</param>
        /// <param name="data">The data.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static CecMessage VendorCommand(LogicalAddress source, LogicalAddress destination, string data)
            => CecMessageBuilder.VendorCommand(source, destination, ByteArrayHelper.ToByteArray(data));

        /// <inheritdoc cref="Command.VendorCommandWithId"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="destination">The <c>CecMessage</c> destination.</param>
        /// <param name="vendorId">The Vendor Id.</param>
        /// <param name="data">The data.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static CecMessage VendorCommandWithId(LogicalAddress source, LogicalAddress destination, int vendorId, byte[] data)
        {
            if (data.Length > 14 || data.Length == 0)
                throw new ArgumentException("Data must be between 1 and 14 bytes", nameof(data));

            var parameters = ByteArrayHelper.ToByteArray(vendorId, "X6")
                .Concat(data).ToArray();

            return new CecMessage(source, destination, Command.VendorCommandWithId, parameters);
        }

        /// <inheritdoc cref="Command.VendorCommandWithId"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="destination">The <c>CecMessage</c> destination.</param>
        /// <param name="vendorId">The Vendor Id.</param>
        /// <param name="data">The data.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static CecMessage VendorCommandWithId(LogicalAddress source, LogicalAddress destination, int vendorId, string data)
            => CecMessageBuilder.VendorCommandWithId(source, destination, vendorId, ByteArrayHelper.ToByteArray(data));

        /// <inheritdoc cref="Command.VendorRemoteButtonDown"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="destination">The <c>CecMessage</c> destination.</param>
        /// <param name="data">The data.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static CecMessage VendorRemoteButtonDown(LogicalAddress source, LogicalAddress destination, byte[] data)
        {
            if (data.Length > 14 || data.Length == 0)
                throw new ArgumentException("Data must be between 1 and 14 bytes", nameof(data));

            return new CecMessage(source, destination, Command.VendorRemoteButtonDown, data);
        }

        /// <inheritdoc cref="Command.VendorRemoteButtonDown"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="destination">The <c>CecMessage</c> destination.</param>
        /// <param name="data">The data.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        /// <exception cref="ArgumentException"></exception>
        public static CecMessage VendorRemoteButtonDown(LogicalAddress source, LogicalAddress destination, string data)
            => CecMessageBuilder.VendorRemoteButtonDown(source, destination, ByteArrayHelper.ToByteArray(data));

        /// <inheritdoc cref="Command.VendorRemoteButtonUp"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="destination">The <c>CecMessage</c> destination.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        public static CecMessage VendorRemoteButtonUp(LogicalAddress source, LogicalAddress destination)
        {
            return new CecMessage(source, destination, Command.VendorRemoteButtonUp);
        }

        /// <inheritdoc cref="Command.StandBy"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="destination">The <c>CecMessage</c> destination.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        public static CecMessage Standby(LogicalAddress source, LogicalAddress destination)
        {
            return new CecMessage(source, destination, Command.StandBy);
        }

        /// <inheritdoc cref="Command.UserControlPressed"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="destination">The <c>CecMessage</c> destination.</param>
        /// <param name="command">The <see cref="UiCommand"/> command.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        public static CecMessage UserControlPressed(LogicalAddress source, LogicalAddress destination, UiCommand command)
        {
            return new CecMessage(source, destination, Command.UserControlPressed, ByteArrayHelper.ToByteArray(command));
        }

        /// <inheritdoc cref="Command.UserControlReleased"/>
        /// <param name="source">The <c>CecMessage</c> source.</param>
        /// <param name="destination">The <c>CecMessage</c> destination.</param>
        /// <returns>A <c>CecMessage</c> that represents the command.</returns>
        public static CecMessage UserControlReleased(LogicalAddress source, LogicalAddress destination)
        {
            return new CecMessage(source, destination, Command.UserControlReleased);
        }

        private static bool IsValidLanguageCode(string value)
        {
            return CultureInfo.GetCultures(CultureTypes.NeutralCultures)
                .Any(x => x.ThreeLetterISOLanguageName == value);
        }
    }
}