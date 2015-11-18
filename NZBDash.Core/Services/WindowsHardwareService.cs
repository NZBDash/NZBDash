using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

using Microsoft.VisualBasic.Devices;

using NZBDash.Common.Models.Hardware;
using NZBDash.Core.Interfaces;

using Omu.ValueInjecter;

namespace NZBDash.Core.Services
{
    public class WindowsHardwareService : IHardwareService
    {
        /// <summary>
        /// Gets the drives.
        /// </summary>
        public IEnumerable<DriveModel> GetDrives()
        {
            var drives = GetDriveInfo();
            var model = new List<DriveModel>();
            foreach (var drive in drives)
            {
                if (!drive.IsReady)
                {
                    continue;
                }

                var driveModel = new DriveModel();
                var mapped = driveModel.InjectFrom(drive);
                model.Add((DriveModel)mapped);
            }

            return model;
        }

        /// <summary>
        /// Gets the ram.
        /// </summary>
        public RamModel GetRam()
        {
            var ramInfo = GetRamInfo();
            var ramModel = new RamModel();
            return (RamModel)ramModel.InjectFrom(ramInfo);
        }

        /// <summary>
        /// Gets up time of the current PC.
        /// </summary>
        public TimeSpan GetUpTime()
        {
            using (var uptime = new PerformanceCounter("System", "System Up Time"))
            {
                // Call this an extra time before reading its value
                uptime.NextValue();
                return TimeSpan.FromSeconds(uptime.NextValue());
            }
        }

        /// <summary>
        /// Gets the cpu percentage of the current PC.
        /// </summary>
        public float GetCpuPercentage()
        {
            using (var process = new PerformanceCounter("Processor", "% Processor Time", "_Total"))
            {
                // Call this an extra time before reading its value
                process.NextValue();

                // We require the PC tp update it self... We need to wait.
                Thread.Sleep(500);
                return process.NextValue();
            }
        }

        /// <summary>
        /// Gets the available ram.
        /// </summary>
        /// <returns></returns>
        public float GetAvailableRam()
        {
            using (var memory = new PerformanceCounter("Memory", "Available MBytes"))
            {
                // Call this an extra time before reading its value
                memory.NextValue();

                return memory.NextValue();
            }
        }

        private ComputerInfo GetRamInfo()
        {
            return new ComputerInfo();
        }

        private DriveInfo[] GetDriveInfo()
        {
            return DriveInfo.GetDrives();
        }
    }
}
