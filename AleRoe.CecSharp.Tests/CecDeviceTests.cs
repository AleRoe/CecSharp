using AleRoe.CecSharp.Model;
using NUnit.Framework;

namespace AleRoe.CecSharp.Tests
{
    [TestFixture()]
    public class CecDeviceTests
    {
        [Test()]
        public void CtorTest()
        {
            var device = new CecDevice(DeviceType.PlaybackDevice, "SomeName", 999);
            Assert.AreEqual(DeviceType.PlaybackDevice, device.DeviceType);
            Assert.AreEqual("SomeName", device.OsdName);
            Assert.AreEqual(999, device.VendorId);
            Assert.AreEqual(PhysicalAddress.None, device.PhysicalAddress);
            Assert.AreEqual(LogicalAddress.Unregistered, device.LogicalAddress);
        }

        [Test()]
        public void CtorTest1()
        {
            var device = new CecDevice(DeviceType.PlaybackDevice, "SomeName", 999,PhysicalAddress.Parse("2.1.0.0"));
            Assert.AreEqual(DeviceType.PlaybackDevice, device.DeviceType);
            Assert.AreEqual("SomeName", device.OsdName);
            Assert.AreEqual(999, device.VendorId);
            Assert.AreEqual(PhysicalAddress.Parse("2.1.0.0"), device.PhysicalAddress);
            Assert.AreEqual(LogicalAddress.Unregistered, device.LogicalAddress);
        }

        [Test()]
        public void CtorTest2()
        {
            var device = new CecDevice(DeviceType.PlaybackDevice, "SomeName", 999, PhysicalAddress.Parse("2.1.0.0"), LogicalAddress.Tuner3);
            Assert.AreEqual(DeviceType.PlaybackDevice, device.DeviceType);
            Assert.AreEqual("SomeName", device.OsdName);
            Assert.AreEqual(999, device.VendorId);
            Assert.AreEqual(PhysicalAddress.Parse("2.1.0.0"), device.PhysicalAddress);
            Assert.AreEqual(LogicalAddress.Tuner3, device.LogicalAddress);
        }

        [Test()]
        public void ProcessCecMessageTest()
        {
            var device = new CecDevice(DeviceType.PlaybackDevice, "SomeName", 999, PhysicalAddress.Parse("2.1.0.0"), LogicalAddress.Tuner3);
            var message = CecMessageBuilder.GivePhysicalAddress(LogicalAddress.TV, LogicalAddress.Tuner3);

            var response = device.ProcessCecMessage(message);
            Assert.AreEqual(CecMessageBuilder.ReportPhysicalAddress(LogicalAddress.Tuner3, DeviceType.PlaybackDevice,
                PhysicalAddress.Parse("2.1.0.0")), response);

        }
    }
}