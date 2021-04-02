using System;
using AleRoe.CecSharp.Extensions;
using NUnit.Framework;

namespace AleRoe.CecSharp.Tests
{
    [TestFixture]
    public class EdidMessageTests
    {

        [Test]
        public void OperatorTest_DefaultValuesAreEqual()
        {
            EdidMessage message1 = default;
            EdidMessage message2 = default;
            Assert.IsTrue(message1 == message2);
        }

        [Test]
        public void OperatorTest_ValuesAreEqual()
        {
            var msg = "0x00 00:ff:ff:ff:ff:ff:ff:00:31:e5:10:90:01:01:01:01:00:ff:01:03:80:a0:5a:78:0a:0d:c9:a0:57:47:98:27:12:48:4c:20:00:00:31:40:01:01:01:01:01:01:01:01:01:01:01:01:01:01:04:74:00:30:f2:70:5a:80:b0:58:8a:00:c2:ad:42:00:00:1e:02:3a:80:18:71:38:2d:40:58:2c:45:00:c2:ad:42:00:00:1e:00:00:00:fc:00:4c:4f:45:57:45:20:48:44:4d:49:20:54:56:00:00:00:fd:00:18:55:0f:5b:1e:00:0a:20:20:20:20:20:20:01:2d"; ;
            EdidMessage message1 = EdidMessage.Parse(msg);
            EdidMessage message2 = EdidMessage.Parse(msg);
            Assert.IsTrue(message1 == message2);
        }

        [Test]
        public void OperatorTest_DifferentBlockValuesAreNotEqual()
        {
            var msg1 = "0x00 00:ff:ff:ff:ff:ff:ff:00:31:e5:10:90:01:01:01:01:00:ff:01:03:80:a0:5a:78:0a:0d:c9:a0:57:47:98:27:12:48:4c:20:00:00:31:40:01:01:01:01:01:01:01:01:01:01:01:01:01:01:04:74:00:30:f2:70:5a:80:b0:58:8a:00:c2:ad:42:00:00:1e:02:3a:80:18:71:38:2d:40:58:2c:45:00:c2:ad:42:00:00:1e:00:00:00:fc:00:4c:4f:45:57:45:20:48:44:4d:49:20:54:56:00:00:00:fd:00:18:55:0f:5b:1e:00:0a:20:20:20:20:20:20:01:2d"; 
            var msg2 = "0x01 00:ff:ff:ff:ff:ff:ff:00:31:e5:10:90:01:01:01:01:00:ff:01:03:80:a0:5a:78:0a:0d:c9:a0:57:47:98:27:12:48:4c:20:00:00:31:40:01:01:01:01:01:01:01:01:01:01:01:01:01:01:04:74:00:30:f2:70:5a:80:b0:58:8a:00:c2:ad:42:00:00:1e:02:3a:80:18:71:38:2d:40:58:2c:45:00:c2:ad:42:00:00:1e:00:00:00:fc:00:4c:4f:45:57:45:20:48:44:4d:49:20:54:56:00:00:00:fd:00:18:55:0f:5b:1e:00:0a:20:20:20:20:20:20:01:2d"; 
            EdidMessage message1 = EdidMessage.Parse(msg1);
            EdidMessage message2 = EdidMessage.Parse(msg2);
            Assert.IsTrue(message1 != message2);
        }

