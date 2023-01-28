namespace AleRoe.CecSharp.Model
{
    /// <summary>
    /// CEC audio mute status values.
    /// </summary>
    public enum AudioMuteStatus : byte
    {
        AudioMuteOff = 0x00,
        AudioMuteOn = 0x80
    }
}