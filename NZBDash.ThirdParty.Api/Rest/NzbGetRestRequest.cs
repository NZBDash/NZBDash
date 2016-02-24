#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: NzbGetRestRequest.cs
//   Created By: Jamie Rees
//  
//   Permission is hereby granted, free of charge, to any person obtaining
//   a copy of this software and associated documentation files (the
//   "Software"), to deal in the Software without restriction, including
//   without limitation the rights to use, copy, modify, merge, publish,
//   distribute, sublicense, and/or sell copies of the Software, and to
//   permit persons to whom the Software is furnished to do so, subject to
//   the following conditions:
//  
//   The above copyright notice and this permission notice shall be
//   included in all copies or substantial portions of the Software.
//  
//   THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//   EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//   MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//   NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//   LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//   OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//   WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ************************************************************************/
#endregion
using System;

using NZBDash.Common.Interfaces;
using NZBDash.DataAccess;
using NZBDash.ThirdParty.Api.Interfaces;
using NZBDash.ThirdParty.Api.Models.Api;

using RestSharp;

namespace NZBDash.ThirdParty.Api.Rest
{
    public class NzbGetRestRequest : BaseRequest
    {
        public NzbGetRestRequest(IApiRequest request, ILogger logger) : base(request, logger)
        {
        }

        /// <summary>
        /// Get the NZBGet List
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns>
        ///   <see cref="NzbGetList" />
        /// </returns>
        public NzbGetList GetNzbGetList(string url, string username, string password)
        {
            var request = new RestRequest
            {
                Resource = "{username}:{password}/jsonrpc/listgroups",
                Method = Method.GET
            };
            request.AddUrlSegment("username", username);
            request.AddUrlSegment("password", password);

            return Api.Execute<NzbGetList>(request, new Uri(url));
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public NzbGetStatus GetStatus(string url, string username, string password)
        {
            var request = new RestRequest
            {
                Resource = "{username}:{password}/jsonrpc/status",
                Method = Method.GET
            };
            request.AddUrlSegment("username", username);
            request.AddUrlSegment("password", password);

            return Api.Execute<NzbGetStatus>(request, new Uri(url));
        }

        /// <summary>
        /// Gets the history.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public NzbGetHistory GetHistory(string url, string username, string password)
        {
            var request = new RestRequest
            {
                Resource = "{username}:{password}/jsonrpc/history",
                Method = Method.GET
            };
            request.AddUrlSegment("username", username);
            request.AddUrlSegment("password", password);

            return Api.Execute<NzbGetHistory>(request, new Uri(url));
        }

        /// <summary>
        /// Gets the logs.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public NzbGetLogs GetLogs(string url, string username, string password)
        {
            var request = new RestRequest
            {
                Resource = "{username}:{password}/jsonrpc/log?IDFrom=0&NumberOfEntries=1000",
                Method = Method.GET
            };
            request.AddUrlSegment("username", username);
            request.AddUrlSegment("password", password);

            return Api.Execute<NzbGetLogs>(request, new Uri(url));
        }
        

        /// <summary>
        /// Sets the download status.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="pause">if set to <c>true</c> [pause].</param>
        /// <returns></returns>
        public bool SetDownloadStatus(string url, string username, string password, bool pause)
        {
            var request = new RestRequest
            {
                Resource = pause ? "{username}:{password}/jsonrpc/pausedownload" : "{username}:{password}/jsonrpc/resumedownload",
                Method = Method.GET
            };

            request.AddUrlSegment("username", username);
            request.AddUrlSegment("password", password);

            return Api.Execute<bool>(request, new Uri(url));
        }

        /// <summary>
        /// Sets the download limit.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="kbLimit">The kb limit.</param>
        /// <returns></returns>
        public bool SetDownloadLimit(string url, string username, string password, int kbLimit)
        {
            var request = new RestRequest
            {
                Resource = "{username}:{password}/jsonrpc/rate",
                Method = Method.GET
            };

            request.AddUrlSegment("username", username);
            request.AddUrlSegment("password", password);
            request.AddParameter("Limit", kbLimit);

            return Api.Execute<bool>(request, new Uri(url));
        }

        /// <summary>
        /// Restarts the specified URL.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public bool Restart(string url, string username, string password)
        {
            var request = new RestRequest
            {
                Resource = "{username}:{password}/jsonrpc/reload",
                Method = Method.GET
            };

            request.AddUrlSegment("username", username);
            request.AddUrlSegment("password", password);

            return Api.Execute<bool>(request, new Uri(url));
        }

        /// <summary>
        /// Writes the log.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="kind">The kind.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public bool WriteLog(string url, string username, string password, NzbLogType kind, string message)
        {
            var request = new RestRequest
            {
                Resource = "{username}:{password}/jsonrpc/reload",
                Method = Method.GET
            };

            request.AddUrlSegment("username", username);
            request.AddUrlSegment("password", password);
            request.AddParameter("Kind", kind.ToString());
            request.AddParameter("Text", message);

            return Api.Execute<bool>(request, new Uri(url));
        }
    }
}
