using System;
using System.Net;

namespace NZBDash.Common.Interfaces
{
    public interface IWebClient : IDisposable
    {
        string DownloadString(string address);
        WebHeaderCollection Headers { get; set; }
        string UploadString(string uri, string method, string data);
    }
}
