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
using System.Net;

using Newtonsoft.Json;

using NZBDash.Common.Interfaces;
using NZBDash.DataAccess;
using NZBDash.DataAccess.Api;
using NZBDash.ThirdParty.Api.Interfaces;
using NZBDash.ThirdParty.Api.Models.Api;

namespace NZBDash.ThirdParty.Api.Rest
{
    public class NzbGetRestRequest : BaseRequest, INzbGetRequest
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
            Logger.Trace("Getting NZBGet Download list");

            using (var webClient = new WebClient())
            {
                var jsonData = GenerateRpcBody("listgroups", null);
                var response = webClient.UploadString($"{url}{username}:{password}/jsonrpc/", "POST", jsonData);

                return JsonConvert.DeserializeObject<NzbGetList>(response);
            }
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
            Logger.Trace("Getting NZBGet status");

            using (var webClient = new WebClient())
            {
                var jsonData = GenerateRpcBody("status", null);
                var response = webClient.UploadString($"{url}{username}:{password}/jsonrpc/", "POST", jsonData);

                return JsonConvert.DeserializeObject<NzbGetStatus>(response);
            }
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
            Logger.Trace("Getting NZBGet history");

            using (var webClient = new WebClient())
            {
                var jsonData = GenerateRpcBody("history", null);
                var response = webClient.UploadString($"{url}{username}:{password}/jsonrpc/", "POST", jsonData);

                return JsonConvert.DeserializeObject<NzbGetHistory>(response);
            }
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
            Logger.Trace("Getting NZBGet logs");

            using (var webClient = new WebClient())
            {
                var jsonData = GenerateRpcBody("log", 0, 1000);
                var response = webClient.UploadString($"{url}{username}:{password}/jsonrpc/", "POST", jsonData);

                return JsonConvert.DeserializeObject<NzbGetLogs>(response);
            }
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

            using (var webClient = new WebClient())
            {
                var jsonData = pause ? GenerateRpcBody("pausedownload", null) : GenerateRpcBody("resumedownload");
                var response = webClient.UploadString($"{url}{username}:{password}/jsonrpc/", "POST", jsonData);

                var result = JsonConvert.DeserializeObject<NzbGetJsonRpcResponse>(response);
                return result.Result;
            }

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
            using (var webClient = new WebClient())
            {
                var body = GenerateRpcBody("rate", kbLimit);
                var response = webClient.UploadString($"{url}{username}:{password}/jsonrpc/","POST", body);

                var result = JsonConvert.DeserializeObject<NzbGetJsonRpcResponse>(response);
                return result.Result;
            }
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
            Logger.Info("Restarting NZBGet ");
            using (var webClient = new WebClient())
            {
                var body = GenerateRpcBody("reload", null);
                var response = webClient.UploadString($"{url}{username}:{password}/jsonrpc/", "POST", body);

                var result = JsonConvert.DeserializeObject<NzbGetJsonRpcResponse>(response);
                return result.Result;
            }
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
            Logger.Trace("Writing to NZBGet's Log");

            using (var webClient = new WebClient())
            {
                var body = GenerateRpcBody("writelog", kind.ToString(), message);
                var response = webClient.UploadString($"{url}{username}:{password}/jsonrpc/", "POST", body);

                var result = JsonConvert.DeserializeObject<NzbGetJsonRpcResponse>(response);
                return result.Result;
            }
        }

        /// <summary>
        /// Generates the JSON RPC body.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <param name="param">The parameter.</param>
        /// <returns></returns>
        private string GenerateRpcBody(string method, params object[] param)
        {
            var model = new NzbGetJsonRpc
            {
                Method = method,
                Params = param ?? new object[0]
            };

            return JsonConvert.SerializeObject(model);
        }
    }
}
