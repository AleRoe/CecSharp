namespace AleRoe.CecSharp.Model
{
    /// <summary>
    /// CEC power status values.
    /// </summary>
    public enum PowerStatus : byte
    {
        On = 0x00,
        Standby = 0x01,
        InTransitionOntoStandby = 0x02,
        InTransitionStandbyToOn = 0x03
    }
}