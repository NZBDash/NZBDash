#region Copyright
// ************************************************************************
//   Copyright (c) 2016 
//   File: CouchPotatoMediaList.cs
//   Created By: Jamie Rees
//  
//   Permission is hereby granted, free of charge, to any person obtaining
//   a copy of this software and associated documentation files (the
//   "Software"), to deal in the Software without restriction, including
//   without limitation the rights to use, copy, modify, merge, publish,
//   distribute, sublicense, and/or sell copies of the Software, and to
//   permit persons to whom the Software is furnished to do so, subject to
//   the following conditions:
//  
//   The above copyright notice and this permission notice shall be
//   included in all copies or substantial portions of the Software.
//  
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//   EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//   MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//   NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//   LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//   OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//   WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ************************************************************************
#endregion
using System.Diagnostics.CodeAnalysis;


namespace NZBDash.DataAccess.Api.CouchPotato
{
    [ExcludeFromCodeCoverage]
    public class CouchPotatoMediaList
    {
        public Movie[] movies { get; set; }
        public int total { get; set; }
        public bool empty { get; set; }
        public bool success { get; set; }
    }

    [ExcludeFromCodeCoverage]
    public class Movie
    {
        public string status { get; set; }
        public Info info { get; set; }
        public string _t { get; set; }
        public Release[] releases { get; set; }
        public string title { get; set; }
        public string _rev { get; set; }
        public string profile_id { get; set; }
        public string _id { get; set; }
        public object[] tags { get; set; }
        public int last_edit { get; set; }
        public string category_id { get; set; }
        public string type { get; set; }
        public Files files { get; set; }
        public Identifiers identifiers { get; set; }
    }
    [ExcludeFromCodeCoverage]
    public class Info
    {
        public Rating rating { get; set; }
        public string[] genres { get; set; }
        public int tmdb_id { get; set; }
        public string plot { get; set; }
        public string tagline { get; set; }
        public int year { get; set; }
        public string original_title { get; set; }
        public bool via_imdb { get; set; }
        public string[] directors { get; set; }
        public string[] titles { get; set; }
        public string imdb { get; set; }
        public string mpaa { get; set; }
        public bool via_tmdb { get; set; }
        public string[] actors { get; set; }
        public string[] writers { get; set; }
        public int runtime { get; set; }
        public string type { get; set; }
        public string released { get; set; }
        public Release_Date release_date { get; set; }
    }

    public class Rating
    {
        public float[] imdb { get; set; }
    }
    
    public class Release_Date
    {
        public int dvd { get; set; }
        public int expires { get; set; }
        public int theater { get; set; }
        public bool bluray { get; set; }
    }

    public class Files
    {
        public string[] image_poster { get; set; }
    }

    public class Identifiers
    {
        public string imdb { get; set; }
    }

    public class Release
    {
        public string status { get; set; }
        public Files1 files { get; set; }
        public string _id { get; set; }
        public string media_id { get; set; }
        public string _rev { get; set; }
        public string _t { get; set; }
        public object is_3d { get; set; }
        public int last_edit { get; set; }
        public string identifier { get; set; }
        public string quality { get; set; }
        public Info1 info { get; set; }
        public Download_Info download_info { get; set; }
    }

    public class Files1
    {
        public string[] nfo { get; set; }
        public string[] movie { get; set; }
        public string[] leftover { get; set; }
        public string[] subtitle { get; set; }
    }

    public class Info1
    {
        public string protocol { get; set; }
        public string description { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public int age { get; set; }
        public string name_extra { get; set; }
        public string seed_ratio { get; set; }
        public string content { get; set; }
        public int score { get; set; }
        public string provider { get; set; }
        public string seed_time { get; set; }
        public string provider_extra { get; set; }
        public string detail_url { get; set; }
        public string type { get; set; }
        public string id { get; set; }
        public int size { get; set; }
    }

    public class Download_Info
    {
        public bool status_support { get; set; }
        public string id { get; set; }
        public string downloader { get; set; }
    }

}