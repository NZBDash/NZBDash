#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: CpuMonitorTests.cs
//   Created By: Jamie Rees
//  
//   Permission is hereby granted, free of charge, to any person obtaining
//   a copy of this software and associated documentation files (the
//   "Software"), to deal in the Software without restriction, including
//   without limitation the rights to use, copy, modify, merge, publish,
//   distribute, sublicense, and/or sell copies of the Software, and to
//   permit persons to whom the Software is furnished to do so, subject to
//   the following conditions:
//  
//   The above copyright notice and this permission notice shall be
//   included in all copies or substantial portions of the Software.
//  
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//   EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//   MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//   NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//   LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//   OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//   WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ************************************************************************/
#endregion
using System;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;

using Moq;

using NUnit.Framework;

using NZBDash.Common.Interfaces;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Models;
using NZBDash.Core.Models.Settings;
using NZBDash.Services.HardwareMonitor.Interfaces;
using NZBDash.Services.HardwareMonitor.Notification;

using Ploeh.AutoFixture;

namespace NZBDash.Services.HardwareMonitor.Tests
{

    [TestFixture]
    public class EmailNotifierTests
    {

        private Mock<ISmtpClient> Smtp { get; set; }
        private Mock<IEventService> EventService { get; set; }

        [SetUp]
        public void Setup()
        {
            Smtp = new Mock<ISmtpClient>();
            EventService = new Mock<IEventService>();
            EventService.Setup(x => x.RecordEvent(It.IsAny<MonitoringEventsDto>())).Returns(1);
        }

        [Test]
        public void TestSendStartNotification()
        {
            var fileMock = new Mock<IFile>();
            fileMock.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns("abc");
            var n = new EmailNotifier(new TimeSpan(0, 0, 0, 1), EventService.Object, Smtp.Object, fileMock.Object)
            {
                CpuSettings = new CpuMonitoringDto { CpuPercentageLimit = 1, Enabled = true, ThresholdTime = 1 },
                EmailSettings = new EmailAlertSettingsDto { AlertOnBreach = true, RecipientAddress = "ABC@ABC.com", EmailHost = "Host123", EmailPort = 99, EmailUsername = "Username", EmailPassword = "password"}
            };
            n.Notify(true);

            Assert.That(n.EndEventSaved, Is.False,"The values never got reset, this means that we didn't correctly end the notification");
            Assert.That(n.StartEventSaved, Is.True, "We never saved the start notification event");
            EventService.Verify(x => x.RecordEvent(It.IsAny<MonitoringEventsDto>()), Times.Once);
            Smtp.Verify(x => x.Send(n.EmailSettings.EmailHost, n.EmailSettings.EmailPort, It.IsAny<MailMessage>(), It.IsAny<NetworkCredential>()), Times.Once);
        }

        [Test]
        public void TestSendEndNotification()
        {
            var fileMock = new Mock<IFile>();
            fileMock.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns("abc");
            var n = new EmailNotifier(new TimeSpan(0, 0, 0, 1), EventService.Object, Smtp.Object, fileMock.Object)
            {
                CpuSettings = new CpuMonitoringDto { CpuPercentageLimit = 1, Enabled = true, ThresholdTime = 1 },
                EmailSettings = new EmailAlertSettingsDto { AlertOnBreach = true,AlertOnBreachEnd = true,RecipientAddress = "ABC@ABC.com", EmailHost = "Host123", EmailPort = 99, EmailUsername = "Username", EmailPassword = "password" }
            };
            n.Notify(true);
            EventService.Verify(x => x.RecordEvent(It.IsAny<MonitoringEventsDto>()), Times.Once);

            n.Notify(false);
            EventService.Verify(x => x.RecordEvent(It.IsAny<MonitoringEventsDto>()), Times.Exactly(2));

            // Values get reset
            Assert.That(n.EndEventSaved, Is.False, "The values never got reset, this means that we didn't correctly end the notification");
            Assert.That(n.StartEventSaved, Is.False, "The values never got reset, this means that we didn't correctly end the notification");


            Smtp.Verify(x => x.Send(n.EmailSettings.EmailHost, n.EmailSettings.EmailPort, It.IsAny<MailMessage>(), It.IsAny<NetworkCredential>()), Times.Exactly(2));
        }

        [Test]
        public void TestSendStartNotificationWithOutEmail()
        {
            var fileMock = new Mock<IFile>();
            fileMock.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns("abc");
            var n = new EmailNotifier(new TimeSpan(0, 0, 0, 1), EventService.Object, Smtp.Object, fileMock.Object)
            {
                CpuSettings = new CpuMonitoringDto { CpuPercentageLimit = 1, Enabled = true, ThresholdTime = 1 },
                EmailSettings = new EmailAlertSettingsDto { AlertOnBreach = false, RecipientAddress = "ABC@ABC.com", EmailHost = "Host123", EmailPort = 99, EmailUsername = "Username", EmailPassword = "password" }
            };
            n.Notify(true);

            Assert.That(n.EndEventSaved, Is.False, "The end event should never get saved.");
            Assert.That(n.StartEventSaved, Is.True, "We never saved the start notification event");
            EventService.Verify(x => x.RecordEvent(It.IsAny<MonitoringEventsDto>()), Times.Once);
            Smtp.Verify(x => x.Send(n.EmailSettings.EmailHost, n.EmailSettings.EmailPort, It.IsAny<MailMessage>(), It.IsAny<NetworkCredential>()), Times.Never);
        }

