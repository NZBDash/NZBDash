using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

using Microsoft.VisualBasic.Devices;

namespace NZBDash.Core.Model
{

    public class RamInfoObj
    {

        public RamInfoObj(ComputerInfo info)
        {
            this.AvailablePhysicalMemory = info.AvailablePhysicalMemory;
            this.AvailableVirtualMemory = info.AvailableVirtualMemory;
            this.OSFullName = info.OSFullName;
            this.OSPlatform = info.OSPlatform;
            this.OSVersion = info.OSVersion;
            this.TotalPhysicalMemory = info.TotalPhysicalMemory;
            this.TotalVirtualMemory = info.TotalVirtualMemory;
            this.VirtualPercentageFilled = 100 - (int)(info.AvailableVirtualMemory * 100 / info.TotalVirtualMemory);
            this.PhysicalPercentageFilled = 100 - (int)(info.AvailablePhysicalMemory * 100 / info.TotalPhysicalMemory);
        }

        public int VirtualPercentageFilled { get; set; }
        public int PhysicalPercentageFilled { get; set; }
        public ulong AvailablePhysicalMemory { get; set; }

        public ulong AvailableVirtualMemory { get; set; }

        //public CultureInfo InstalledUICulture { get; set;}

        public string OSFullName { get; set; }

        public string OSPlatform { get; set; }

        public string OSVersion { get; set; }

        public ulong TotalPhysicalMemory { get; set; }

        public ulong TotalVirtualMemory { get; set; }
    }

    public class DriveInfoObj
    {
        public DriveInfoObj(DriveInfo info)
        {
            AvailableFreeSpace = info.AvailableFreeSpace;
            DriveFormat = info.DriveFormat;
            IsReady = info.IsReady;
            Name = info.Name;
            TotalFreeSpace = info.TotalFreeSpace;
            TotalSize = info.TotalSize;
            VolumeLabel = info.VolumeLabel;
            var i = info.TotalFreeSpace * 100 / info.TotalSize;
            PercentageFilled = Int32.Parse((100 - i).ToString());
            var s = (info.TotalFreeSpace / 1024 / 1024).ToString();
            s = s.Substring(0, s.Length - 3) + "," + s.Substring(s.Length - 3);
            FreeSpaceString = s;
            s = (info.TotalSize / 1024 / 1024).ToString();
            s = s.Substring(0, s.Length - 3) + "," + s.Substring(s.Length - 3);
            TotalSpaceString = s;
        }
        public long AvailableFreeSpace { get; set; }
        public int PercentageFilled { get; set; }
        public string DriveFormat { get; set; }
        public string FreeSpaceString { get; set; }
        public string TotalSpaceString { get; set; }
        public bool IsReady { get; set; }
        public string Name { get; set; }
        public long TotalFreeSpace { get; set; }
        public long TotalSize { get; set; }
        public string VolumeLabel { get; set; }
    }

    public class NetworkInfo
    {
        public float Sent { get; set; }
        public float Recieved { get; set; }
        public float Total { get; set; }
    }

    public class ProcessObj
    {
        public int BasePriority { get; set; }
        public bool EnableRaisingEvents { get; set; }
        public int ExitCode { get; set; }
        public DateTime ExitTime { get; set; }
        public IntPtr Handle { get; set; }
        public int HandleCount { get; set; }
        public bool HasExited { get; set; }
        public int Id { get; set; }
        public string MachineName { get; set; }
        public IntPtr MainWindowHandle { get; set; }
        public string MainWindowTitle { get; set; }
        public IntPtr MaxWorkingSet { get; set; }
        public IntPtr MinWorkingSet { get; set; }
        public long NonpagedSystemMemorySize64 { get; set; }
        public long PagedMemorySize64 { get; set; }
        public long PagedSystemMemorySize64 { get; set; }
        public long PeakPagedMemorySize64 { get; set; }
        public long PeakVirtualMemorySize64 { get; set; }
        public long PeakWorkingSet64 { get; set; }
        public long PrivateMemorySize64 { get; set; }
        public TimeSpan PrivilegedProcessorTime { get; set; }
        public string ProcessName { get; set; }
        public IntPtr ProcessorAffinity { get; set; }
        public bool Responding { get; set; }
        public int SessionId { get; set; }
        public ProcessStartInfo StartInfo { get; set; }
        public DateTime StartTime { get; set; }
        //public ISynchronizeInvoke SynchronizingObject { get; set; }
        // public ProcessThreadCollection Threads { get; set;}
        public TimeSpan TotalProcessorTime { get; set; }
        public TimeSpan UserProcessorTime { get; set; }
        public long VirtualMemorySize64 { get; set; }
        public long WorkingSet64 { get; set; }

    }

    public class InfoViewModel
    {
        public List<DriveInfoObj> DriveInfos { get; set; }
        public RamInfoObj RamInfo { get; set; }
        public TimeSpan UpTime { get; set; }
        public NetworkInfo NetworkInfo { get; set; }
        public List<ProcessObj> Processes { get; set; }
        public SystemLinks Links { get; set; }
        public SabNzbObject SabNzbObject { get; set; }
    }

}
