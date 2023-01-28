namespace AleRoe.CecSharp.Model
{
    /// <summary>
    /// Available CEC devices types.
    /// </summary>
    public enum DeviceType : byte
    {
        TV = 0x00,
        Reserved = 0x02,
        RecordingDevice = 0x01,
        Tuner = 0x03,
        PlaybackDevice = 0x04,
        AudioSystem = 0x05
    }
}