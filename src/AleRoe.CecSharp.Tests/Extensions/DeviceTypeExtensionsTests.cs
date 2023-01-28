using System.Collections.Generic;
using System.Linq;
using AleRoe.CecSharp.Extensions;
using AleRoe.CecSharp.Model;
using NUnit.Framework;

namespace AleRoe.CecSharp.Tests.Extensions
{
    [TestFixture()]
    public class DeviceTypeExtensionsTests
    {
        [Test()]
        public void GetLogicalAddressesTest()
        {
            List<LogicalAddress> result = null;
            var deviceType = DeviceType.PlaybackDevice;
            Assert.DoesNotThrow(() => result = deviceType.GetLogicalAddresses().ToList());
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            CollectionAssert.Contains(result, LogicalAddress.PlaybackDevice1);
            CollectionAssert.Contains(result, LogicalAddress.PlaybackDevice2);
            CollectionAssert.Contains(result, LogicalAddress.PlaybackDevice3);
        }

        [Test()]
        public void GetLogicalAddressesAreOrderedTest()
        {
            List<LogicalAddress> result = null;
            var deviceType = DeviceType.PlaybackDevice;
            Assert.DoesNotThrow(() => result = deviceType.GetLogicalAddresses().ToList());
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(LogicalAddress.PlaybackDevice1, result[0]);
            Assert.AreEqual(LogicalAddress.PlaybackDevice2, result[1]);
            Assert.AreEqual(LogicalAddress.PlaybackDevice3, result[2]);
        }

        [Test()]
        public void GetLogicalAddressesToTopTest()
        {
            List<LogicalAddress> result = null;
            var deviceType = DeviceType.PlaybackDevice;
            Assert.DoesNotThrow(() => result = deviceType.GetLogicalAddresses(LogicalAddress.PlaybackDevice3).ToList());
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(LogicalAddress.PlaybackDevice3, result[0]);
            Assert.AreEqual(LogicalAddress.PlaybackDevice1, result[1]);
            Assert.AreEqual(LogicalAddress.PlaybackDevice2, result[2]);
        }
    }
}