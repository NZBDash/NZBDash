using System;

using NZBDash.Api.Models;
using NZBDash.Common.Interfaces;
using NZBDash.Common.Models.Api;
using NZBDash.ThirdParty.Api.Interfaces;

namespace NZBDash.ThirdParty.Api.Service
{
    public class ThirdPartyService : IThirdPartyService
    {
        private ISerializer Serializer { get; set; }
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
            return Serializer.SerializedJsonData<SonarrSystemStatus>(uri + "api/system/status?apikey=" + api);
        }

        public SonarrSeriesWrapper GetSonarrSeries(string uri, string api)
        {
            return Serializer.SerializedJsonData<SonarrSeriesWrapper>(uri + "api/system/series?apikey=" + api);
        }

        public CouchPotatoStatus GetCouchPotatoStatus(string uri, string api)
        {
            return Serializer.SerializedJsonData<CouchPotatoStatus>(uri + "api/" + api + "/app.available/");
        }

        public NzbGetHistory GetNzbGetHistory(string url, string username, string password)
        {
            return Serializer.SerializedJsonData<NzbGetHistory>(string.Format("{0}{1}:{2}/jsonrpc/history", url, username, password));
        }

        public NzbGetList GetNzbGetList(string url, string username, string password)
        {
            return Serializer.SerializedJsonData<NzbGetList>(string.Format("{0}{1}:{2}/jsonrpc/listgroups", url, username, password));
        }

        public NzbGetStatus GetNzbGetStatus(string url, string username, string password)
        {
            return Serializer.SerializedJsonData<NzbGetStatus>(string.Format("{0}{1}:{2}/jsonrpc/status", url, username, password));
        }
    }
}