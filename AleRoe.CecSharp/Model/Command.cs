namespace AleRoe.CecSharp.Model
{
    /// <summary>
    /// Available CEC commands.
    /// </summary>
    public enum Command : byte
    {
        /// <summary>
        /// Used by a new source to indicate that it has started to transmit a stream OR used in response to a "Request Active Source" (Broadcast). This message is used in several features : One Touch Play,Routing Control
        /// </summary>
        [Features(Features.RoutingControl)]
        ActiveSource = 0x82,

        /// <summary>
        /// Used to indicate the supported CEC version, in response to a "Get CEC Version" (Directly addressed)
        /// </summary>
        [Features(Features.SystemInformation)]
        CecVersion = 0x9E,

        /// <summary>
        /// Used to clear an Analogue timer block of a device (Directly addressed)
        /// </summary>
        ClearAnalogueTimer = 0x33,

        /// <summary>
        /// Used to clear a Digital timer block of a device (Directly addressed)
        /// </summary>
        ClearDigitalTimer = 0x99,

        /// <summary>
        /// Used to clear an External timer block of a device (Directly addressed)
        /// </summary>
        ClearExternalTimer = 0xA1,

        /// <summary>
        /// Used to control a device’s media functions (Directly addressed)
        /// </summary>
        DeckControl = 0x42,

        /// <summary>
        ///  Used to provide a deck’s status to the initiator of the "Give Deck Status" message (Directly addressed)
        /// </summary>
        DeckStatus = 0x1B,

        /// <summary>
        /// Reports the Vendor ID of this device (Broadcast)
        /// </summary>
        [Features(Features.VendorSpecificCommand)]
        DeviceVendorId = 0x87,

        /// <summary>
        /// Used as a response to indicate that the device does not support the requested message type, or that it cannot execute it at the present time (Directly addressed)
        /// </summary>
        [Features(Features.Abort)]
        FeatureAbort = 0x00,

        /// <summary>
        /// Used by a device to inquire which version of CEC the target supports (Directly addressed)
        /// </summary>
        [Features(Features.SystemInformation)]
        GetCecVersion = 0x9F,

        /// <summary>
        /// Sent by a device capable of character generation (for OSD and Menus) to a TV in order to discover the currently selected Menu language. Also used by a TV during installation to discover the currently set menu language from other devices (Directly addressed)
        /// </summary>
        /// <remarks>Devices which have Logical Addresses other than 0 (TV) or 14 (when a TV) shall send a FeatureAbort[“Unrecognized opcode”] message in response to a GetMenuLanguage messages and shall not send SetMenuLanguage messages.</remarks>
        [Features(Features.SystemInformation)]
        GetMenuLanguage = 0x91,

        /// <summary>
        /// Requests an amplifier to send its volume and mute status (Directly addressed)
        /// </summary>
        GiveAudioStatus = 0x71,

        /// <summary>
        /// Used to request the status of a device, regardless of whether or not it is the current active source (Directly addressed)
        /// </summary>
        GiveDeckStatus = 0x1A,

        /// <summary>
        /// Used to determine the current power status of a target device (Directly addressed)
        /// </summary>
        [Features(Features.DevicePowerStatus)]
        GiveDevicePowerStatus = 0x8F,

        /// <summary>
        /// Requests the Vendor ID from a device (Directly addressed)
        /// </summary>
        [Features(Features.VendorSpecificCommand)]
        GiveDeviceVendorId = 0x8C,

        /// <summary>
        /// Used to request the preferred OSD name of a device for use in menus associated with that device (Directly addressed)
        /// </summary>
        [Features(Features.DeviceOsdNameTransfer)]
        GiveOsdName = 0x46,

        /// <summary>
        /// A request to a device to return its physical address. (Directly addressed)
        /// </summary>
        [Features(Features.SystemInformation)]
        GivePhysicalAddress = 0x83,

        /// <summary>
        /// Requests the status of the System Audio Mode (Directly addressed)
        /// </summary>
        GiveSystemAudioModeStatus = 0x7D,

        /// <summary>
        /// Used to request the status of a tuner device (Directly addressed)
        /// </summary>
        GiveTunerDeviceStatus = 0x08,

        /// <summary>
        /// Sent by a source device to the TV whenever it enters the active state (alternatively it may send "Text View On") (Directly addressed)
        /// </summary>
        ImageViewOn = 0x04,

        /// <summary>
        /// Used by the currently active source to inform the TV that it has no video to be presented to the user, or is going into standby as the result of a local user command on the device (Directly addressed)
        /// </summary>
        [Features(Features.RoutingControl)]
        InactiveSource = 0x9D,

        /// <summary>
        ///  A request from the TV for a device to show/remove a menu or to query if a device is currently showing a menu (Directly addressed)
        /// </summary>
        [Features(Features.DeviceMenuControl)]
        MenuRequest = 0x8D,

        /// <summary>
        /// Used to indicate to the TV that the device is showing/has removed a menu and requests the remote control keys to be passed though (Directly addressed)
        /// </summary>
        [Features(Features.DeviceMenuControl)]
        MenuStatus = 0x8E,
        
        /// <summary>
        /// This message is reserved for internal purposes (Directly addressed)
        /// </summary>
        None = 0xFF,
        
        /// <summary>
        /// Used to control the playback behaviour of a source device (Directly addressed)
        /// </summary>
        Play = 0x41,
        
        /// <summary>
        /// Requests a device to stop a recording (Directly addressed)
        /// </summary>
        RecordOff = 0x0B,

        /// <summary>
        /// Attempt to record the specified source (Directly addressed). This message is used in several features : One Touch Record,Tuner Control
        /// </summary>
        RecordOn = 0x09,

        /// <summary>
        /// Used by a Recording Device to inform the initiator of the message "Record On" about its status (Directly addressed)
        /// </summary>
        RecordStatus = 0x0A,

        /// <summary>
        /// Request by the Recording Device to record the presently displayed source (Directly addressed)
        /// </summary>
        RecordTVScreen = 0x0F,

        /// <summary>
        /// Reports an amplifier’s volume and mute status (Directly addressed)
        /// </summary>
        ReportAudioStatus = 0x7A,

        /// <summary>
        /// Used by an ARC TX device to indicate that its ARC functionality has been activated.
        /// </summary>
        ReportArcInitiated = 0xC1,

        /// <summary>
        /// Used by an ARC RX device to activate the ARC functionality in an ARC TX device.
        /// </summary>
        InitiateArc = 0xC0,

        /// <summary>
        /// Used to inform all other devices of the mapping between physical and logical address of the initiator (Broadcast)
        /// </summary>
        [Features(Features.SystemInformation)]
        ReportPhysicalAddress = 0x84,

        /// <summary>
        /// Used by an ARC TX device to indicate that its ARC functionality has been deactivated.
        /// </summary>
        ReportArcTerminated = 0xC2,

        /// <summary>
        /// Used by an ARC TX device to request an ARC RX device to activate the ARC  functionality in the ARC TX device.
        /// </summary>
        RequestArcInitiated = 0xC3,

        /// <summary>
        /// Used by an ARC TX device to request an ARC RX device to deactivate the ARC functionality in the ARC TX device.
        /// </summary>
        RequestArcTermination = 0xC4,

        /// <summary>
        /// Used by an ARC RX device to deactivate the ARC functionality in an ARC TX device.
        /// </summary>
        TerminateArc = 0xC5,

        /// <summary>
        /// Used for Capability Discovery and Control,
        /// </summary>
        CdcMessage = 0xF8,

        /// <summary>
        /// Used to inform a requesting device of the current power status (Directly addressed)
        /// </summary>
        [Features(Features.DevicePowerStatus)]
        ReportPowerStatus = 0x90,

        /// <summary>
        /// Used by a new device to discover the status of the system (Broadcast)
        /// </summary>
        [Features(Features.RoutingControl)]
        RequestActiveSource = 0x85,

        /// <summary>
        /// Sent by a CEC Switch when it is manually switched to inform all other devices on the network that the active route below the switch has changed (Broadcast)
        /// </summary>
        [Features(Features.RoutingControl)]
        RoutingChange = 0x80,

        /// <summary>
        /// Sent by a CEC Switch to indicate the active route below the switch (Broadcast)
        /// </summary>
        [Features(Features.RoutingControl)]
        RoutingInformation = 0x81,

        /// <summary>
        /// Directly selects an Analogue TV service (Directly addressed)
        /// </summary>
        SelectAnalogueService = 0x92,

        /// <summary>
        /// Directly selects a Digital TV, Radio or Data Broadcast Service (Directly addressed)
        /// </summary>
        SelectDigitalService = 0x93,

        /// <summary>
        /// Used to set a single timer block on an Analogue Recording Device (Directly addressed)
        /// </summary>
        SetAnalogueTimer = 0x34,

        /// <summary>
        /// Used to control audio rate from Source Device (Directly addressed)
        /// </summary>
        SetAudioRate = 0x9A,

        /// <summary>
        /// Used to set a single timer block on a Digital Recording Device (Directly addressed)
        /// </summary>
        SetDigitalTimer = 0x97,

        /// <summary>
        /// Used to set a single timer block to record from an external device (Directly addressed)
        /// </summary>
        SetExternalTimer = 0xA2,

        /// <summary>
        /// Used by a TV or another device to indicate the menu language (Broadcast)
        /// </summary>
        [Features(Features.SystemInformation)]
        SetMenuLanguage = 0x32,

        /// <summary>
        /// Used to set the preferred OSD name of a device for use in menus associated with that device (Directly addressed)
        /// </summary>
        [Features(Features.DeviceOsdNameTransfer)]
        SetOSDName = 0x47,

        /// <summary>
        ///  Used to send a text message to output on a TV (Directly addressed)
        /// </summary>
        [Features(Features.OsdDisplay)]
        SetOSDString = 0x64,

        /// <summary>
        /// Used by the TV to request a streaming path from the specified address (Broadcast)
        /// </summary>
        [Features(Features.RoutingControl)]
        SetStreamPath = 0x86,

        /// <summary>
        /// Turns the System Audio Mode On or Off (Directly addressed or Broadcast)
        /// </summary>
        SetSystemAudioMode = 0x72,

        /// <summary>
        /// Used to set the name of a program associated with a timer block. Sent directly after sending a "Set Analogue Timer" or "Set Digital Timer" message. The name is then associated with that timer block (Directly addressed)
        /// </summary>
        SetTimerProgramTitle = 0x67,

        /// <summary>
        /// Switches one or all devices into standby mode. Can be used a broadcast message or be addressed to a specific device (Broadcast or Directly addressed)
        /// </summary>
        [Features(Features.SystemStandby)]
        StandBy = 0x36,

        /// <summary>
        /// A device implementing System Audio Control and which has volume control RC buttons (eg TV or STB) requests to use System Audio Mode to the amplifier (Directly addressed)
        /// </summary>
        SystemAudioModeRequest = 0x70,

        /// <summary>
        /// Reports the current status of the System Audio Mode (Directly addressed)
        /// </summary>
        SystemAudioModeStatus = 0x7E,

        /// <summary>
        /// As "Image View On", but should also remove any text, menus and PIP windows from the TV’s display (Directly addressed)
        /// </summary>
        TextViewOn = 0x0D,

        /// <summary>
        ///  Used to give the status of a "Clear Analogue Timer", "Clear Digital Timer" or "Clear External Timer" message (Directly addressed)
        /// </summary>
        TimerClearedStatus = 0x43,

        /// <summary>
        /// Used to send timer status to the initiator of a "Status Timer" message (Directly addressed)
        /// </summary>
        TimerStatus = 0x35,

        /// <summary>
        /// Used by a tuner device to provide its status to the initiator of the "Give Tuner Device Status" message (Directly addressed)
        /// </summary>
        TunerDeviceStatus = 0x07,

        /// <summary>
        /// Used to tune to next lowest service in a tuner’s service list. Can be used for PIP (Directly addressed)
        /// </summary>
        TunerStepDecrement = 0x06,

        /// <summary>
        /// Used to tune to next highest service in a tuner’s service list. Can be used for PIP (Directly addressed)
        /// </summary>
        TunerStepIncrement = 0x05,

        /// <summary>
        /// Used to indicate that the user pressed a remote control button or switched from one remote control button to another (Directly addressed). This message is used in several features : Device Menu Control,System Audio Control,Remote Control Pass Through
        /// </summary>
        [Features(Features.DeviceMenuControl)]
        UserControlPressed = 0x44,

        /// <summary>
        ///  Indicates that user released a remote control button (the last one indicated by the "User Control Pressed" message) (Directly addressed). This message is used in several features : Device Menu Control,System Audio Control,Remote Control Pass Through
        /// </summary>
        [Features(Features.DeviceMenuControl)]
        UserControlReleased = 0x45,

        /// <summary>
        /// Allows vendor specific commands to be sent between two devices (Directly addressed)
        /// </summary>
        [Features(Features.VendorSpecificCommand)]
        VendorCommand = 0x89,

        /// <summary>
        /// Allows vendor specific commands to be sent between two devices or broadcast (Directly addressed or Broadcast)
        /// </summary>
        [Features(Features.VendorSpecificCommand)]
        VendorCommandWithId = 0xA0,

        /// <summary>
        /// Indicates that a remote control button has been depressed (Directly addressed or Broadcast)
        /// </summary>
        [Features(Features.VendorSpecificCommand)]
        VendorRemoteButtonDown = 0x8A,

        /// <summary>
        ///  Indicates that a remote control button (the last button pressed indicated by the "Vendor Remote Button Down" message) has been released (Directly addressed or Broadcast)
        /// </summary>
        [Features(Features.VendorSpecificCommand)]
        VendorRemoteButtonUp = 0x8B
    }
}