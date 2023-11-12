using System;
using AleRoe.CecSharp.Extensions;
using AleRoe.CecSharp.Model;
using NUnit.Framework;

namespace AleRoe.CecSharp.Tests
{
    [TestFixture]
    public class CecMessageTests
    {
        [Test]
        public void CtorTest1()
        {
            var value = "BF:84:20:00:04";

            var message = new CecMessage(LogicalAddress.PlaybackDevice3, LogicalAddress.Unregistered,
                Command.ReportPhysicalAddress, ByteArrayHelper.Parse("20:00:04"));

            Assert.AreEqual(value, message.ToCec());
            Assert.AreEqual(LogicalAddress.PlaybackDevice3, message.Source);
            Assert.AreEqual(LogicalAddress.Unregistered, message.Destination);
            Assert.AreEqual(Command.ReportPhysicalAddress, message.Command);
            Assert.That(message.Parameters.Length > 0);
            Assert.AreEqual("20:00:04", message.Parameters.ToHex());
        }


        [Test]
        public void CtorTest2()
        {
            var expected = "BF:87:01:09:32";
            var id = 67890;

            var message = new CecMessage(LogicalAddress.PlaybackDevice3, LogicalAddress.Unregistered,
                Command.DeviceVendorId, ByteArrayHelper.ToByteArray(id, "X6"));
            Assert.AreEqual(expected, message.ToCec());
        }

        [Test]
        public void CtorTest3()
        {
            var message = CecMessage.None;
            Assert.AreEqual(LogicalAddress.Unregistered, message.Source);
            Assert.AreEqual(LogicalAddress.Unregistered, message.Destination);
            Assert.AreEqual(Command.None, message.Command);
            Assert.IsNull(message.Parameters);
        }

        [Test]
        public void ParseTest1()
        {
            //var msg = "* cec:message A 15:35:09.080 08:8F#";
            var msg = "08:8F#";
            CecMessage message = default; 

            Assert.DoesNotThrow(() => message = CecMessage.Parse(msg));
            Assert.AreEqual("08:8F", message.ToCec());
            Assert.AreEqual(LogicalAddress.TV, message.Source);
            Assert.AreEqual(LogicalAddress.PlaybackDevice2, message.Destination);
            Assert.AreEqual(Command.GiveDevicePowerStatus, message.Command);
            Assert.IsNull(message.Parameters);
        }


        [Test]
        public void ParseTest2()
        {
            //var msg = "* cec:message A 15:35:09.200 80:90:01#";
            
            var msg = "80:90:01";
            CecMessage message = default;

            Assert.DoesNotThrow(() => message = CecMessage.Parse(msg));
            Assert.AreEqual("80:90:01", message.ToCec());
            Assert.AreEqual(LogicalAddress.PlaybackDevice2, message.Source);
            Assert.AreEqual(LogicalAddress.TV, message.Destination);
            Assert.AreEqual(Command.ReportPowerStatus, message.Command);
            Assert.That(message.Parameters.Length == 1);
            Assert.That(message.Parameters[0].ToString() == "1");
        }

        [Test]
        public void ParseTest3()
        {
            //var msg = "* cec:message A 16:02:26.740 04:44:30#";

            var msg = "04:44:30";
            CecMessage message = default;

            Assert.DoesNotThrow(() => message = CecMessage.Parse(msg));
            Assert.AreEqual(LogicalAddress.TV, message.Source);
            Assert.AreEqual(LogicalAddress.PlaybackDevice1, message.Destination);
            Assert.AreEqual(Command.UserControlPressed, message.Command);
            Assert.That(message.Parameters.Length == 1);
            Assert.AreEqual(message.Parameters[0].ToString("X"), UiCommand.ChannelUp.ToString("X"));
        }

