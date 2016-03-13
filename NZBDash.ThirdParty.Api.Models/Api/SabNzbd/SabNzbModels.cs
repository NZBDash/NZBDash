#region Copyright
//  ***********************************************************************
//  Copyright (c) 2016 Jamie Rees
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

namespace NZBDash.ThirdParty.Api.Models.Api.SabNzbd
{
    public class SabNzbObject
    {
        public SabNzbdQueue Queue { get; set; }
        public SabNzbdHistory SabHistory { get; set; }
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

    public class SabNzbdVersion
    {
        public string version { get; set; }
    }

}
