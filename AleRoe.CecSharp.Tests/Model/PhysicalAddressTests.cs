using System;
using AleRoe.CecSharp.Model;
using NUnit.Framework;

namespace AleRoe.CecSharp.Tests.Model
{
    [TestFixture]
    public class PhysicalAddressTests
    {
        [Test]
        public void CtorTest()
        {
            byte value1 = 0x10;
            byte value2 = 0x00;

            var result = new PhysicalAddress(value1, value2);
            Assert.AreEqual((value1, value2), result.Address);
        }

        [Test]
        public void CtorTest_NullValues()
        {
            byte value1 = 0xF2;
            byte value2 = 0x00;

            var result = new PhysicalAddress(value1, value2);
            Assert.AreEqual((value1, value2), result.Address);
        }

        [Test]
        public void ParseTest()
        {
            var expected = ValueTuple.Create(Convert.ToByte("21", 16), Convert.ToByte("00", 16));
            var result = PhysicalAddress.Parse("2.1.0.0");
            Assert.AreEqual(expected, result.Address);
        }

        [Test]
        public void ToStringTest()
        {
            var value = "2.1.0.0";
            var result = PhysicalAddress.Parse(value);
            Assert.AreEqual(value, result.ToString());
        }

        [Test]
        public void ParseTest_FormatException()
        {
            var value = "G.1.0.0";
            Assert.Throws<FormatException>(() => _ = PhysicalAddress.Parse(value));
        }

        [Test]
        public void ParseTest_ArgumentException1()
        {
            var value = "2.1.0.10";
            Assert.Throws<ArgumentException>(() => _ = PhysicalAddress.Parse(value));
        }

        [Test]
        public void ParseTest_ArgumentException2()
        {
            var value = "2.1.0";
            Assert.Throws<ArgumentException>(() => _ = PhysicalAddress.Parse(value));
        }

        [Test]
        public void EqualsTest()
        {
            var value1 = PhysicalAddress.Parse("2.1.0.1");
            var value2 = PhysicalAddress.Parse("2.1.0.1");
            Assert.AreEqual(value1, value2);
            Assert.IsTrue(value1.Equals(value2));
            Assert.IsTrue(Equals(value1, value2));
        }

        [Test]
        public void NotEqualsTest()
        {
            var value1 = PhysicalAddress.Parse("2.1.0.0");
            var value2 = PhysicalAddress.Parse("2.1.0.1");
            Assert.AreNotEqual(value1, value2);
            Assert.IsFalse(value1.Equals(value2));
            Assert.IsFalse(Equals(value1, value2));
        }

        [Test]
        public void EqualsTest_Null()
        {
            var value = PhysicalAddress.Parse("2.1.0.1");
            Assert.IsFalse(value.Equals(null));
        }

    }
}