﻿#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: HardwareMapperInitialise.cs
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
using System.Linq;

using NZBDash.Core.Models.Settings;
using NZBDash.DataAccessLayer.Models.Settings;
using NZBDash.UI.Models.ViewModels.Settings;

using Omu.ValueInjecter;

namespace NZBDash.Common.Mapping
{
    public class HardwareMapperInitialise
    {
        static HardwareMapperInitialise()
        {
            Mapper.AddMap<HardwareSettings, HardwareSettingsDto>(x =>
            {
                var settings = new HardwareSettingsDto();
                settings.NetworkMonitoring.InjectFrom(x.NetworkMonitoring);
                settings.CpuMonitoring.InjectFrom(x.CpuMonitoring);
                settings.EmailAlertSettings.InjectFrom(x.EmailAlertSettings);

                settings.Drives = x.Drives
                    .Select(c => new DriveSettingsDto().InjectFrom(c)).Cast<DriveSettingsDto>()
                    .ToList();

                return settings;
            });

            Mapper.AddMap<HardwareSettingsDto, HardwareSettings>(x =>
            {
                var settings = new HardwareSettings();
                settings.NetworkMonitoring.InjectFrom(x.NetworkMonitoring);
                settings.CpuMonitoring.InjectFrom(x.CpuMonitoring);
                settings.EmailAlertSettings.InjectFrom(x.EmailAlertSettings);

                settings.Drives = x.Drives
                    .Select(c => new DriveSettings().InjectFrom(c)).Cast<DriveSettings>()
                    .ToList();

                return settings;
            });

            Mapper.AddMap<HardwareSettingsViewModel, HardwareSettingsDto>(x =>
            {
                var settings = new HardwareSettingsDto();
                settings.NetworkMonitoring.InjectFrom(x.NetworkMonitoring);
                settings.CpuMonitoring.InjectFrom(x.CpuMonitoring);
                settings.EmailAlertSettings.InjectFrom(x.EmailAlertSettings);

                settings.Drives = x.Drives
                    .Select(c => new DriveSettingsDto().InjectFrom(c)).Cast<DriveSettingsDto>()
                    .ToList();

                return settings;
            });

            Mapper.AddMap<HardwareSettingsDto, HardwareSettingsViewModel>(x =>
            {
                var settings = new HardwareSettingsViewModel();
                settings.NetworkMonitoring.InjectFrom(x.NetworkMonitoring);
                settings.CpuMonitoring.InjectFrom(x.CpuMonitoring);
                settings.EmailAlertSettings.InjectFrom(x.EmailAlertSettings);

                settings.Drives = x.Drives
                    .Select(c => new DriveSettingsViewModel().InjectFrom(c)).Cast<DriveSettingsViewModel>()
                    .ToList();

                return settings;
            });
        }
    }
}
