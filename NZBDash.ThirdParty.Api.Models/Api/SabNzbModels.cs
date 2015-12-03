#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: SabNzbModels.cs
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
    public class SabNzbObject
    {
        public SabNzbdQueue Queue { get; set; }
        public History SabHistory { get; set; }
    }

    public class SabNzbdQueue
    {
        public string have_warnings { get; set; }
        public bool pp_active { get; set; }
        public int noofslots { get; set; }
        public bool paused { get; set; }
        public string pause_int { get; set; }
        public double mbleft { get; set; }
        public double diskspace2 { get; set; }
        public double diskspace1 { get; set; }
        public List<Job> jobs { get; set; }
        public string speed { get; set; }
        public string timeleft { get; set; }
        public double mb { get; set; }
        public string state { get; set; }
        public string loadavg { get; set; }
        public double kbpersec { get; set; }
    }
    public class Job
    {
        public string msgid { get; set; }
        public string filename { get; set; }
        public double mbleft { get; set; }
        public string id { get; set; }
        public double mb { get; set; }
    }

    public class SabNzbdHistory
    {
        public History History { get; set; }
    }

    public class History
    {
        public string total_size { get; set; }
        public string month_size { get; set; }
        public string week_size { get; set; }
        public string cache_limit { get; set; }
        public bool paused { get; set; }
        public string new_rel_url { get; set; }
        public bool restart_req { get; set; }
        public List<Slot> slots { get; set; }
        public string helpuri { get; set; }
        public string uptime { get; set; }
        public string version { get; set; }
        public string diskspacetotal2 { get; set; }
        public string color_scheme { get; set; }
        public bool darwin { get; set; }
        public bool nt { get; set; }
        public string status { get; set; }
        public string last_warning { get; set; }
        public string have_warnings { get; set; }
        public string cache_art { get; set; }
        public object finishaction { get; set; }
        public int noofslots { get; set; }
        public string cache_size { get; set; }
        public string new_release { get; set; }
        public string pause_int { get; set; }
        public string mbleft { get; set; }
        public string diskspace2 { get; set; }
        public string diskspace1 { get; set; }
        public string diskspacetotal1 { get; set; }
        public string timeleft { get; set; }
        public string mb { get; set; }
        public string eta { get; set; }
        public string nzb_quota { get; set; }
        public string loadavg { get; set; }
        public string kbpersec { get; set; }
        public string speedlimit { get; set; }
        public string webdir { get; set; }
    }


    public class Slot
    {
        public string action_line { get; set; }
        public string show_details { get; set; }
        public string script_log { get; set; }
        public object meta { get; set; }
        public string fail_message { get; set; }
        public bool loaded { get; set; }
        public int id { get; set; }
        public string size { get; set; }
        public string category { get; set; }
        public string pp { get; set; }
        public int completeness { get; set; }
        public string script { get; set; }
        public string nzb_name { get; set; }
        public int download_time { get; set; }
        public string storage { get; set; }
        public string status { get; set; }
        public string script_line { get; set; }
        public int completed { get; set; }
        public string nzo_id { get; set; }
        public int downloaded { get; set; }
        public string report { get; set; }
        public string path { get; set; }
        public int postproc_time { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public int bytes { get; set; }
        public string url_info { get; set; }
        public List<StageLog> stage_log { get; set; }
    }

    public class SabNzbdVersion
    {
        public string version { get; set; }
    }

}
