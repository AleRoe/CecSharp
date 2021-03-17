using System.Collections;
using System.Collections.Generic;
using AleRoe.CecSharp.Extensions;
using AleRoe.CecSharp.Model;
using NUnit.Framework;

namespace AleRoe.CecSharp.Tests
{
    [TestFixture()]
    public class CecMessageBuilderTests
    {
        [Test, TestCaseSource(typeof(BuilderCases))]
        public void BuilderTest(CecMessage message, string expected)
        {
            Assert.AreEqual(expected, message.ToCec());
        }
    }
    class BuilderCases : IEnumerable<TestCaseData>
    {
        public IEnumerator<TestCaseData> GetEnumerator()
        {
            yield return new TestCaseData(CecMessageBuilder.ReportPowerStatus(LogicalAddress.Unregistered, LogicalAddress.Unregistered, PowerStatus.On), "FF:90:00") {TestName = nameof(CecMessageBuilder.ReportPowerStatus)};
            yield return new TestCaseData(CecMessageBuilder.ActiveSource(LogicalAddress.Unregistered, PhysicalAddress.Parse("2.0.0.0")), "FF:82:20:00") { TestName = nameof(CecMessageBuilder.ActiveSource) };
            yield return new TestCaseData(CecMessageBuilder.CecVersion(LogicalAddress.Unregistered, LogicalAddress.Unregistered, CecVersion.Version13A), "FF:9E:04") { TestName = nameof(CecMessageBuilder.CecVersion) };
            yield return new TestCaseData(CecMessageBuilder.SetSystemAudioMode(LogicalAddress.Unregistered, LogicalAddress.Unregistered, SystemAudioStatus.Off), "FF:72:00") { TestName = nameof(CecMessageBuilder.SetSystemAudioMode) };
            yield return new TestCaseData(CecMessageBuilder.SetSystemAudioMode(LogicalAddress.Unregistered, LogicalAddress.Unregistered, SystemAudioStatus.On), "FF:72:01") { TestName = nameof(CecMessageBuilder.SetSystemAudioMode) };
            yield return new TestCaseData(CecMessageBuilder.ReportAudioStatus(LogicalAddress.Unregistered, LogicalAddress.Unregistered, AudioMuteStatus.AudioMuteOff, 127), "FF:7A:7F") { TestName = nameof(CecMessageBuilder.ReportAudioStatus) };
            yield return new TestCaseData(CecMessageBuilder.ReportAudioStatus(LogicalAddress.Unregistered, LogicalAddress.Unregistered, AudioMuteStatus.AudioMuteOn, 3), "FF:7A:83") { TestName = nameof(CecMessageBuilder.ReportAudioStatus) };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}