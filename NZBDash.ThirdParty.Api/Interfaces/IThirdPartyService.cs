using NZBDash.Api.Models;

namespace NZBDash.ThirdParty.Api.Interfaces
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