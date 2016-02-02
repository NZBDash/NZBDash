using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZBDash.Services.HardwareMonitor.Interfaces
{
    public interface IHardwareObserver
    {
        void Start(Configuration c);
        void Stop();
    }
}
