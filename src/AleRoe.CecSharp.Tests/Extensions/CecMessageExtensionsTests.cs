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
            Assert.AreEqual(expected, message.ToCec());
        }

        [Test()]
        public void ToVerboseTest()
        {
            var expected = "GivePhysicalAddress";
            var message = CecMessageBuilder.GivePhysicalAddress(LogicalAddress.TV, LogicalAddress.PlaybackDevice1);
            Assert.AreEqual(expected, message.ToVerbose());
        }
    }
}