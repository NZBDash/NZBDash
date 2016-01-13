#region Copyright
//  ***********************************************************************
//  Copyright (c) 2016 Jamie Rees
//  File: IconHelper.cs
//  Created By: Jamie Rees
// 
//  Permission is hereby granted, free of charge, to any person obtaining
//  a copy of this software and associated documentation files (the
//  "Software"), to deal in the Software without restriction, including
//  without limitation the rights to use, copy, modify, merge, publish,
//  distribute, sublicense, and/or sell copies of the Software, and to
//  permit persons to whom the Software is furnished to do so, subject to
//  the following conditions:
// 
//  The above copyright notice and this permission notice shall be
//  included in all copies or substantial portions of the Software.
// 
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//  LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//  OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//  WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//  ***********************************************************************
#endregion
using System;

using NZBDash.UI.Models;
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
                    return FontAwesome.Clock;
                case DownloadStatus.PAUSED:
                    return FontAwesome.Pause;
                case DownloadStatus.DOWNLOADING:
                    return FontAwesome.Download;
                case DownloadStatus.FETCHING:
                    return FontAwesome.LoadingSpinner;
                case DownloadStatus.PP_QUEUED:
                    break;
                case DownloadStatus.LOADING_PARS:
                    return FontAwesome.LoadingSpinner;
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
                    return FontAwesome.ArrowRight;
                case DownloadStatus.EXECUTING_SCRIPT:
                    break;
                case DownloadStatus.PP_FINISHED:
                    break;
                default:
                    throw new ArgumentOutOfRangeException("downloadStatus", downloadStatus, null);
            }
            return FontAwesome.QuestionMark;
        }
    }
}