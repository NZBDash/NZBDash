using NZBDash.Api.Models;

namespace NZBDash.Api.Controllers
{
    public interface IStatusApi
    {
        NzbGetStatus GetNzbGetStatus(string url, string username, string password);
        NzbGetList GetNzbGetList(string url, string username, string password);
        NzbGetHistory GetNzbGetHistory(string url, string username, string password);
    }
}
