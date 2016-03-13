#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: HardwareSettingsViewModel.cs
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
    public class HardwareSettingsViewModel
    {
        public HardwareSettingsViewModel()
        {
            CpuMonitoring = new CpuMonitoringViewModel();
            NetworkMonitoring = new NetworkMonitoringViewModel();
            EmailAlertSettings = new EmailAlertSettingsViewModel();
            Drives = new List<DriveSettingsViewModel>();
        }
        public CpuMonitoringViewModel CpuMonitoring { get; set; }
        public NetworkMonitoringViewModel NetworkMonitoring { get; set; }
        public List<DriveSettingsViewModel> Drives { get; set; }
        public EmailAlertSettingsViewModel EmailAlertSettings { get; set; }
    }

    public class CpuMonitoringViewModel
    {
        public bool Enabled { get; set; }
        public int ThresholdTime { get; set; }
        public int CpuPercentageLimit { get; set; }
    }

    public class NetworkMonitoringViewModel
    {
        public bool Enabled { get; set; }
        public int ThresholdTime { get; set; }
        public int NicId { get; set; }
        public int NetworkUsagePercentage { get; set; }
        public Dictionary<string, int> NicDict { get; set; }
        public SelectList Nics { get; set; }
    }


    public class EmailAlertSettingsViewModel
    {
        public bool AlertOnBreach { get; set; }
        public bool AlertOnBreachEnd { get; set; }
        public string EmailUsername { get; set; }
        public string EmailPassword { get; set; }
        public string EmailHost { get; set; }
        public int EmailPort { get; set; }
        public string RecipientAddress { get; set; }
    }

}