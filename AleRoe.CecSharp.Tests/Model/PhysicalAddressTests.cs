using System;
using AleRoe.CecSharp.Model;
using NUnit.Framework;

namespace AleRoe.CecSharp.Tests.Model
{
    [TestFixture]
    public class PhysicalAddressTests
    {
        [Test]
        public void CtorFromBytes()
        {
            byte value1 = 0x10;
            byte value2 = 0x00;

            var result = new PhysicalAddress(value1, value2);
            Assert.AreEqual((value1, value2), result.Address);
        }

        [Test]
        public void CtorFromString()
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
    }
}