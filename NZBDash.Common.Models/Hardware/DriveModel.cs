namespace NZBDash.Common.Models.Hardware
{
    public class DriveModel
    {
        public long AvailableFreeSpace { get; set; }
        public string DriveFormat { get; set; }
        public string DriveType { get; set; }
        public bool IsReady { get; set; }
        public string Name { get; set; }
        public long TotalFreeSpace { get; set; }
        public long TotalSize { get; set; }
        public string VolumeLabel { get; set; }
        public int PercentageFilled
        {
            get
            {
                var i = TotalFreeSpace * 100 / TotalSize;
                return int.Parse((100 - i).ToString());
            }
        }
    }
}
