using System;
using System.Globalization;
using AleRoe.CecSharp.Model;
using NUnit.Framework;

namespace AleRoe.CecSharp.Tests
{
    [TestFixture()]
    public class CecDeviceTests
    {
        [Test()]
        public void CtorTest_0()
        {
            var device = new CecDevice(DeviceType.PlaybackDevice, "SomeName", 999);
            Assert.AreEqual(DeviceType.PlaybackDevice, device.DeviceType);
            Assert.AreEqual("SomeName", device.OsdName);
            Assert.AreEqual(999, device.VendorId);
            Assert.AreEqual(PhysicalAddress.None, device.PhysicalAddress);
            Assert.AreEqual(LogicalAddress.Unregistered, device.LogicalAddress);
        }

        [Test()]
        public void CtorTest_1()
        {
            var device = new CecDevice(DeviceType.PlaybackDevice, "SomeName", 999,PhysicalAddress.Parse("2.1.0.0"));
            Assert.AreEqual(DeviceType.PlaybackDevice, device.DeviceType);
            Assert.AreEqual("SomeName", device.OsdName);
            Assert.AreEqual(999, device.VendorId);
            Assert.AreEqual(PhysicalAddress.Parse("2.1.0.0"), device.PhysicalAddress);
            Assert.AreEqual(LogicalAddress.Unregistered, device.LogicalAddress);
        }

        [Test()]
        public void CtorTest_2()
        {
            var device = new CecDevice(DeviceType.PlaybackDevice, "SomeName", 999, PhysicalAddress.Parse("2.1.0.0"), LogicalAddress.PlaybackDevice1);
            Assert.AreEqual(DeviceType.PlaybackDevice, device.DeviceType);
            Assert.AreEqual("SomeName", device.OsdName);
            Assert.AreEqual(999, device.VendorId);
            Assert.AreEqual(PhysicalAddress.Parse("2.1.0.0"), device.PhysicalAddress);
            Assert.AreEqual(LogicalAddress.PlaybackDevice1, device.LogicalAddress);
        }

        [Test()]
        public void CtorTest_DeviceTypeMismatchThrows()
        {
            Assert.Throws<InvalidOperationException>(() => _ = new CecDevice(DeviceType.PlaybackDevice, "SomeName", 999,
                PhysicalAddress.Parse("2.1.0.0"), LogicalAddress.Tuner3));
        }

        [Test()]
        public void CtorTest_OSDNameTooLongThrows()
        {
            Assert.Throws<ArgumentException>(() => _ = new CecDevice(DeviceType.PlaybackDevice, "SomeVeryLongName", 999,
                PhysicalAddress.Parse("2.1.0.0"), LogicalAddress.Tuner3));
        }

        [Test()]
        public void CtorTest_OSDNameEmptyThrows()
        {
            Assert.Throws<ArgumentException>(() => _ = new CecDevice(DeviceType.PlaybackDevice, "", 999,
                PhysicalAddress.Parse("2.1.0.0"), LogicalAddress.Tuner3));
        }

        [Test()]
        public void ProcessCecMessageTest_SetMenuLanguage()
        {
            var device = CreatePlaybackDevice();
            Assert.AreEqual(CultureInfo.CurrentUICulture.ThreeLetterISOLanguageName, device.Language);

            var message = CecMessageBuilder.SetMenuLanguage(LogicalAddress.TV, "heb");
            var response = device.ProcessCecMessage(message);

            Assert.AreEqual(CecMessage.None, response);
            Assert.AreEqual("heb", device.Language);
        }

        [Test()]
        public void ProcessCecMessageTest_SetOSDString()
        {
            var device = CreateTVDevice();
            var message = CecMessageBuilder.SetOSDString(LogicalAddress.Tuner1, DisplayControl.DisplayForDefaultTime,"Hello");
            var response = device.ProcessCecMessage(message);
            Assert.AreEqual(CecMessage.None, response);
        }


        [Test()]
        public void ProcessCecMessageTest_GetMenuLanguage_PlaybackDevice()
        {
            var device = CreatePlaybackDevice();
            var message = CecMessageBuilder.GetMenuLanguage(LogicalAddress.TV, device.LogicalAddress);
            var response = device.ProcessCecMessage(message);

            var expected = CecMessageBuilder.FeatureAbort(device.LogicalAddress, message.Source,
                Command.GetMenuLanguage, AbortReason.UnrecognizedOpcode);

            Assert.AreEqual(expected, response);
        }

        [Test()]
        public void ProcessCecMessageTest_GetMenuLanguage_Broadcast()
        {
            var device = CreatePlaybackDevice();
            var message = CecMessageBuilder.GetMenuLanguage(LogicalAddress.TV, LogicalAddress.Unregistered);
            var response = device.ProcessCecMessage(message);

            Assert.AreEqual(Command.None, response.Command);
        }

