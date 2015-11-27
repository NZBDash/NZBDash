#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: IHardwareService.cs
//  Created By: Jamie Rees
// 
//  Permission is hereby granted, free of charge, to any person obtaining
//  a copy of this software and associated documentation files (the
//  "Software"), to deal in the Software without restriction, including
//  without limitation the rights to use, copy, modify, merge, publish,
//  distribute, sublicense, and/or sell copies of the Software, and to
//  permit persons to whom the Software is furnished to do so, subject to
//  the following conditions:
// 
//  The above copyright notice and this permission notice shall be
//  included in all copies or substantial portions of the Software.
// 
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//  LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//  OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//  WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//  ***********************************************************************
#endregion
using System;
using System.Collections.Generic;

using NZBDash.ThirdParty.Api.Models.Api;
using NZBDash.UI.Models.Hardware;

namespace NZBDash.Core.Interfaces
{
    /// <summary>
    /// The Interface for interacting with any hardware components e.g. RAM, CPU, GPU
    /// </summary>
    public interface IHardwareService
    {
        /// <summary>
        /// Gets the drives for the current PC.
        /// </summary>
        IEnumerable<DriveModel> GetDrives();

        /// <summary>
        /// Gets the ram for the current PC.
        /// </summary>
        RamModel GetRam();

        /// <summary>
        /// Gets up time for the current PC.
        /// </summary>
        TimeSpan GetUpTime();

        /// <summary>
        /// Gets the cpu percentage for the current PC.
        /// </summary>
        float GetCpuPercentage();

        /// <summary>
        /// Gets the available ram for the current PC.
        /// </summary>
        float GetAvailableRam();

        /// <summary>
        /// Gets the network information.
        /// </summary>
        NetworkInfo GetNetworkInformation();
    }
}
