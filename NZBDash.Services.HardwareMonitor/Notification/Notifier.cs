﻿#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: CpuNotifier.cs
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

using NZBDash.Common.Interfaces;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Models;
using NZBDash.Core.Models.Settings;
using NZBDash.Services.HardwareMonitor.Interfaces;

namespace NZBDash.Services.Monitor.Notification
{
    public class Notifier : INotifier
    {
        public Notifier(TimeSpan interval, IEventService eventService, ISmtpClient client, IFile file, ILogger logger, AlertTypeDto type)
        {
            Interval = interval;
            EventService = eventService;
            SmtpClient = client;
            File = file;
            Logger = logger;
            Sendy = new EmailSender(file, logger, client);
            AlertType = type;
        }

        private AlertTypeDto AlertType { get; set; }
        public bool StartEventSaved { get; set; }
        public bool EndEventSaved { get; set; }
        public NotificationSettings NotificationSettings { get; set; }
        public EmailModel Email { get; set; }

        private ISmtpClient SmtpClient { get; set; }
        private ILogger Logger { get; set; }
        private IFile File { get; set; }
        private IEventService EventService { get; set; }
        private int AlertCount { get; set; }
        public TimeSpan Interval { get; set; }
        private bool SentStartNotification { get; set; }
        private DateTime StartEventTime { get; set; }
        private DateTime EndEventTime { get; set; }
        private EmailSender Sendy { get; set; }


        public void ResetCounter()
        {
            Logger.Trace("Reset {0} Counter", AlertType);
            AlertCount = 0;
        }

        private void CheckSend()
        {
            if (AlertCount >= Interval.TotalSeconds)
            {
                if (!StartEventSaved)
                {
                    StartEventTime = DateTime.Now;
                    Logger.Trace("Saving Start Event");
                    SaveEvent();
                    StartEventSaved = true;

                    if (Email.Alert && !SentStartNotification)
                    {
                        SendEmail();
                        Logger.Trace("Sending out alert start email");
                        SentStartNotification = true;
                    }
                }
            }


            if (AlertCount == 0 && StartEventSaved)
            {
                if (!EndEventSaved)
                {

                    EndEventTime = DateTime.Now;
                    Logger.Trace("Saving End Event");
                    SaveEvent();
                    EndEventSaved = true;

                    if (Email.Alert)
                    {
                        SendEmail();
                        Logger.Trace("Sent out Alert end email");
                        SentStartNotification = false;
                    }
                }
                Reset();
            }
        }

        private void Reset()
        {
            StartEventTime = DateTime.MinValue;
            EndEventTime = DateTime.MinValue;
            StartEventSaved = false;
            EndEventSaved = false;
        }

        public void Notify(bool critical)
        {
            if (!NotificationSettings.Enabled)
            {
                Logger.Trace("{0} Monitoring is not enabled", AlertType);
                return;
            }

            if (critical)
            {
                AlertCount++;
                Logger.Trace("Current alert count {0}", AlertCount);
            }
            else
            {
                ResetCounter();
            }

            CheckSend();
        }

        public void SaveEvent()
        {
            var dto = new MonitoringEventsDto
            {
                EventName = ConvertEvent(AlertType),
                EventEnd = EndEventTime,
                EventStart = StartEventTime,
                EventType = EndEventTime == DateTime.MinValue ? EventTypeDto.Start : EventTypeDto.End
            };

            var result = EventService.RecordEvent(dto);
            Logger.Trace("Saved event to DB result {0}", result);

        }

        private void SendEmail()
        {
            Email.BreachEnd = EndEventTime;
            Email.BreachStart = StartEventTime;
            Email.Percentage = NotificationSettings.PercentageLimit;
            Email.TimeThresholdSec = NotificationSettings.ThresholdTime;
            Email.BreachType = AlertType.ToString();

            Sendy.SendEmail(Email);
        }

        private string ConvertEvent(AlertTypeDto type)
        {
            switch (type)
            {
                case AlertTypeDto.Cpu:
                    return EventName.CpuEvent;
                case AlertTypeDto.Network:
                    return EventName.NetworkEvent;
                case AlertTypeDto.Hdd:
                    return EventName.HddSpaceEvent;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}