        [Test()]
        public void ProcessCecMessageTest_GetMenuLanguage_TV()
        {
            var device = CreateTVDevice();
            var message = CecMessageBuilder.GetMenuLanguage(LogicalAddress.Tuner3, device.LogicalAddress);
            var response = device.ProcessCecMessage(message);
            
            var expected = CecMessageBuilder.SetMenuLanguage(device.LogicalAddress, device.Language);
            Assert.AreEqual(expected, response);
        }

        [Test()]
        public void ProcessCecMessageTest_GetMenuLanguage_FreeUseTV()
        {
            var device = CreateTVDevice();
            var message = CecMessageBuilder.GetMenuLanguage(LogicalAddress.Tuner3, device.LogicalAddress);
            var response = device.ProcessCecMessage(message);

            var expected = CecMessageBuilder.SetMenuLanguage(device.LogicalAddress, device.Language);
            Assert.AreEqual(expected, response);
        }

        [Test()]
        public void ProcessCecMessageTest_GivePhysicalAddress()
        {
            var device = CreatePlaybackDevice();
            var message = CecMessageBuilder.GivePhysicalAddress(LogicalAddress.TV, device.LogicalAddress);
            var response = device.ProcessCecMessage(message);

            var expected = CecMessageBuilder.ReportPhysicalAddress(device.LogicalAddress, device.DeviceType, device.PhysicalAddress);
            Assert.AreEqual(expected, response);
        }

        [Test()]
        public void ProcessCecMessageTest_GiveOSDName()
        {
            var device = CreatePlaybackDevice();
            var message = CecMessageBuilder.GiveOsdName(LogicalAddress.TV, device.LogicalAddress);
            var response = device.ProcessCecMessage(message);

            var expected = CecMessageBuilder.SetOsdName(device.LogicalAddress, message.Source, device.OsdName);
            Assert.AreEqual(expected, response);
        }

        [Test()]
        public void ProcessCecMessageTest_SetOSDName()
        {
            var device = CreateTVDevice();

            var message = CecMessageBuilder.SetOsdName(LogicalAddress.Tuner3, device.LogicalAddress, "Hello");
            var response = device.ProcessCecMessage(message);
            Assert.AreEqual(CecMessage.None, response);
        }

        [Test()]
        public void ProcessCecMessageTest_RequestActiveSource()
        {
            var device = CreatePlaybackDevice();
            var message = CecMessageBuilder.SetOsdName(LogicalAddress.Tuner3, device.LogicalAddress, "Hello");
            var response = device.ProcessCecMessage(message);
            Assert.AreEqual(CecMessage.None, response);
        }

        [Test()]
        public void ProcessCecMessageTest_SetStreamPath()
        {
            var device = CreatePlaybackDevice();
            var message = CecMessageBuilder.SetStreamPath(device.PhysicalAddress);
            var response = device.ProcessCecMessage(message);
            var expected = CecMessageBuilder.ActiveSource(device.LogicalAddress, device.PhysicalAddress);
            Assert.AreEqual(expected, response);
        }

        [Test()]
        public void ProcessCecMessageTest_RoutingChange()
        {
            var device = CreatePlaybackDevice();
            var message = CecMessageBuilder.RoutingChange(LogicalAddress.Reserved1, PhysicalAddress.Parse("3.0.0.0"),
                device.PhysicalAddress);
            var response = device.ProcessCecMessage(message);
            Assert.AreEqual(CecMessage.None, response);
            Assert.IsTrue(device.IsActiveSource);
        }

        [Test()]
        public void ProcessCecMessageTest_RoutingInformationThrows()
        {
            var device = CreatePlaybackDevice();
            var message = CecMessageBuilder.RoutingInformation(LogicalAddress.Reserved1, PhysicalAddress.Parse("3.0.0.0"));
            Assert.Throws<NotSupportedException>(() => _ = device.ProcessCecMessage(message));
        }

        [Test()]
        public void ProcessCecMessageTest_GiveDevicePowerStatus()
        {
            var device = CreatePlaybackDevice();
            var message = CecMessageBuilder.GiveDevicePowerStatus(LogicalAddress.TV, device.LogicalAddress);
            var response = device.ProcessCecMessage(message);
            var expected = CecMessageBuilder.ReportPowerStatus(device.LogicalAddress, message.Source, device.PowerStatus);
            Assert.AreEqual(expected, response);
        }

        [Test()]
        public void ProcessCecMessageTest_DeviceVendorId()
        {
            var device = CreatePlaybackDevice();
            var message = CecMessageBuilder.DeviceVendorId(LogicalAddress.TV, 999);
            var response = device.ProcessCecMessage(message);
            Assert.AreEqual(CecMessage.None, response);
        }

        [Test()]
        public void ProcessCecMessageTest_GiveDeviceVendorId()
        {
            var device = CreatePlaybackDevice();
            var message = CecMessageBuilder.GiveDeviceVendorId(LogicalAddress.TV, device.LogicalAddress);
            var response = device.ProcessCecMessage(message);
            var expected = CecMessageBuilder.DeviceVendorId(device.LogicalAddress, device.VendorId);
            Assert.AreEqual(expected, response);
        }

