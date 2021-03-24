# CecSharp
A .net class library to facilitate HDMI-CEC communications. 

## Overview
This library can be used to create and parse HDMI Version 1.4 compliant CEC-messages for communication with HDMI-CEC devices.

### Installing via NuGet

Install-Package AleRoe.CecSharp

## Basic Usage
Use the static `CecMessageBuilder` class to build `CecMessage` structs.

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
## Platform Support
The API is supported on all platforms.
