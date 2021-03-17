namespace AleRoe.CecSharp.Model
{
    /// <summary>
    /// CEC logical addresses
    /// </summary>
    public enum LogicalAddress : byte
    {
        [DeviceType(DeviceType.TV)] 
        TV = 0x00,

        [DeviceType(DeviceType.RecordingDevice)]
        RecordingDevice1 = 0x01,

        [DeviceType(DeviceType.RecordingDevice)]
        RecordingDevice2 = 0x02,

        [DeviceType(DeviceType.Tuner)] 
        Tuner1 = 0x03,

        [DeviceType(DeviceType.PlaybackDevice)]
        PlaybackDevice1 = 0x04,

        [DeviceType(DeviceType.AudioSystem)] 
        AudioSystem = 0x05,

        [DeviceType(DeviceType.Tuner)] 
        Tuner2 = 0x06,

        [DeviceType(DeviceType.Tuner)] 
        Tuner3 = 0x07,

        [DeviceType(DeviceType.PlaybackDevice)]
        PlaybackDevice3 = 0x0B,

        [DeviceType(DeviceType.PlaybackDevice)]
        PlaybackDevice2 = 0x08,

        [DeviceType(DeviceType.RecordingDevice)]
        RecordingDevice3 = 0x09,

        [DeviceType(DeviceType.Tuner)] 
        Tuner4 = 0x0A,

        Reserved1 = 0x0C,
        Reserved2 = 0x0D,
        FreeUse = 0x0E,
        Unregistered = 0x0F
    }
}