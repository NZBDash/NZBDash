#region Copyright
//  ***********************************************************************
//  Copyright (c) 2016 Jamie Rees
//  File: NzbGetHistory.cs
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
using System.Collections.Generic;

namespace NZBDash.ThirdParty.Api.Models.Api
{
    //https://github.com/nzbget/nzbget/wiki/API-Method-%22history%22
    public class NzbGetHistoryResult
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int RemainingFileCount { get; set; }
        public int HistoryTime { get; set; }
        public string Status { get; set; }
        public List<object> Log { get; set; }
        public int NZBID { get; set; }
        public string NZBName { get; set; }
        public string NZBNicename { get; set; }
        public string Kind { get; set; }
        public string URL { get; set; }
        public string NZBFilename { get; set; }
        public string DestDir { get; set; }
        public string FinalDir { get; set; }
        public string Category { get; set; }
        public string ParStatus { get; set; }
        public string ExParStatus { get; set; }
        public string UnpackStatus { get; set; }
        public string MoveStatus { get; set; }
        public string ScriptStatus { get; set; }
        public string DeleteStatus { get; set; }
        public string MarkStatus { get; set; }
        public string UrlStatus { get; set; }
        public object FileSizeLo { get; set; }
        public int FileSizeHi { get; set; }
        public int FileSizeMB { get; set; }
        public int FileCount { get; set; }
        public int MinPostTime { get; set; }
        public int MaxPostTime { get; set; }
        public int TotalArticles { get; set; }
        public int SuccessArticles { get; set; }
        public int FailedArticles { get; set; }
        public int Health { get; set; }
        public int CriticalHealth { get; set; }
        public string DupeKey { get; set; }
        public int DupeScore { get; set; }
        public string DupeMode { get; set; }
        public bool Deleted { get; set; }
        public object DownloadedSizeLo { get; set; }
        public int DownloadedSizeHi { get; set; }
        public int DownloadedSizeMB { get; set; }
        public int DownloadTimeSec { get; set; }
        public int PostTotalTimeSec { get; set; }
        public int ParTimeSec { get; set; }
        public int RepairTimeSec { get; set; }
        public int UnpackTimeSec { get; set; }
        public int MessageCount { get; set; }
        public int ExtraParBlocks { get; set; }
        public List<object> Parameters { get; set; }
        public List<object> ScriptStatuses { get; set; }
        public List<object> ServerStats { get; set; }
    }

    public class NzbGetHistory
    {
        public string version { get; set; }
        public List<NzbGetHistoryResult> result { get; set; }
    }
}