        [Test]
        public void ParseTest4()
        {
            //var msg = "* cec:message A 16:33:38.780 80:47:41:6D:61:7A:6F:6E:20:46:69:72:65:54:56#";
            var paramValue = "Amazon FireTV";
            var msg = "80:47:41:6D:61:7A:6F:6E:20:46:69:72:65:54:56";
            CecMessage message = default;

            Assert.DoesNotThrow(() => message = CecMessage.Parse(msg));
            Assert.AreEqual(LogicalAddress.PlaybackDevice2, message.Source);
            Assert.AreEqual(LogicalAddress.TV, message.Destination);
            Assert.AreEqual(Command.SetOSDName, message.Command);
            Assert.That(message.Parameters.Length > 0);
            Assert.AreEqual(paramValue, message.Parameters.ToASCIIString());
        }

        

        [Test]
        public void ParseTest5()
        {
            //var msg = "* cec:message A 16:02:26.740 33#";
            var msg = "33";
            CecMessage message = default;

            Assert.DoesNotThrow(() => message = CecMessage.Parse(msg));
            Assert.AreEqual(message.Destination, message.Source);
        }

        [Test]
        public void ParseTest6()
        {
            var msg = "";
            Assert.Throws<ArgumentNullException>(() => CecMessage.Parse(msg));
        }

        [Test]
        public void ParseTest7()
        {
            //var msg = "* cec:message A 16:02:26.740 33#";
            var msg = "33#";
            CecMessage message = default;

            Assert.DoesNotThrow(() => message = CecMessage.Parse(msg));
            Assert.IsTrue(message.IsAcknowledged());
        }

        [Test]
        public void ParseTest8()
        {
            //var msg = "cec:message A 11:21:13.200 81:83#";
            var msg = "81:83#";
            CecMessage message = default;

            Assert.DoesNotThrow(() => message = CecMessage.Parse(msg));
            Assert.AreEqual(LogicalAddress.PlaybackDevice2, message.Source);
            Assert.AreEqual(LogicalAddress.RecordingDevice1, message.Destination);
            Assert.AreEqual(Command.GivePhysicalAddress, message.Command);
            Assert.IsTrue(message.IsAcknowledged());
        }

        [Test]
        public void EqualsTest_NoneValuesAreEqual()
        {
            CecMessage message1 = CecMessage.None;
            CecMessage message2 = CecMessage.None;
            Assert.AreEqual(message1, message2);
            Assert.IsTrue(message1.Equals(message2));
            Assert.IsTrue(Equals(message1, message2));
        }

        [Test]
        public void EqualsTest_ValuesAreEqual()
        {
            CecMessage message1 = CecMessageBuilder.CecVersion(LogicalAddress.PlaybackDevice1, LogicalAddress.TV, CecVersion.Version14);
            CecMessage message2 = CecMessageBuilder.CecVersion(LogicalAddress.PlaybackDevice1, LogicalAddress.TV, CecVersion.Version14);
            Assert.AreEqual(message1, message2);
            Assert.IsTrue(message1.Equals(message2));
            Assert.IsTrue(Equals(message1, message2));
        }

        [Test]
        public void EqualsTest_DefaultValuesAreEqual()
        {
            CecMessage message1 = default;
            CecMessage message2 = default;
            Assert.AreEqual(message1, message2);
            Assert.IsTrue(message1.Equals(message2));
            Assert.IsTrue(Equals(message1, message2));
        }

        
        [Test]
        public void NotEqualsTest_Success()
        {
            var message1 = CecMessage.None;
            var message2 = CecMessage.Parse("33#");

            Assert.AreNotEqual(message1, message2);
            Assert.IsFalse(message1.Equals(message2));
            Assert.IsFalse(Equals(message1, message2));
        }

        [Test]
        public void NotEqualsTest_WrongTypesAreNotEqual()
        {
            var message1 = CecMessage.None;
            var message2 = Guid.NewGuid();
            Assert.IsFalse(message1.Equals(message2));
            Assert.IsFalse(Equals(message1, message2));
        }

        [Test]
        public void OperatorTest_ValuesAreEqual()
        {
            var message1 = CecMessage.None;
            var message2 = CecMessage.None;
            Assert.IsTrue(message1 == message2);
        }

        [Test]
        public void OperatorTest_ValuesAreNotEqual()
        {
            CecMessage message1 = CecMessage.None;
            CecMessage message2 = default;
            Assert.IsTrue(message1 != message2);
        }
    }
}