using System.Linq;
using AleRoe.CecSharp.Extensions;
using AleRoe.CecSharp.Model;
using NUnit.Framework;

namespace AleRoe.CecSharp.Tests.Extensions
{
    [TestFixture]
    public class ByteArrayHelperTests
    {
        [Test]
        public void ToByteArrayTestCombined()
        {
            var expected = "20:00:04";
            var data = ByteArrayHelper.ToByteArray(PhysicalAddress.Parse("2.0.0.0").Address)
                .Concat(ByteArrayHelper.ToByteArray(DeviceType.PlaybackDevice)).ToArray();
            Assert.AreEqual(expected, data.ToHex());
            Assert.AreEqual(ByteArrayHelper.Parse(expected), data);
        }

        [Test]
        public void ToByteArrayTestFromBool()
        {
            var expected = "01";
            var result = ByteArrayHelper.ToByteArray(true);
            Assert.AreEqual(expected, result.ToHex());
        }

        [Test]
        public void ToByteArrayTestFromByteEnum()
        {
            var value = CecVersion.Version14;
            var result = ByteArrayHelper.ToByteArray(value);
            Assert.That(result.Length == 1);
            Assert.AreEqual((byte) value, result[0]);
        }

        [Test]
        public void ToByteArrayTestFromByteString()
        {
            var value = "01:09:32";
            var result = ByteArrayHelper.Parse(value);
            StringAssert.AreEqualIgnoringCase(value, result.ToHex());
        }

        [Test]
        public void ToByteArrayTestFromByteTuple()
        {
            byte value1 = 0x10;
            byte value2 = 0x00;

            var result = ByteArrayHelper.ToByteArray((value1, value2));
            Assert.That(result.Length == 2);
            Assert.AreEqual(value1, result[0]);
            Assert.AreEqual(value2, result[1]);
        }

        [Test]
        public void ToByteArrayTestFromInt()
        {
            var expected = "01:09:32";
            var value = 67890;
            var result = ByteArrayHelper.ToByteArray(value, "X6");
            Assert.AreEqual(expected, result.ToHex());
        }

        [Test]
        public void ToByteArrayTestFromString()
        {
            var expected = "54:65:6c:65:6b:6f:6d:20:4d:52:34:30:31";
            var value = "Telekom MR401";
            var result = ByteArrayHelper.ToByteArray(value);
            StringAssert.AreEqualIgnoringCase(expected, result.ToHex());
            Assert.AreEqual(value, result.ToASCIIString());
        }
    }
}