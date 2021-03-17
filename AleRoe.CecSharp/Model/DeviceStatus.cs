namespace AleRoe.CecSharp.Model
{
    /// <summary>
    /// CEC device status values.
    /// </summary>
    public enum DeviceStatus : byte
    {
        On = 0x01,
        Off = 0x02,
        Once = 0x03
    }
}