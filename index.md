# CecSharp
A .net class library to facilitate HDMI-CEC communications. 

## Overview
This library can be used to create and parse HDMI Version 1.4 compliant CEC-messages for communication with HDMI-CEC devices. It can be used on top of other communication protocols and libraries which support CEC hardware, such as kwikwai HDMI-CEC AV-controllers or Pulse-Eight's USB-CEC adapter.

## Installing via NuGet
```csharp
Install-Package AleRoe.CecSharp
```

## Basic Usage
CecSharp provides an object model for dealing with HDMI-CEC messages as well as emulating a HDMI-CEC device. The static `CecMessageBuilder` class can be used to create arbitrary HDMI-CEC frames which can be used to control devices on the HDMI-CEC bus. 
The `CecDevice` class represents a HDMI-CEC enabled device capable of responding to incoming HDMI-CEC messages via the `ProcessCecMessage` method.

### CecDevice
To emulate a HDMI-CEC device, create a new `CecDevice` instance, specifying the device type, name, vendor, physical- and logical address. Use the `ProcessCecMessage()` and `ToCec()`methods to get the appropriate response depending on the device settings. Not all input messages will generate a response (like UserControlPressed, UserControlReleased or StandBy and messages not intended for this device) and instead return `CecMessage.None`.

```csharp
// create device
var device = new CecDevice(DeviceType.PlaybackDevice, "OsdName", 99999, PhysicalAddress.Parse("2.1.0.0"), LogicalAddress.Tuner3);

// create a CecMessage from an incoming CEC-frame
// i.e. a request to a device to return its physical address. (Directly addressed)
var frame = "07:83";
var message = CecMessage.Parse(frame);

// process the message to get the appropriate response
var response = device.ProcessCecMessage(message).ToCec();

// returns a ReportPhysicalAddress broadcast message which can be sent as a response
// response = "7F:84:21:00:04"
```
### CecMessageBuilder

In order to create arbitrary CEC-frames, use the static `CecMessageBuilder` class to build `CecMessage` structs.
```csharp
public static class CecMessageBuilder
{
    public static CecMessage GivePhysicalAddress(LogicalAddress source, LogicalAddress destination);
    public static CecMessage ReportPhysicalAddress(LogicalAddress source, DeviceType deviceType, PhysicalAddress physicalAddress);
    public static CecMessage ReportAudioStatus(LogicalAddress source, LogicalAddress destination, AudioMuteStatus status, int value);
    public static CecMessage SetSystemAudioMode(LogicalAddress source, LogicalAddress destination, SystemAudioStatus status);
    public static CecMessage DeviceVendorId(LogicalAddress source, int vendorId);
    public static CecMessage SetOsdName(LogicalAddress source, LogicalAddress destination, string osdName);
    public static CecMessage MenuStatus(LogicalAddress source, LogicalAddress destination, MenuStatus state);
    public static CecMessage InactiveSource(LogicalAddress source, PhysicalAddress physicalAddress);
    public static CecMessage ActiveSource(LogicalAddress source, PhysicalAddress physicalAddress);
    public static CecMessage FeatureAbort(LogicalAddress source, LogicalAddress destination, Command opCode, AbortReason reason);
    public static CecMessage CecVersion(LogicalAddress source, LogicalAddress destination, CecVersion version);
    public static CecMessage ReportPowerStatus(LogicalAddress source, LogicalAddress destination, PowerStatus status);
    public static CecMessage Polling(LogicalAddress source);
}
```
Use the `.ToCec()` extension method to get the actual CEC-Message.

```csharp
using AleRoe.CecSharp;
using AleRoe.CecSharp.Extensions;

var message = CecMessageBuilder.ActiveSource(LogicalAddress.Unregistered, PhysicalAddress.Parse("2.0.0.0"));
var cec = message.ToCec();
// cec = "FF:82:20:00"
```
## Feedback
Please let me know if you are using this library and have any suggestions or questions.
