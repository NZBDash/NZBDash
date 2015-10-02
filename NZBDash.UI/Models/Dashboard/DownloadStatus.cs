namespace NZBDash.UI.Models.Dashboard
{
    public enum DownloadStatus
    {
        QUEUED,
        PAUSED,
        DOWNLOADING,
        FETCHING,
        PP_QUEUED,
        LOADING_PARS,
        VERIFYING_SOURCES,
        REPAIRING,
        VERIFYING_REPAIRED,
        RENAMING,
        UNPACKING,
        MOVING,
        EXECUTING_SCRIPT,
        PP_FINISHED
    }
}