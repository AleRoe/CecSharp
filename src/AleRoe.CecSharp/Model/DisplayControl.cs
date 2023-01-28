namespace AleRoe.CecSharp.Model
{
    public enum DisplayControl : byte
    {
        DisplayForDefaultTime = 0x00,
        DisplayUntilCleared = 0x40,
        ClearPreviousMessage = 0x80,
        ReservedForFutureUse = 0xC0
    }
}