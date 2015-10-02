namespace NZBDash.UI.Models.Dashboard
{
    public class DrivesViewModel
    {
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
}