        [Test]
        public void OperatorTest_DifferentDataValuesAreNotEqual()
        {
            var msg1 = "0x00 00:ff:ff:ff:ff:ff:ff:00:31:e5:10:90:01:01:01:01:00:ff:01:03:80:a0:5a:78:0a:0d:c9:a0:57:47:98:27:12:48:4c:20:00:00:31:40:01:01:01:01:01:01:01:01:01:01:01:01:01:01:04:74:00:30:f2:70:5a:80:b0:58:8a:00:c2:ad:42:00:00:1e:02:3a:80:18:71:38:2d:40:58:2c:45:00:c2:ad:42:00:00:1e:00:00:00:fc:00:4c:4f:45:57:45:20:48:44:4d:49:20:54:56:00:00:00:fd:00:18:55:0f:5b:1e:00:0a:20:20:20:20:20:20:01:2d";
            var msg2 = "0x00 01:ff:ff:ff:ff:ff:ff:00:31:e5:10:90:01:01:01:01:00:ff:01:03:80:a0:5a:78:0a:0d:c9:a0:57:47:98:27:12:48:4c:20:00:00:31:40:01:01:01:01:01:01:01:01:01:01:01:01:01:01:04:74:00:30:f2:70:5a:80:b0:58:8a:00:c2:ad:42:00:00:1e:02:3a:80:18:71:38:2d:40:58:2c:45:00:c2:ad:42:00:00:1e:00:00:00:fc:00:4c:4f:45:57:45:20:48:44:4d:49:20:54:56:00:00:00:fd:00:18:55:0f:5b:1e:00:0a:20:20:20:20:20:20:01:2d";
            EdidMessage message1 = EdidMessage.Parse(msg1);
            EdidMessage message2 = EdidMessage.Parse(msg2);
            Assert.IsTrue(message1 != message2);
        }

        [Test]
        public void EqualsTest_DefaultValuesAreEqual()
        {
            EdidMessage message1 = default;
            EdidMessage message2 = default;
            Assert.AreEqual(message1, message2);
            Assert.IsTrue(message1.Equals(message2));
            Assert.IsTrue(Equals(message1, message2));
        }

        [Test]
        public void EqualsTest_ValuesAreEqual()
        {
            var paramValue =
                "00:ff:ff:ff:ff:ff:ff:00:31:e5:10:90:01:01:01:01:00:ff:01:03:80:a0:5a:78:0a:0d:c9:a0:57:47:98:27:12:48:4c:20:00:00:31:40:01:01:01:01:01:01:01:01:01:01:01:01:01:01:04:74:00:30:f2:70:5a:80:b0:58:8a:00:c2:ad:42:00:00:1e:02:3a:80:18:71:38:2d:40:58:2c:45:00:c2:ad:42:00:00:1e:00:00:00:fc:00:4c:4f:45:57:45:20:48:44:4d:49:20:54:56:00:00:00:fd:00:18:55:0f:5b:1e:00:0a:20:20:20:20:20:20:01:2d";
            var msg = "0x00 " + paramValue;

            EdidMessage message1 = EdidMessage.Parse(msg);
            EdidMessage message2 = EdidMessage.Parse(msg);
            Assert.AreEqual(message1, message2);
            Assert.IsTrue(message1.Equals(message2));
            Assert.IsTrue(Equals(message1, message2));
        }

        [Test]
        public void ParseTest_Success()
        {
            var paramValue =
                "00:ff:ff:ff:ff:ff:ff:00:31:e5:10:90:01:01:01:01:00:ff:01:03:80:a0:5a:78:0a:0d:c9:a0:57:47:98:27:12:48:4c:20:00:00:31:40:01:01:01:01:01:01:01:01:01:01:01:01:01:01:04:74:00:30:f2:70:5a:80:b0:58:8a:00:c2:ad:42:00:00:1e:02:3a:80:18:71:38:2d:40:58:2c:45:00:c2:ad:42:00:00:1e:00:00:00:fc:00:4c:4f:45:57:45:20:48:44:4d:49:20:54:56:00:00:00:fd:00:18:55:0f:5b:1e:00:0a:20:20:20:20:20:20:01:2d";
            var msg = "0x00 " + paramValue;

            var message = EdidMessage.Parse(msg);
            Assert.AreEqual(0, message.Block, "Block value failed");
            StringAssert.AreEqualIgnoringCase(paramValue, message.Data.ToHex());
        }

        [Test]
        public void ParseTest_EmptyValueThrows()
        {
            var msg = "";
            Assert.Throws<ArgumentNullException>(() => EdidMessage.Parse(msg));
        }

        [Test]
        public void ParseTest_InvalidFormatThrows()
        {
            var msg = "0x80 Z2:03:32:70:4e:5f:5e:5d:10:1f";
            Assert.Throws<FormatException>(() => EdidMessage.Parse(msg));
        }

        [Test]
        public void ParseTest_InvalidDataThrows()
        {
            var msg = "0x80";
            Assert.Throws<ArgumentException>(() => EdidMessage.Parse(msg));
        }
    }
}