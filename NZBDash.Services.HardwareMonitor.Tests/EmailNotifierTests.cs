#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: EmailNotifierTests.cs
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
using System.Net;
using System.Net.Mail;

using Moq;

using NUnit.Framework;

using NZBDash.Common.Interfaces;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Models;
using NZBDash.Core.Models.Settings;
using NZBDash.Services.HardwareMonitor.Interfaces;
using NZBDash.Services.Monitor.Notification;

namespace NZBDash.Services.HardwareMonitor.Tests
{

    [TestFixture]
    public class EmailNotifierTests
    {

        private Mock<ISmtpClient> Smtp { get; set; }
        private Mock<IEventService> EventService { get; set; }
        private CpuNotifier N { get; set; }

        [SetUp]
        public void Setup()
        {
            Smtp = new Mock<ISmtpClient>();
            EventService = new Mock<IEventService>();
            var logger = new Mock<ILogger>();

            EventService.Setup(x => x.RecordEvent(It.IsAny<MonitoringEventsDto>())).Returns(1);
            var fileMock = new Mock<IFile>();
            fileMock.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns("<html>\\r\\n</html>");

            N = new CpuNotifier(new TimeSpan(0, 0, 0, 1), EventService.Object, Smtp.Object, fileMock.Object, logger.Object);
        }

        [Test]
        public void TestSendStartNotification()
        {

            N.NotificationSettings = new NotificationSettings { PercentageLimit = 1, Enabled = true, ThresholdTime = 1 };
            N.Email = new EmailModel
            {
                Alert = true,
                Address = "ABC@ABC.com",
                Host = "Host123",
                Port = 99,
                Username = "Username",
                Password = "password"
            };
            N.Notify(true);

            Assert.That(N.EndEventSaved, Is.False, "The values never got reset, this means that we didn't correctly end the notification");
            Assert.That(N.StartEventSaved, Is.True, "We never saved the start notification event");
            EventService.Verify(x => x.RecordEvent(It.IsAny<MonitoringEventsDto>()), Times.Once);
            Smtp.Verify(x => x.Send(N.Email.Host, N.Email.Port, It.IsAny<MailMessage>(), It.IsAny<NetworkCredential>()), Times.Once);
        }

        [Test]
        public void TestSendEndNotification()
        {
            var fileMock = new Mock<IFile>();
            fileMock.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns("abc");
            N.NotificationSettings = new NotificationSettings { PercentageLimit = 1, Enabled = true, ThresholdTime = 1 };
            N.Email = new EmailModel
            {
                Alert = true,
                Address = "ABC@ABC.com",
                Host = "Host123",
                Port = 99,
                Username = "Username",
                Password = "password"
            };
            N.Notify(true);
            EventService.Verify(x => x.RecordEvent(It.IsAny<MonitoringEventsDto>()), Times.Once);

            N.Notify(false);
            EventService.Verify(x => x.RecordEvent(It.IsAny<MonitoringEventsDto>()), Times.Exactly(2));

            // Values get reset
            Assert.That(N.EndEventSaved, Is.False, "The values never got reset, this means that we didn't correctly end the notification");
            Assert.That(N.StartEventSaved, Is.False, "The values never got reset, this means that we didn't correctly end the notification");


            Smtp.Verify(x => x.Send(N.Email.Host, N.Email.Port, It.IsAny<MailMessage>(), It.IsAny<NetworkCredential>()), Times.Exactly(2));
        }

        [Test]
        public void TestSendStartNotificationWithOutEmail()
        {
            var fileMock = new Mock<IFile>();
            fileMock.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns("abc");
            N.NotificationSettings = new NotificationSettings { PercentageLimit = 1, Enabled = true, ThresholdTime = 1 };
            N.Email = new EmailModel
            {
                Alert = false,
                Address = "ABC@ABC.com",
                Host = "Host123",
                Port = 99,
                Username = "Username",
                Password = "password"
            };
            N.Notify(true);

            Assert.That(N.EndEventSaved, Is.False, "The end event should never get saved.");
            Assert.That(N.StartEventSaved, Is.True, "We never saved the start notification event");
            EventService.Verify(x => x.RecordEvent(It.IsAny<MonitoringEventsDto>()), Times.Once);
            Smtp.Verify(x => x.Send(N.Email.Host, N.Email.Port, It.IsAny<MailMessage>(), It.IsAny<NetworkCredential>()), Times.Never);
        }

        [Test]
        public void TestSendEndNotificationWithoutEmail()
        {
            var fileMock = new Mock<IFile>();
            fileMock.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns("abc");
            N.NotificationSettings = new NotificationSettings { PercentageLimit = 1, Enabled = true, ThresholdTime = 1 };
            N.Email = new EmailModel
            {
                Alert = false,
                Address = "ABC@ABC.com",
                Host = "Host123",
                Port = 99,
                Username = "Username",
                Password = "password"
            };

            N.Notify(true);
            EventService.Verify(x => x.RecordEvent(It.IsAny<MonitoringEventsDto>()), Times.Once);

            N.Notify(false);
            EventService.Verify(x => x.RecordEvent(It.IsAny<MonitoringEventsDto>()), Times.Exactly(2));

            // Values get reset
            Assert.That(N.EndEventSaved, Is.False, "The values never got reset, this means that we didn't correctly end the notification");
            Assert.That(N.StartEventSaved, Is.False, "The values never got reset, this means that we didn't correctly end the notification");


            Smtp.Verify(x => x.Send(N.Email.Host, N.Email.Port, It.IsAny<MailMessage>(), It.IsAny<NetworkCredential>()), Times.Never);
        }

        [Test]
        public void TestNotificationDisabled()
        {
            N.NotificationSettings = new NotificationSettings { PercentageLimit = 1, Enabled = false, ThresholdTime = 1 };
            N.Email = new EmailModel
            {
                Alert = false,
                Address = "ABC@ABC.com",
                Host = "Host123",
                Port = 99,
                Username = "Username",
                Password = "password"
            };
            N.Notify(true);
            EventService.Verify(x => x.RecordEvent(It.IsAny<MonitoringEventsDto>()), Times.Never);

            // Values never get set
            Assert.That(N.EndEventSaved, Is.False, "The values have been set, they should not be set as notifier should be disabled");
            Assert.That(N.StartEventSaved, Is.False, "The values have been set, they should not be set as notifier should be disabled");

            Smtp.Verify(x => x.Send(N.Email.Host, N.Email.Port, It.IsAny<MailMessage>(), It.IsAny<NetworkCredential>()), Times.Never);
        }

        [Test]
        public void TestNotificationStartEventSavedButAttemptStartEvent()
        {
            N.NotificationSettings = new NotificationSettings { PercentageLimit = 1, Enabled = true, ThresholdTime = 1 };
            N.Email = new EmailModel
            {
                Alert = true,
                Address = "ABC@ABC.com",
                Host = "Host123",
                Port = 99,
                Username = "Username",
                Password = "password"
            };
            N.StartEventSaved = true;
            N.Notify(true);
            EventService.Verify(x => x.RecordEvent(It.IsAny<MonitoringEventsDto>()), Times.Never);
            // Values never get set
            Assert.That(N.EndEventSaved, Is.False, "The values have been set, they should not be set as notifier should be disabled");

            Smtp.Verify(x => x.Send(N.Email.Host, N.Email.Port, It.IsAny<MailMessage>(), It.IsAny<NetworkCredential>()), Times.Never);
        }
    }
}
