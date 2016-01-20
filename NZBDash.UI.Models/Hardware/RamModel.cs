#region Copyright
//  ***********************************************************************
//  Copyright (c) 2016 Jamie Rees
//  File: RamModel.cs
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
using System.ComponentModel.DataAnnotations;

namespace NZBDash.UI.Models.Hardware
{
    public class RamModel
    {
        [Display(Name = "UI_Models_Hardware_RamModel_VirtualPercentageFilled", ResourceType = typeof(Resources.Resources))]
        public int VirtualPercentageFilled
        {
            get { return 100 - (int)(AvailableVirtualMemory * 100 / TotalVirtualMemory); }
        }

        [Display(Name = "UI_Models_Hardware_RamModel_VirtualPercentageFilled", ResourceType = typeof(Resources.Resources))]
        public int PhysicalPercentageFilled
        {
            get { return 100 - (int)(AvailablePhysicalMemory * 100 / TotalPhysicalMemory); }
        }

        [Display(Name = "UI_Models_Hardware_RamModel_AvailablePhysicalMemory", ResourceType = typeof(Resources.Resources))]
        public ulong AvailablePhysicalMemory { get; set; }

        [Display(Name = "UI_Models_Hardware_RamModel_AvailableVirtualMemory", ResourceType = typeof(Resources.Resources))]
        public ulong AvailableVirtualMemory { get; set; }

        [Display(Name = "UI_Models_Hardware_RamModel_OSName", ResourceType = typeof(Resources.Resources))]
        public string OSFullName { get; set; }

        [Display(Name = "UI_Models_Hardware_RamModel_OSPlatform", ResourceType = typeof(Resources.Resources))]
        public string OSPlatform { get; set; }

        [Display(Name = "UI_Models_Hardware_RamModel_OSVersion", ResourceType = typeof(Resources.Resources))]
        public string OSVersion { get; set; }

        [Display(Name = "UI_Models_Hardware_RamModel_TotalPhysicalMemory", ResourceType = typeof(Resources.Resources))]
        public ulong TotalPhysicalMemory { get; set; }

        [Display(Name = "UI_Models_Hardware_RamModel_TotalVirtualMemory", ResourceType = typeof(Resources.Resources))]
        public ulong TotalVirtualMemory { get; set; }
    }
}