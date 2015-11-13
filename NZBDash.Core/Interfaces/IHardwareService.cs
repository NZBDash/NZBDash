using System.Collections.Generic;

using NZBDash.Common.Models.Hardware;

namespace NZBDash.Core.Interfaces
{
    public interface IHardwareService
    {
        IEnumerable<DriveModel> GetDrives();
        RamModel GetRam();
    }
}
