using AleRoe.CecSharp.Extensions;
using AleRoe.CecSharp.Model;
using NUnit.Framework;

namespace AleRoe.CecSharp.Tests.Extensions
{
    [TestFixture()]
    public class LogicalAddressExtensionsTests
    {
        [Test()]
        public void GetDeviceTypeTest()
        {
            DeviceType result = default;
            var value = LogicalAddress.PlaybackDevice1;
            Assert.DoesNotThrow(() => result = value.GetDeviceType());
            Assert.AreEqual(DeviceType.PlaybackDevice, result);
        }

        [Test]
        public void ToStringTest()
        {
            var value = LogicalAddress.Reserved1;
            Assert.AreEqual("C", value.AsString());
        }
    }
}