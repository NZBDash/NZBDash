using System;
using System.Collections.Generic;

using Newtonsoft.Json;

using NZBDash.Common.Interfaces;
using NZBDash.ThirdParty.Api.Interfaces;
using NZBDash.ThirdParty.Api.Models.Api;
using NZBDash.ThirdParty.Api.Models.Api.Sonarr;

namespace NZBDash.ThirdParty.Api.Service
{
    public class ThirdPartyService : IThirdPartyService
    {
        private ISerializer Serializer { get; set; }

        private const string SabNzbQueueApiAddress = "api?mode=qstatus&output=json&apikey=";
        private const string SabNzbHistoryApiAddress = "api?mode=history&start=0&limit=10&output=json&apikey=";
        private const string SonarrApiAddress = "api/system/status?apikey=";
        private const string NzbGetApiAddress = "/jsonrpc/";

        public ThirdPartyService(ISerializer serializer)
        {
            Serializer = serializer;
        }

        public void GetCouchPotatoMovies(string uri, string api)
        {
            throw new NotImplementedException("TODO");
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
            //var jsonData = Resources.Resources.Json2;
            //return !string.IsNullOrEmpty(jsonData) ? JsonConvert.DeserializeObject<List<SonarrSeries>>(jsonData) : new List<SonarrSeries>();
            
            return Serializer.SerializedJsonData<List<SonarrSeries>>(uri + "api/series?apikey=" + api);
        }

        public List<SonarrEpisode> GetSonarrEpisodes(string uri, string api, int seriesId)
        {
            //var jsonData = Resources.Resources.Json;
            //return !string.IsNullOrEmpty(jsonData) ? JsonConvert.DeserializeObject<List<SonarrEpisode>>(jsonData) : new List<SonarrEpisode>();

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

        public SabNzbHistory GetSabNzbHistory(string url, string apiKey)
        {
            return Serializer.SerializedJsonData<SabNzbHistory>(url + SabNzbHistoryApiAddress + apiKey);
        }

        public SabNzbQueue GetSanNzbQueue(string url, string apiKey)
        {
            return Serializer.SerializedJsonData<SabNzbQueue>(url + SabNzbQueueApiAddress + apiKey);
        }
    }
}