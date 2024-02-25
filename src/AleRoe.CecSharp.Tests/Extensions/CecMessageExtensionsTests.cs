using AleRoe.CecSharp.Extensions;
using AleRoe.CecSharp.Model;
using NUnit.Framework;

namespace AleRoe.CecSharp.Tests.Extensions
{
    [TestFixture()]
    public class CecMessageExtensionsTests
    {
        [Test()]
        public void ToCecTest()
        {
            var expected = "04:83";
            var message = CecMessageBuilder.GivePhysicalAddress(LogicalAddress.TV, LogicalAddress.PlaybackDevice1);
            Assert.That(expected, Is.EqualTo(message.ToCec()));
        }

        [Test()]
        public void ToVerboseTest_GivePhysicalAddress()
        {
            var expected = "GivePhysicalAddress";
            var message = CecMessageBuilder.GivePhysicalAddress(LogicalAddress.TV, LogicalAddress.PlaybackDevice1);
            Assert.That(expected, Is.EqualTo(message.ToVerbose()));
        }

        [Test()]
        public void ToVerboseTest_ReportPhysicalAddress()
        {
            var expected = "ReportPhysicalAddress - Address: 2.1.0.0 Type: PlaybackDevice";
            var message = CecMessageBuilder.ReportPhysicalAddress(LogicalAddress.PlaybackDevice1, DeviceType.PlaybackDevice, PhysicalAddress.Parse("2.1.0.0"));
            Assert.That(message.ToVerbose(), Is.EqualTo(expected));
        }

        [Test()]
        public void ToVerboseTest_ReportPowerStatus()
        {
            var expected = "ReportPowerStatus - Status: Standby";
            var message = CecMessageBuilder.ReportPowerStatus(LogicalAddress.PlaybackDevice1, LogicalAddress.TV, PowerStatus.Standby);
            Assert.That(message.ToVerbose(), Is.EqualTo(expected));
        }

        [Test()]
        public void ToVerboseTest_DeviceVendorId()
        {
            var expected = "DeviceVendorId - Id: 9845";
            var message = CecMessageBuilder.DeviceVendorId(LogicalAddress.PlaybackDevice1, 9845);
            Assert.That(message.ToVerbose(), Is.EqualTo(expected));
        }

        [Test()]
        public void ToVerboseTest_CecVersion()
        {
            var expected = "CecVersion - Version: Version20";
            var message = CecMessageBuilder.CecVersion(LogicalAddress.PlaybackDevice1, LogicalAddress.TV, CecVersion.Version20);
            Assert.That(message.ToVerbose(), Is.EqualTo(expected));
        }
    }
}