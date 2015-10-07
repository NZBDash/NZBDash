using System;

using NZBDash.UI.Models.Dashboard;

namespace NZBDash.UI.Helpers
{
    public static class IconHelper
    {
        public static string ChooseIcon(DownloadStatus downloadStatus)
        {
            switch (downloadStatus)
            {
                case DownloadStatus.QUEUED:
                    return "fa-clock-o";
                case DownloadStatus.PAUSED:
                    return "fa-pause";
                case DownloadStatus.DOWNLOADING:
                    return "fa-download";
                case DownloadStatus.FETCHING:
                    return "fa-spinner";
                case DownloadStatus.PP_QUEUED:
                    break;
                case DownloadStatus.LOADING_PARS:
                    return "fa-spinner";
                case DownloadStatus.VERIFYING_SOURCES:
                    break;
                case DownloadStatus.REPAIRING:
                    break;
                case DownloadStatus.VERIFYING_REPAIRED:
                    break;
                case DownloadStatus.RENAMING:
                    break;
                case DownloadStatus.UNPACKING:
                    break;
                case DownloadStatus.MOVING:
                    return "fa-arrow-right";
                case DownloadStatus.EXECUTING_SCRIPT:
                    break;
                case DownloadStatus.PP_FINISHED:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("downloadStatus", downloadStatus, null);
            }
            return "fa-question";
        }
    }
}