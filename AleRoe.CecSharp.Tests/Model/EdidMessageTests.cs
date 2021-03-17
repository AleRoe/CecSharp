using System;
using AleRoe.CecSharp.Extensions;
using AleRoe.CecSharp.Model;
using NUnit.Framework;

namespace AleRoe.CecSharp.Tests.Model
{
    [TestFixture]
    public class EdidMessageTests
    {
        [Test]
        public void ParseTest1()
        {
            var paramValue =
                "00:ff:ff:ff:ff:ff:ff:00:31:e5:10:90:01:01:01:01:00:ff:01:03:80:a0:5a:78:0a:0d:c9:a0:57:47:98:27:12:48:4c:20:00:00:31:40:01:01:01:01:01:01:01:01:01:01:01:01:01:01:04:74:00:30:f2:70:5a:80:b0:58:8a:00:c2:ad:42:00:00:1e:02:3a:80:18:71:38:2d:40:58:2c:45:00:c2:ad:42:00:00:1e:00:00:00:fc:00:4c:4f:45:57:45:20:48:44:4d:49:20:54:56:00:00:00:fd:00:18:55:0f:5b:1e:00:0a:20:20:20:20:20:20:01:2d";
            var msg = "0x00 " + paramValue;
            EdidMessage message = default;

            Assert.DoesNotThrow(() => message = EdidMessage.Parse(msg));
            Assert.AreEqual(0, message.Block, "Block value failed");
            StringAssert.AreEqualIgnoringCase(paramValue, message.Data.ToHex());
        }

        [Test]
        public void ParseTest2()
        {
            var paramValue =
                "02:03:32:70:4e:5f:5e:5d:10:1f:21:20:22:04:13:03:12:05:14:2c:15:07:50:3d:06:c0:57:06:01:09:07:07:83:01:00:00:6d:03:0c:00:10:00:80:3c:20:08:60:01:02:03:04:74:00:30:f2:70:5a:80:b0:58:8a:00:c2:ad:42:00:00:1e:02:3a:80:d0:72:38:2d:40:10:2c:45:80:c2:ad:42:00:00:1e:01:1d:00:bc:52:d0:1e:20:b8:28:55:40:c2:ad:42:00:00:18:00:00:00:00:00:00:00:00:00:00:00:00:00:00:00:00:00:00:00:00:00:00:00:80";
            var msg = "0x80 " + paramValue;
            EdidMessage message = default;

            Assert.DoesNotThrow(() => message = EdidMessage.Parse(msg));
            Assert.AreEqual(128, message.Block, "Block value failed");
            StringAssert.AreEqualIgnoringCase(paramValue, message.Data.ToHex());
        }

        [Test]
        public void ParseTest3()
        {
            var msg = "";
            Assert.Throws<ArgumentNullException>(() => EdidMessage.Parse(msg));
        }

        [Test]
        public void ParseTest4()
        {
            var msg = "0x80";
            Assert.Throws<ArgumentException>(() => EdidMessage.Parse(msg));
        }
    }
}