#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: SonarrEpisodeViewModel.cs
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
using System.Collections.Generic;

namespace NZBDash.UI.Models.ViewModels.Sonarr
{
    public class EpisodeViewModel
    {
        public int SeriesId { get; set; }
        public int EpisodeFileId { get; set; }
        public int SeasonNumber { get; set; }
        public int EpisodeNumber { get; set; }
        public string Title { get; set; }
        public DateTime AirDate { get; set; }
        public DateTime AirDateUtc { get; set; }
        public string Overview { get; set; }
        public bool HasFile { get; set; }
        public string HasFileIcon
        {
            get
            {
                return string.Format("<i class=\"fa {0}\"></i>", HasFile ? FontAwesome.Check : FontAwesome.Cross);
            }
        }
        public bool Monitored { get; set; }
        public int SceneEpisodeNumber { get; set; }
        public int SceneSeasonNumber { get; set; }
        public int TvDbEpisodeId { get; set; }
        public int AbsoluteEpisodeNumber { get; set; }
        public bool Downloading { get; set; }
        public int ID { get; set; }
    }

    public class SonarrEpisodeViewModel
    {
        public string SeasonTitle { get; set; }
        public Dictionary<int, List<EpisodeViewModel>> EpisodeViewModels { get; set; }
    }
}
