using System;
using System.ComponentModel.DataAnnotations;

namespace NZBDash.Common.Models.Hardware
{
    public class ServerInformationViewModel
    {
        [Display(Name = "Operating System")]
        public string OsFullName { get;set; }
        [Display(Name = "Operating Platform")]
        public string OsPlatform { get;set; }
        [Display(Name = "Operating Version")]
        public string OsVersion { get; set; }
        [Display(Name = "System Uptime")]
        public TimeSpan Uptime { get; set; }
        [Display(Name="Cpu %")]
        public float CpuPercentage { get; set; }
        [Display(Name = "Available Memory")]
        public float AvailableMemory { get; set; }
    }
}
