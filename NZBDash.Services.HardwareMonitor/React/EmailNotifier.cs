#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: EmailNotifier.cs
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

using NZBDash.Services.HardwareMonitor.Interfaces;

namespace NZBDash.Services.HardwareMonitor.React
{
    class EmailNotifier : INotifier
    {
        public EmailNotifier(TimeSpan interval)
        {
            Interval = interval;
        }

        private int AlertCount { get; set; }
        private TimeSpan Interval { get; set; }
        private bool SendEndNotification { get; set; }
        private bool SendStartNotification { get; set; }
        private bool SentStartNotification { get; set; }

        public void ResetCounter()
        {
            Console.WriteLine("RESET!");
            AlertCount = 0;
        }

        private void CheckSend()
        {
            if (AlertCount >= Interval.Seconds && !SentStartNotification)
            {
                Console.WriteLine("SEND OUT TEH EMAILZ");
                SentStartNotification = true;
            }
            if (AlertCount == 0 && SentStartNotification)
            {
                Console.WriteLine("ALERT, IT's ENDED");
                SentStartNotification = false;
            }
        }

        public void Notify(bool critical)
        {
            Console.WriteLine("ALERT! {0}", critical);
            Console.WriteLine(DateTime.Now.Second);

            if (critical)
            {
                AlertCount++;
            }
            else
            {
                ResetCounter();
            }

            CheckSend();
        }
    }
}