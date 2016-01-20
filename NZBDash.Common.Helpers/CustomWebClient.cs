using System;
using System.Diagnostics.CodeAnalysis;
using System.Net;

using NZBDash.Common.Interfaces;

namespace NZBDash.Common.Helpers
{
    [ExcludeFromCodeCoverage]
    public class CustomWebClient : IWebClient
    {
        private WebClient Client { get; set; }
        public CustomWebClient()
        {
            Client = new WebClient();
        }

        public WebHeaderCollection Headers
        {
            get
            {
                return Client.Headers;
            }
            set { Client.Headers = value; }
        }

        public string UploadString(string uri, string method, string data)
        {
            return Client.UploadString(uri, method, data);
        }

        public string DownloadString(string address)
        {
            return Client.DownloadString(address);
        }


        public void Dispose()
        {
            Client.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