        [Test()]
        public void ProcessCecMessageTest_VendorCommand()
        {
            var device = CreatePlaybackDevice();
            var message = CecMessageBuilder.VendorCommand(LogicalAddress.TV, device.LogicalAddress,"Hello");
            var response = device.ProcessCecMessage(message);
            Assert.AreEqual(CecMessage.None, response);
        }

        [Test()]
        public void ProcessCecMessageTest_VendorCommandWithId()
        {
            var device = CreatePlaybackDevice();
            var message = CecMessageBuilder.VendorCommandWithId(LogicalAddress.TV, device.LogicalAddress, 999,"Hello");
            var response = device.ProcessCecMessage(message);
            Assert.AreEqual(CecMessage.None, response);
        }

        [Test()]
        public void ProcessCecMessageTest_VendorRemoteButtonDown()
        {
            var device = CreatePlaybackDevice();
            var message = CecMessageBuilder.VendorRemoteButtonDown(LogicalAddress.TV, device.LogicalAddress, "Hello");
            var response = device.ProcessCecMessage(message);
            Assert.AreEqual(CecMessage.None, response);
        }

        [Test()]
        public void ProcessCecMessageTest_VendorRemoteButtonUp()
        {
            var device = CreatePlaybackDevice();
            var message = CecMessageBuilder.VendorRemoteButtonUp(LogicalAddress.TV, device.LogicalAddress);
            var response = device.ProcessCecMessage(message);
            Assert.AreEqual(CecMessage.None, response);
        }

        [Test()]
        public void ProcessCecMessageTest_DirectlyAddressedStandby()
        {
            var device = CreatePlaybackDevice();
            var message = CecMessageBuilder.Standby(LogicalAddress.TV, device.LogicalAddress);
            var response = device.ProcessCecMessage(message);
            Assert.AreEqual(CecMessage.None, response);
            Assert.AreEqual(PowerStatus.Standby, device.PowerStatus);
        }

        [Test()]
        public void ProcessCecMessageTest_BroadcastStandby()
        {
            var device = CreatePlaybackDevice();
            var message = CecMessageBuilder.Standby(LogicalAddress.TV, LogicalAddress.Unregistered);
            var response = device.ProcessCecMessage(message);
            Assert.AreEqual(CecMessage.None, response);
            Assert.AreEqual(PowerStatus.Standby, device.PowerStatus);
        }

        [Test()]
        public void ProcessCecMessageTest_UserControlPressed()
        {
            var device = CreatePlaybackDevice();
            var message = CecMessageBuilder.UserControlPressed(LogicalAddress.TV, device.LogicalAddress, UiCommand.RootMenu);
            var response = device.ProcessCecMessage(message);
            Assert.AreEqual(CecMessage.None, response);
        }

        [Test()]
        public void ProcessCecMessageTest_UserControlReleased()
        {
            var device = CreatePlaybackDevice();
            var message = CecMessageBuilder.UserControlReleased(LogicalAddress.TV, device.LogicalAddress);
            var response = device.ProcessCecMessage(message);
            Assert.AreEqual(CecMessage.None, response);
        }

        [Test()]
        public void ProcessCecMessageTest_MenuRequestActivate()
        {
            var device = CreatePlaybackDevice();
            var message = CecMessageBuilder.MenuRequest(device.LogicalAddress, MenuRequestType.Activate);
            var response = device.ProcessCecMessage(message);
            var expected = CecMessageBuilder.MenuStatus(device.LogicalAddress, MenuState.Activated);
            Assert.AreEqual(expected, response);
            Assert.AreEqual(MenuState.Activated, device.MenuState);
        }

        [Test()]
        public void ProcessCecMessageTest_MenuRequestDeactivate()
        {
            var device = CreatePlaybackDevice();
            var message = CecMessageBuilder.MenuRequest(device.LogicalAddress, MenuRequestType.Deactivate);
            var response = device.ProcessCecMessage(message);
            var expected = CecMessageBuilder.MenuStatus(device.LogicalAddress, MenuState.Deactivated);
            Assert.AreEqual(expected, response);
            Assert.AreEqual(MenuState.Deactivated, device.MenuState);
        }

        [Test()]
        public void ProcessCecMessageTest_MenuRequestQuery()
        {
            var device = CreatePlaybackDevice();
            device.MenuState = MenuState.Activated;
            var message = CecMessageBuilder.MenuRequest(device.LogicalAddress, MenuRequestType.Query);
            var response = device.ProcessCecMessage(message);
            var expected = CecMessageBuilder.MenuStatus(device.LogicalAddress, MenuState.Activated);
            Assert.AreEqual(expected, response);
        }

        private static CecDevice CreatePlaybackDevice()
        {
            return new CecDevice(DeviceType.PlaybackDevice, "PlaybackDevice", 999, PhysicalAddress.Parse("2.0.0.0"), LogicalAddress.PlaybackDevice1);
        }

        private static CecDevice CreateTVDevice()
        {
            return new CecDevice(DeviceType.TV, "TVDevice", 999, PhysicalAddress.Parse("1.0.0.0"), LogicalAddress.TV);
        }
        
    }
}