        [Test]
        public void TestSendEndNotificationWithoutEmail()
        {
            var fileMock = new Mock<IFile>();
            fileMock.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns("abc");
            var n = new EmailNotifier(new TimeSpan(0, 0, 0, 1), EventService.Object, Smtp.Object, fileMock.Object)
            {
                CpuSettings = new CpuMonitoringDto { CpuPercentageLimit = 1, Enabled = true, ThresholdTime = 1 },
                EmailSettings = new EmailAlertSettingsDto { AlertOnBreach = false, AlertOnBreachEnd = false, RecipientAddress = "ABC@ABC.com", EmailHost = "Host123", EmailPort = 99, EmailUsername = "Username", EmailPassword = "password" }
            };
            n.Notify(true);
            EventService.Verify(x => x.RecordEvent(It.IsAny<MonitoringEventsDto>()), Times.Once);

            n.Notify(false);
            EventService.Verify(x => x.RecordEvent(It.IsAny<MonitoringEventsDto>()), Times.Exactly(2));

            // Values get reset
            Assert.That(n.EndEventSaved, Is.False, "The values never got reset, this means that we didn't correctly end the notification");
            Assert.That(n.StartEventSaved, Is.False, "The values never got reset, this means that we didn't correctly end the notification");


            Smtp.Verify(x => x.Send(n.EmailSettings.EmailHost, n.EmailSettings.EmailPort, It.IsAny<MailMessage>(), It.IsAny<NetworkCredential>()), Times.Never);
        }

        [Test]
        public void TestNotificationDisabled()
        {
            var fileMock = new Mock<IFile>();
            var n = new EmailNotifier(new TimeSpan(0, 0, 0, 1), EventService.Object, Smtp.Object, fileMock.Object)
            {
                CpuSettings = new CpuMonitoringDto { CpuPercentageLimit = 1, Enabled = false, ThresholdTime = 1 },
                EmailSettings = new EmailAlertSettingsDto { AlertOnBreach = false, AlertOnBreachEnd = false, RecipientAddress = "ABC@ABC.com", EmailHost = "Host123", EmailPort = 99, EmailUsername = "Username", EmailPassword = "password" }
            };
            n.Notify(true);
            EventService.Verify(x => x.RecordEvent(It.IsAny<MonitoringEventsDto>()), Times.Never);

            // Values never get set
            Assert.That(n.EndEventSaved, Is.False, "The values have been set, they should not be set as notifier should be disabled");
            Assert.That(n.StartEventSaved, Is.False, "The values have been set, they should not be set as notifier should be disabled");

            Smtp.Verify(x => x.Send(n.EmailSettings.EmailHost, n.EmailSettings.EmailPort, It.IsAny<MailMessage>(), It.IsAny<NetworkCredential>()), Times.Never);
        }

        [Test]
        public void TestNotificationStartEventNotSavedButAttemptEndEvent()
        {
            var fileMock = new Mock<IFile>();
            var n = new EmailNotifier(new TimeSpan(0, 0, 0, 1), EventService.Object, Smtp.Object, fileMock.Object)
            {
                CpuSettings = new CpuMonitoringDto { CpuPercentageLimit = 1, Enabled = true, ThresholdTime = 1 },
                EmailSettings = new EmailAlertSettingsDto { AlertOnBreach = false, AlertOnBreachEnd = true, RecipientAddress = "ABC@ABC.com", EmailHost = "Host123", EmailPort = 99, EmailUsername = "Username", EmailPassword = "password" }
            };
            n.Notify(true);
            EventService.Verify(x => x.RecordEvent(It.IsAny<MonitoringEventsDto>()), Times.Once);
            n.StartEventSaved = false;
            n.Notify(false);
            // Values never get set
            Assert.That(n.EndEventSaved, Is.False, "The values have been set, they should not be set as notifier should be disabled");

            Smtp.Verify(x => x.Send(n.EmailSettings.EmailHost, n.EmailSettings.EmailPort, It.IsAny<MailMessage>(), It.IsAny<NetworkCredential>()), Times.Never);
        }

        [Test]
        public void TestNotificationStartEventSavedButAttemptStartEvent()
        {
            var fileMock = new Mock<IFile>();
            var n = new EmailNotifier(new TimeSpan(0, 0, 0, 1), EventService.Object, Smtp.Object, fileMock.Object)
            {
                CpuSettings = new CpuMonitoringDto { CpuPercentageLimit = 1, Enabled = true, ThresholdTime = 1 },
                EmailSettings = new EmailAlertSettingsDto { AlertOnBreach = true, AlertOnBreachEnd = false, RecipientAddress = "ABC@ABC.com", EmailHost = "Host123", EmailPort = 99, EmailUsername = "Username", EmailPassword = "password" }
            };
            n.StartEventSaved = true;
            n.Notify(true);
            EventService.Verify(x => x.RecordEvent(It.IsAny<MonitoringEventsDto>()), Times.Never);
            // Values never get set
            Assert.That(n.EndEventSaved, Is.False, "The values have been set, they should not be set as notifier should be disabled");

            Smtp.Verify(x => x.Send(n.EmailSettings.EmailHost, n.EmailSettings.EmailPort, It.IsAny<MailMessage>(), It.IsAny<NetworkCredential>()), Times.Never);
        }
    }
}
