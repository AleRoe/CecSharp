﻿using System.Collections.Generic;
using AleRoe.CecSharp.Extensions;
using AleRoe.CecSharp.Model;
using NUnit.Framework;

namespace AleRoe.CecSharp.Tests
{
    [TestFixture()]
    public class CecMessageBuilderTests
    {
        [TestCaseSource(typeof(CecMessageBuilderTests), nameof(CecMessageBuilderTests.BuilderTestData))]
        public string BuilderTest(CecMessage message)
        {
            //Assert.That(message.ToCec(), Is.EqualTo(expected));
            return message.ToCec();
        }

        //[Test]
        //[TestCaseSource(typeof(TestData), nameof(TestData.DevicePowerStatusFeatures), Category = "DevicePowerStatusFeatures")]
        //[TestCaseSource(typeof(TestData), nameof(TestData.DeviceOSDNameTransferFeatures), Category = "DeviceOSDNameTransferFeatures")]
        //public string BuilderTests(CecMessage message)
        //{
        //    return message.ToCec();
        //}


        private static IEnumerable<TestCaseData> BuilderTestData
        {
            get
            {
                yield return new TestCaseData(CecMessageBuilder.ReportPowerStatus(LogicalAddress.Unregistered, LogicalAddress.Unregistered, PowerStatus.On)).Returns("FF:90:00").SetName(nameof(CecMessageBuilder.ReportPowerStatus));
                yield return new TestCaseData(CecMessageBuilder.CecVersion(LogicalAddress.Unregistered, LogicalAddress.Unregistered, CecVersion.Version13A)).Returns("FF:9E:04").SetName(nameof(CecMessageBuilder.CecVersion)) ;
                yield return new TestCaseData(CecMessageBuilder.SetSystemAudioMode(LogicalAddress.Unregistered, LogicalAddress.Unregistered, SystemAudioStatus.Off)).Returns("FF:72:00").SetName(nameof(CecMessageBuilder.SetSystemAudioMode));
                //yield return new TestCaseData(CecMessageBuilder.SetSystemAudioMode(LogicalAddress.Unregistered, LogicalAddress.Unregistered, SystemAudioStatus.On), "FF:72:01") { TestName = nameof(CecMessageBuilder.SetSystemAudioMode) };
                //yield return new TestCaseData(CecMessageBuilder.ReportAudioStatus(LogicalAddress.Unregistered, LogicalAddress.Unregistered, AudioMuteStatus.AudioMuteOff, 127), "FF:7A:7F") { TestName = nameof(CecMessageBuilder.ReportAudioStatus) };
                //yield return new TestCaseData(CecMessageBuilder.ReportAudioStatus(LogicalAddress.Unregistered, LogicalAddress.Unregistered, AudioMuteStatus.AudioMuteOn, 3), "FF:7A:83") { TestName = nameof(CecMessageBuilder.ReportAudioStatus) };
                //yield return new TestCaseData(CecMessageBuilder.SetMenuLanguage(LogicalAddress.TV, "heb"), "0F:32:68:65:62") { TestName = nameof(CecMessageBuilder.SetMenuLanguage) };
                //yield return new TestCaseData(CecMessageBuilder.GetMenuLanguage(LogicalAddress.TV, LogicalAddress.Tuner3), "07:91") { TestName = nameof(CecMessageBuilder.GetMenuLanguage) };
                //yield return new TestCaseData(CecMessageBuilder.FeatureAbort(LogicalAddress.Tuner3, LogicalAddress.TV, Command.GetMenuLanguage, AbortReason.UnrecognizedOpcode), "70:00:91:00") { TestName = nameof(CecMessageBuilder.FeatureAbort) };
                //yield return new TestCaseData(CecMessageBuilder.ReportPhysicalAddress(LogicalAddress.Tuner3, DeviceType.PlaybackDevice, PhysicalAddress.Parse("2.0.0.0")), "7F:84:20:00:04") { TestName = nameof(CecMessageBuilder.ReportPhysicalAddress) };
                //yield return new TestCaseData(CecMessageBuilder.SetOSDString(LogicalAddress.Tuner3, DisplayControl.DisplayForDefaultTime, "Hello"), "70:64:00:48:65:6C:6C:6F") { TestName = nameof(CecMessageBuilder.SetOSDString) };
                //yield return new TestCaseData(CecMessageBuilder.SetOSDString(LogicalAddress.Tuner3, DisplayControl.DisplayUntilCleared, "Hello"), "70:64:40:48:65:6C:6C:6F") { TestName = nameof(CecMessageBuilder.SetOSDString) };
                //yield return new TestCaseData(CecMessageBuilder.SetOSDString(LogicalAddress.Tuner3, DisplayControl.ClearPreviousMessage, "Hello"), "70:64:80:48:65:6C:6C:6F") { TestName = nameof(CecMessageBuilder.SetOSDString) };
                //yield return new TestCaseData(CecMessageBuilder.SetOSDString(LogicalAddress.Tuner3, DisplayControl.ReservedForFutureUse, "Hello"), "70:64:C0:48:65:6C:6C:6F") { TestName = nameof(CecMessageBuilder.SetOSDString) };
                //yield return new TestCaseData(CecMessageBuilder.GiveOsdName(LogicalAddress.TV, LogicalAddress.Tuner3), "07:46") { TestName = nameof(CecMessageBuilder.GiveOsdName) };
                //yield return new TestCaseData(CecMessageBuilder.SetOsdName(LogicalAddress.Tuner3, LogicalAddress.TV, "MyName"), "70:47:4D:79:4E:61:6D:65") { TestName = nameof(CecMessageBuilder.SetOsdName) };
                //yield return new TestCaseData(CecMessageBuilder.ActiveSource(LogicalAddress.Unregistered, PhysicalAddress.Parse("2.0.0.0")), "FF:82:20:00") { TestName = nameof(CecMessageBuilder.ActiveSource) };
                //yield return new TestCaseData(CecMessageBuilder.InactiveSource(LogicalAddress.Tuner3, PhysicalAddress.Parse("2.0.0.0")), "70:9D:20:00") { TestName = nameof(CecMessageBuilder.InactiveSource) };
                //yield return new TestCaseData(CecMessageBuilder.RequestActiveSource(LogicalAddress.Tuner3), "7F:85") { TestName = nameof(CecMessageBuilder.RequestActiveSource) };
                //yield return new TestCaseData(CecMessageBuilder.SetStreamPath(PhysicalAddress.Parse("2.0.0.0")), "0F:86:20:00") { TestName = nameof(CecMessageBuilder.SetStreamPath) };
                //yield return new TestCaseData(CecMessageBuilder.RoutingChange(LogicalAddress.Reserved1, PhysicalAddress.Parse("2.0.0.0"), PhysicalAddress.Parse("2.1.0.0")), "CF:80:20:00:21:00") { TestName = nameof(CecMessageBuilder.RoutingChange) };
                //yield return new TestCaseData(CecMessageBuilder.RoutingInformation(LogicalAddress.Reserved1, PhysicalAddress.Parse("2.0.0.0")), "CF:81:20:00") { TestName = nameof(CecMessageBuilder.RoutingInformation) };
                //yield return new TestCaseData(CecMessageBuilder.GiveDevicePowerStatus(LogicalAddress.TV, LogicalAddress.PlaybackDevice1), "04:8F").SetCategory(Features.DevicePowerStatus.ToString()).SetName(nameof(CecMessageBuilder.GiveDevicePowerStatus));
                //yield return new TestCaseData(CecMessageBuilder.ReportPowerStatus(LogicalAddress.PlaybackDevice1, LogicalAddress.TV, PowerStatus.On), "40:90:00").SetCategory(Features.DevicePowerStatus.ToString()).SetName(nameof(CecMessageBuilder.ReportPowerStatus));
                //yield return new TestCaseData(CecMessageBuilder.DeviceVendorId(LogicalAddress.PlaybackDevice1, 999), "4F:87:00:03:E7").SetCategory(Features.VendorSpecificCommand.ToString()).SetName(nameof(CecMessageBuilder.DeviceVendorId));
                //yield return new TestCaseData(CecMessageBuilder.GiveDeviceVendorId(LogicalAddress.TV, LogicalAddress.PlaybackDevice3), "0B:8C").SetCategory(Features.VendorSpecificCommand.ToString()).SetName(nameof(CecMessageBuilder.GiveDeviceVendorId));
                //yield return new TestCaseData(CecMessageBuilder.VendorCommand(LogicalAddress.TV, LogicalAddress.PlaybackDevice3, "Hello"), "0B:89:48:65:6C:6C:6F").SetCategory(Features.VendorSpecificCommand.ToString()).SetName(nameof(CecMessageBuilder.VendorCommand));
                //yield return new TestCaseData(CecMessageBuilder.VendorCommandWithId(LogicalAddress.TV, LogicalAddress.PlaybackDevice3, 999, "Hello"), "0B:A0:00:03:E7:48:65:6C:6C:6F").SetCategory(Features.VendorSpecificCommand.ToString()).SetName(nameof(CecMessageBuilder.VendorCommandWithId));
                //yield return new TestCaseData(CecMessageBuilder.VendorRemoteButtonDown(LogicalAddress.TV, LogicalAddress.PlaybackDevice3, "Hello"), "0B:8A:48:65:6C:6C:6F").SetCategory(Features.VendorSpecificCommand.ToString()).SetName(nameof(CecMessageBuilder.VendorRemoteButtonDown));
                //yield return new TestCaseData(CecMessageBuilder.Standby(LogicalAddress.TV, LogicalAddress.PlaybackDevice3), "0B:36").SetCategory(Features.SystemStandby.ToString()).SetName(nameof(CecMessageBuilder.Standby));
                //yield return new TestCaseData(CecMessageBuilder.UserControlPressed(LogicalAddress.TV, LogicalAddress.PlaybackDevice3, UiCommand.RootMenu), "0B:44:09").SetCategory(Features.DeviceMenuControl.ToString()).SetName(nameof(CecMessageBuilder.UserControlPressed));
                //yield return new TestCaseData(CecMessageBuilder.UserControlReleased(LogicalAddress.TV, LogicalAddress.PlaybackDevice3), "0B:45").SetCategory(Features.DeviceMenuControl.ToString()).SetName(nameof(CecMessageBuilder.UserControlReleased));
                //yield return new TestCaseData(CecMessageBuilder.MenuRequest(LogicalAddress.PlaybackDevice3, MenuRequestType.Query), "0B:8D:02").SetCategory(Features.DeviceMenuControl.ToString()).SetName(nameof(CecMessageBuilder.MenuRequest));
                //yield return new TestCaseData(CecMessageBuilder.MenuStatus(LogicalAddress.PlaybackDevice3, MenuState.Deactivated), "B0:8E:01").SetCategory(Features.DeviceMenuControl.ToString()).SetName(nameof(CecMessageBuilder.MenuStatus));
            }
        }
    }

    class TestData
    {
        public static IEnumerable<TestCaseData> DevicePowerStatusFeatures
        {
            get
            {
                yield return new TestCaseData(CecMessageBuilder.GiveDevicePowerStatus(LogicalAddress.TV, LogicalAddress.PlaybackDevice1)).Returns("04:8F").SetName(nameof(CecMessageBuilder.GiveDevicePowerStatus));
                yield return new TestCaseData(CecMessageBuilder.ReportPowerStatus(LogicalAddress.PlaybackDevice1, LogicalAddress.TV, PowerStatus.On)).Returns("40:90:00").SetName(nameof(CecMessageBuilder.ReportPowerStatus));
            }
        }

        public static IEnumerable<TestCaseData> DeviceOSDNameTransferFeatures
        {
            get
            {
                yield return new TestCaseData(CecMessageBuilder.GiveOsdName(LogicalAddress.TV, LogicalAddress.Tuner3)).Returns("07:46");
                yield return new TestCaseData(CecMessageBuilder.SetOsdName(LogicalAddress.Tuner3, LogicalAddress.TV, "MyName")).Returns("70:47:4D:79:4E:61:6D:65").SetName(nameof(CecMessageBuilder.SetOsdName));
            }
        }
    }
}
