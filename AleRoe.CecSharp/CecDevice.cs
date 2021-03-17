using System;
using System.Linq;
using System.Runtime.CompilerServices;
using AleRoe.CecSharp.Extensions;
using AleRoe.CecSharp.Model;

[assembly: InternalsVisibleTo("AleRoe.CecSharp.Tests")]

namespace AleRoe.CecSharp
{
    /// <summary>
    /// Provides a class representing a HDMI-CEC enabled device.
    /// </summary>
    public sealed class CecDevice : ICecDevice
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CecDevice"/> class.
        /// </summary>
        /// <param name="deviceType">Type of the device.</param>
        /// <param name="osdName">The OSD name of the device.</param>
        /// <param name="vendorId">The vendor identifier.</param>
        public CecDevice(DeviceType deviceType, string osdName, int vendorId)
            : this(deviceType, osdName, vendorId, PhysicalAddress.None, LogicalAddress.Unregistered) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="CecDevice"/> class.
        /// </summary>
        /// <param name="deviceType">Type of the device.</param>
        /// <param name="osdName">The OSD name of the device.</param>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <param name="physicalAddress">The physical address.</param>
        public CecDevice(DeviceType deviceType, string osdName, int vendorId, PhysicalAddress physicalAddress)
            : this(deviceType, osdName, vendorId, physicalAddress, LogicalAddress.Unregistered) {}

        /// <summary>
        /// Initializes a new instance of the <see cref="CecDevice"/> class.
        /// </summary>
        /// <param name="deviceType">Type of the device.</param>
        /// <param name="osdName">The OSD name of the device.</param>
        /// <param name="vendorId">The vendor identifier.</param>
        /// <param name="physicalAddress">The physical address.</param>
        /// <param name="logicalAddress">The logical address.</param>
        /// <exception cref="ArgumentException"></exception>
        public CecDevice(DeviceType deviceType, string osdName, int vendorId, PhysicalAddress physicalAddress, LogicalAddress logicalAddress)
        {
            if (string.IsNullOrEmpty(osdName) || osdName.Length > 14)
                throw new ArgumentException("OSD name may not be empty or exceed 14 characters", nameof(osdName));
            
            DeviceType = deviceType;
            OsdName = osdName;
            VendorId = vendorId;
            PhysicalAddress = physicalAddress;
            LogicalAddress = LogicalAddress;
            PowerStatus = PowerStatus.Standby;
        }

        /// <summary>
        /// Gets the OSD name of the device.
        /// </summary>
        public string OsdName { get; }

        /// <summary>
        /// Gets the vendor identifier.
        /// </summary>
        public int VendorId { get; }

        /// <summary>
        /// Gets the type of the device.
        /// </summary>
        public DeviceType DeviceType { get; }

        /// <summary>
        /// Gets or sets the logical device address.
        /// </summary>
        public LogicalAddress LogicalAddress { get; set; }

        /// <summary>
        /// Gets or sets the physical device address.
        /// </summary>
        public PhysicalAddress PhysicalAddress { get; set; }

        /// <summary>
        /// Gets a value indicating whether this device is the active source.
        /// </summary>
        public bool IsActiveSource { get; internal set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether the physical address should be locked.
        /// </summary>
        public bool LockPhysicalAddress { get; set; }

        /// <summary>
        /// Gets or sets the power status of the device.
        /// </summary>
        public PowerStatus PowerStatus { get; set; }
    

        /// <summary>
        /// Processes an incoming CecMessage according to CEC Specifications
        /// </summary>
        /// <param name="message">A <see cref="CecMessage"/> request message.</param>
        /// <returns>An appropriate <see cref="CecMessage"/> response message based on the request message or <see cref="CecMessage.None"/> for noop requests.</returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public CecMessage ProcessCecMessage(CecMessage message)
        {
            if (message.Destination == LogicalAddress) // directly addressed
            {
                switch (message.Command)
                {
                    //Report physical address
                    case Command.GivePhysicalAddress:
                        return this.ReportPhysicalAddress();

                    //Vendor Command
                    case Command.VendorCommand:
                        return this.FeatureAbort(message.Source, Command.VendorCommand, AbortReason.InvalidOperand);

                    //Report VendorID
                    case Command.GiveDeviceVendorId:
                        return this.DeviceVendorId();

                    //Report OSD Name
                    case Command.GiveOsdName:
                        return this.SetOsdName(message.Source);

                    //get menu language -> aborted
                    case Command.GetMenuLanguage:
                        return this.FeatureAbort(message.Source, Command.GetMenuLanguage, AbortReason.InvalidOperand);

                    //get CEC Version
                    case Command.GetCecVersion:
                        return this.CecVersion(message.Source);

                    //ToDo: design how we want to handle power status
                    //report power status
                    case Command.GiveDevicePowerStatus:
                        return this.ReportPowerStatus(message.Source, this.PowerStatus);

                    //report tuner status
                    case Command.GiveTunerDeviceStatus:
                        return this.ReportPowerStatus(message.Source, this.PowerStatus);

                    //messages from other devices that we can ignore
                    case Command.FeatureAbort:
                    case Command.ReportPowerStatus:
                    case Command.CecVersion:
                    case Command.None:
                        return CecMessage.None;

                    //messages which are passed thru
                    case Command.UserControlPressed:
                    case Command.UserControlReleased:
                    case Command.StandBy:
                    break;

                    default:
                        return this.FeatureAbort(message.Source, message.Command, AbortReason.UnrecognizedOpcode);
                }
            }
            
            if (message.Destination == LogicalAddress.Unregistered) // Broadcast Messages
            { 
                switch (message.Command)
                {
                    //set stream path
                    case Command.SetStreamPath:
                        if (IsPhysicalAddress(message.Parameters))
                            return this.ActiveSource();
                        else
                            IsActiveSource = false;
                        break;

                    //set active source
                    case Command.ActiveSource:
                        IsActiveSource = IsPhysicalAddress(message.Parameters);
                        break;

                    //routing change
                    case Command.RoutingChange:
                        IsActiveSource = IsPhysicalAddress(message.Parameters.TakeLast(2).ToArray());
                        break;

                    //get active source
                    case Command.RequestActiveSource:
                        if (IsActiveSource)
                            return this.ActiveSource();
                        break;

                    //broadcast messages from other devices that we can ignore
                    case Command.DeviceVendorId:
                    case Command.ReportPhysicalAddress:
                    case Command.ReportPowerStatus:
                    case Command.SetMenuLanguage:
                    case Command.StandBy:
                        break;
                    
                    default:
                        return this.FeatureAbort(message.Source, message.Command, AbortReason.UnrecognizedOpcode);
                }
            }
            return CecMessage.None;
        }

        private bool IsPhysicalAddress(byte[] bytes)
        {
            return bytes.SequenceEqual(ByteArrayHelper.ToByteArray(PhysicalAddress.Address));
        }
    }
}