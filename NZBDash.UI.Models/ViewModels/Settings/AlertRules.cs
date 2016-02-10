#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: AlertRules.cs
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
using System.Collections.Generic;
using System.Web.Mvc;

namespace NZBDash.UI.Models.ViewModels.Settings
{
    public class AlertRules
    {
        public AlertRules()
        {
            Errors = new Dictionary<string, string>();
        }
        public AlertType AlertType { get; set; }
        public int Id { get; set; }
        public int ThresholdTime { get; set; }
        public int Percentage { get; set; }
        public bool Enabled { get; set; }

        public const string ThresholdErrorKey = "ThresholdTime";
        public const string PercentageErrorKey = "Percentage";
        


        /// <summary>
        /// This is only valid for network monitoring, we need to know the Nic to monitor
        /// </summary>
        public int NicId { get; set; }

        public int DriveId { get; set; }

        public Dictionary<string, int> NicDict { get; set; }
        public SelectList Nics { get; set; }
        public List<DriveAlertViewModel> Drives { get; set; }

        /// <summary>
        /// Custom check to see if the model is valid
        /// </summary>
        public bool IsValid
        {
            get
            {
                if (Enabled)
                {
                    if (ThresholdTime == 0)
                        Errors.Add(ThresholdErrorKey, "Threshold is not valid"); //TODO Resource

                    if (Percentage == 0)
                        Errors.Add(PercentageErrorKey, "Percentage is not valid");

                    return Errors.Count == 0;
                }
                return true;
            }
        }

        public Dictionary<string, string> Errors { get; set; }
    }
}