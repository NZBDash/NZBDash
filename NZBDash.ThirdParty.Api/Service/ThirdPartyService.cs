#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: ThirdPartyService.cs
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
using NZBDash.ThirdParty.Api.Interfaces;
using NZBDash.ThirdParty.Api.Models.Api;
using NZBDash.ThirdParty.Api.Models.Api.CouchPotato;
using NZBDash.ThirdParty.Api.Models.Api.SabNzbd;
using NZBDash.ThirdParty.Api.Models.Api.Sonarr;

namespace NZBDash.ThirdParty.Api.Service
{
    public class ThirdPartyService : IThirdPartyService
    {
        private ISerializer Serializer { get; set; }

        private const string SabNzbdQueueApiAddress = "api?mode=qstatus&output=json&apikey=";
        private const string SabNzbdHistoryApiAddress = "api?mode=history&start=0&limit=10&output=json&apikey=";
        private const string SonarrApiAddress = "api/system/status?apikey=";
        private const string NzbGetApiAddress = "/jsonrpc/";

        public ThirdPartyService(ISerializer serializer)
        {
            Serializer = serializer;
        }

        public CouchPotatoMediaList GetCouchPotatoMovies(string uri, string api)
        {
            return Serializer.SerializedJsonData<CouchPotatoMediaList>(uri + "api/" + api + "/app.available/");
        }

        public PlexServers GetPlexServers(string uri)
        {
            return Serializer.SerializeXmlData<PlexServers>(uri + "servers");
        }

        public SonarrSystemStatus GetSonarrSystemStatus(string uri, string api)
        {
            return Serializer.SerializedJsonData<SonarrSystemStatus>(uri + SonarrApiAddress + api);
        }

        public List<SonarrSeries> GetSonarrSeries(string uri, string api)
        {
            return Serializer.SerializedJsonData<List<SonarrSeries>>(uri + "api/series?apikey=" + api);
        }

        public List<SonarrEpisode> GetSonarrEpisodes(string uri, string api, int seriesId)
        {
            return Serializer.SerializedJsonData<List<SonarrEpisode>>(string.Format("{0}api/episode?seriesId={1}&apikey={2}", uri, seriesId, api));
        }

        public CouchPotatoStatus GetCouchPotatoStatus(string uri, string api)
        {
            return Serializer.SerializedJsonData<CouchPotatoStatus>(uri + "api/" + api + "/app.available/");
        }

        public NzbGetHistory GetNzbGetHistory(string url, string username, string password)
        {
            return Serializer.SerializedJsonData<NzbGetHistory>(string.Format("{0}{1}:{2}{3}history", url, username, password, NzbGetApiAddress));
        }

        public NzbGetList GetNzbGetList(string url, string username, string password)
        {
            return Serializer.SerializedJsonData<NzbGetList>(string.Format("{0}{1}:{2}{3}listgroups", url, username, password, NzbGetApiAddress));
        }

        public NzbGetStatus GetNzbGetStatus(string url, string username, string password)
        {
            return Serializer.SerializedJsonData<NzbGetStatus>(string.Format("{0}{1}:{2}{3}status", url, username, password, NzbGetApiAddress));
        }

        public NzbGetLogs GetNzbGetLogs(string url, string username, string password)
        {
            return Serializer.SerializedJsonData<NzbGetLogs>(string.Format("{0}{1}:{2}{3}/log?IDFrom=0&NumberOfEntries=1000", url, username, password, NzbGetApiAddress));
        }

        public SabNzbdHistory GetSabNzbdHistory(string url, string apiKey)
        {
            //var jsonData = Resources.MockData.Sabnzbd_History;
            //return !string.IsNullOrEmpty(jsonData) ? JsonConvert.DeserializeObject<SabNzbdHistory>(jsonData) : new SabNzbdHistory();
            return Serializer.SerializedJsonData<SabNzbdHistory>(url + SabNzbdHistoryApiAddress + apiKey);
        }

        public SabNzbdQueue GetSabNzbdQueue(string url, string apiKey)
        {
            //var jsonData = Resources.MockData.Sabnzbd_Queue;
            //return !string.IsNullOrEmpty(jsonData) ? JsonConvert.DeserializeObject<SabNzbdQueue>(jsonData) : new SabNzbdQueue();

            return Serializer.SerializedJsonData<SabNzbdQueue>(url + SabNzbdQueueApiAddress + apiKey);
        }
    }
}