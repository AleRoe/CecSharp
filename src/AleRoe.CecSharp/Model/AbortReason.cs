namespace AleRoe.CecSharp.Model
{
    /// <summary>
    /// CEC abort reason values.
    /// </summary>
    public enum AbortReason : byte
    {
        UnrecognizedOpcode = 0x00,
        NotInCorrectModeToRespond = 0x01,
        CannotProvideSource = 0x02,
        InvalidOperand = 0x03,
        Refused = 0x04
    }
}