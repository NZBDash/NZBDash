using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using NZBDash.Api.Models;

namespace NZBDash.Api.Controllers
{
    public interface IThirdPartyService
    {
        void GetCouchPotatoMovies(string uri, string api);
        PlexServers GetPlexServers(string uri);
        SonarrSystemStatus GetSonarrSystemStatus(string uri, string api);
        CouchPotatoStatus GetCouchPotatoStatus(string uri, string api);
        NzbGetHistory GetNzbGetHistory(string url, string username, string password);
        NzbGetList GetNzbGetList(string url, string username, string password);
        NzbGetStatus GetNzbGetStatus(string url, string username, string password);
    }
}