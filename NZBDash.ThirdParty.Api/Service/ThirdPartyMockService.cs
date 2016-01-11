#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: ThirdPartyMockService.cs
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

using Newtonsoft.Json;

using NZBDash.Common.Interfaces;
using NZBDash.DataAccess.Api.CouchPotato;
using NZBDash.Resources;
using NZBDash.ThirdParty.Api.Interfaces;
using NZBDash.ThirdParty.Api.Models.Api;
using NZBDash.ThirdParty.Api.Models.Api.CouchPotato;
using NZBDash.ThirdParty.Api.Models.Api.SabNzbd;
using NZBDash.ThirdParty.Api.Models.Api.Sonarr;

namespace NZBDash.ThirdParty.Api.Service
{
    public class ThirdPartyMockService : IThirdPartyService
    {
        private ISerializer Serializer { get; set; }


        public ThirdPartyMockService(ISerializer serializer)
        {
            Serializer = serializer;
        }

        public CouchPotatoMediaList GetCouchPotatoMovies(string uri, string api)
        {
            var jsonData = MockData.CouchPotato_MediaList;
            return !string.IsNullOrEmpty(jsonData) ? JsonConvert.DeserializeObject<CouchPotatoMediaList>(jsonData) : new CouchPotatoMediaList();
 
        }

        public PlexServers GetPlexServers(string uri)
        {
            throw new NotImplementedException();
        }

        public SonarrSystemStatus GetSonarrSystemStatus(string uri, string api)
        {
            throw new NotImplementedException();
        }

        public List<SonarrSeries> GetSonarrSeries(string uri, string api)
        {
            var jsonData = MockData.Sonarr_Series;
            return !string.IsNullOrEmpty(jsonData) ? JsonConvert.DeserializeObject<List<SonarrSeries>>(jsonData) : new List<SonarrSeries>();
        }

        public List<SonarrEpisode> GetSonarrEpisodes(string uri, string api, int seriesId)
        {
            var jsonData = MockData.Sonarr_Episode;
            return !string.IsNullOrEmpty(jsonData) ? JsonConvert.DeserializeObject<List<SonarrEpisode>>(jsonData) : new List<SonarrEpisode>();
        }

        public CouchPotatoStatus GetCouchPotatoStatus(string uri, string api)
        {
            throw new NotImplementedException();
        }

        public NzbGetHistory GetNzbGetHistory(string url, string username, string password)
        {
            throw new NotImplementedException();
        }

        public NzbGetList GetNzbGetList(string url, string username, string password)
        {
            throw new NotImplementedException();
        }

        public NzbGetStatus GetNzbGetStatus(string url, string username, string password)
        {
            throw new NotImplementedException();
        }

        public NzbGetLogs GetNzbGetLogs(string url, string username, string password)
        {
            throw new NotImplementedException();
        }

        public SabNzbdHistory GetSabNzbdHistory(string url, string apiKey)
        {
            var jsonData = MockData.Sabnzbd_History;
            return !string.IsNullOrEmpty(jsonData) ? JsonConvert.DeserializeObject<SabNzbdHistory>(jsonData) : new SabNzbdHistory();
        }

        public SabNzbdQueue GetSabNzbdQueue(string url, string apiKey)
        {
            var jsonData = MockData.Sabnzbd_Queue;
            return !string.IsNullOrEmpty(jsonData) ? JsonConvert.DeserializeObject<SabNzbdQueue>(jsonData) : new SabNzbdQueue();
        }
    }
}