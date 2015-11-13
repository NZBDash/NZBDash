using System.Collections.Generic;
using System.IO;

using Microsoft.VisualBasic.Devices;

using NZBDash.Common.Models.Hardware;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model;

using Omu.ValueInjecter;

namespace NZBDash.Core.Services
{
    public class WindowsHardwareService : IHardwareService
    {
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

        public RamModel GetRam()
        {
            var ramInfo = GetRamInfo();
            var ramModel = new RamModel();
            return (RamModel)ramModel.InjectFrom(ramInfo);
        }

        private RamInfoObj GetRamInfo()
        {
            return new RamInfoObj(new ComputerInfo());
        }

        private DriveInfo[] GetDriveInfo()
        {
            return DriveInfo.GetDrives();
        }
    }
}
