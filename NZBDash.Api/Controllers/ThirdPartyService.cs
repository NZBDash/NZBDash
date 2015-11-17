using System;
using System.Web.Http;

using NZBDash.Api.Models;
using NZBDash.Common;
using NZBDash.Common.Interfaces;

namespace NZBDash.Api.Controllers
{
    public class ThirdPartyService : ApiController
    {
        private ISerializer Serializer { get; set; }
        public ThirdPartyService(ISerializer serializer)
        {
            Serializer = serializer;
        }

        [HttpGet]
        [ActionName("GetCouchPotatoMovies")]
        public void GetCouchPotatoMovies(string uri, string api)
        {
            throw new NotImplementedException("TODO");
        }

        [HttpGet]
        [ActionName("GetPlexServers")]
        public PlexServers GetPlexServers(string uri)
        {
            return Serializer.SerializeXmlData<PlexServers>(uri + "servers");
        }

        [HttpGet]
        [ActionName("GetSonarrSystemStatus")]
        public SonarrSystemStatus GetSonarrSystemStatus(string uri, string api)
        {
            return Serializer.SerializedJsonData<SonarrSystemStatus>(uri + "api/system/status?apikey=" + api);
        }

        [HttpGet]
        [ActionName("GetCouchPotatoStatus")]
        public CouchPotatoStatus GetCouchPotatoStatus(string uri, string api)
        {
            return Serializer.SerializedJsonData<CouchPotatoStatus>(uri + "api/" + api + "/app.available/");
        }

        [HttpGet]
        [ActionName("GetNzbGetHistory")]
        public NzbGetHistory GetNzbGetHistory(string url, string username, string password)
        {
            return Serializer.SerializedJsonData<NzbGetHistory>(string.Format("{0}{1}:{2}/jsonrpc/history", url, username, password));
        }

        [HttpGet]
        [ActionName("GetNzbGetHistory")]
        public NzbGetList GetNzbGetList(string url, string username, string password)
        {
            return Serializer.SerializedJsonData<NzbGetList>(string.Format("{0}{1}:{2}/jsonrpc/listgroups", url, username, password));
        }

        [HttpGet]
        [ActionName("GetNzbGetStatus")]
        public NzbGetStatus GetNzbGetStatus(string url, string username, string password)
        {
            return Serializer.SerializedJsonData<NzbGetStatus>(string.Format("{0}{1}:{2}/jsonrpc/status", url, username, password));
        }


    }
}