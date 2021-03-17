using System;

namespace AleRoe.CecSharp.Model
{
    internal class DeviceTypeAttribute : Attribute
    {
        public DeviceTypeAttribute(DeviceType deviceType)
        {
            DeviceType = deviceType;
        }

        public DeviceType DeviceType { get; }
    }
}