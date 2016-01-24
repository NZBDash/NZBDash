#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: EmailAlert.cs
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
using System.Threading.Tasks;

using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.Core.Models;
using NZBDash.Core.Models.Settings;

namespace NZBDash.Services.HardwareMonitor.Alert
{
    internal class EmailAlert : IAlert
    {
        public bool AlertedOnStart { get; set; }
        public bool AlertedOnEnd { get; set; }

        private string EmailUsername { get; set; }
        private string EmailPassword { get; set; }
        private string EmailHost { get; set; }
        private int EmailPort { get; set; }
        public bool AlertEnabledOnBreach { get; set; }
        public bool AlertEnabledOnBreachEnd { get; set; }
        private string Recipient { get; set; }

        private DateTime BreachTime { get; set; }
        private DateTime EndBreachTime { get; set; }

        private IEventService EventService { get; set; }

        public EmailAlert(IEventService eventService, EmailAlertSettingsDto settings, DateTime breachTime, DateTime endBreachTime)
        {
            EventService = eventService;
            EmailUsername = settings.EmailUsername;
            EmailPassword = settings.EmailPassword;
            EmailHost = settings.EmailHost;
            EmailPort = settings.EmailPort;
            AlertEnabledOnBreach = settings.AlertOnBreach;
            AlertEnabledOnBreachEnd = settings.AlertOnBreachEnd;
            Recipient = settings.RecipientAddress;
            BreachTime = breachTime;
            EndBreachTime = endBreachTime;
        }

        public void SaveEvent()
        {
            var dto = new MonitoringEventsDto
            {
                EventName = EventName.CpuEvent,
                EventEnd = EndBreachTime,
                EventStart = BreachTime,
                EventType = EndBreachTime == DateTime.MinValue ? EventTypeDto.Start : EventTypeDto.End
            };

            var result = EventService.RecordEvent(dto);
            
        }

        public void Alert()
        {
            // EndBreachTime could be DateTime.MinValue meaning it hasn't ended.
            // TODO: Requirements in the UI:
            // Alert on threashold breached?
            // Alert on threashold breach recovered/ended?

            if (AlertEnabledOnBreach && BreachTime != DateTime.MinValue && !AlertedOnStart)
            {
                AlertOnBreach();
            }

            if (AlertEnabledOnBreachEnd && EndBreachTime != DateTime.MinValue && !AlertedOnEnd)
            {
                AlertOnBreachEnd();
            }
        }

        private void AlertOnBreach()
        {
            SaveEvent();
            Console.WriteLine("Breach Started");
        }

        private void AlertOnBreachEnd()
        {
            SaveEvent();
            Console.WriteLine("Breach Ended");
        }
    }
}