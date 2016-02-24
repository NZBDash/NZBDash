#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: SabNzbdRestRequest.cs
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
using NZBDash.ThirdParty.Api.Interfaces;
using NZBDash.ThirdParty.Api.Models.Api.SabNzbd;

using RestSharp;

namespace NZBDash.ThirdParty.Api.Rest
{
    public class SabNzbdRestRequest : BaseRequest
    {
        public SabNzbdRestRequest(IApiRequest request, ILogger logger) : base(request, logger)
        {
            Api = request;
            Request = new RestRequest { Resource = "api" };
        }
        
        private IRestRequest Request { get; set; }

        private const string ApiKeyParam = "apikey";
        private const string OutputParam = "output";
        private const string OutputType = "json";

        /// <summary>
        /// Get the History
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="apiKey">The API key.</param>
        /// <returns>
        ///   <see cref="SabNzbdHistory" />
        /// </returns>
        public SabNzbdHistory GetHistory(string url, string apiKey)
        {
            //api?mode=history&start=0&limit=10&output=json&apikey=
            Request.Method = Method.GET;

            AddRequiredParameters(apiKey);
            Request.AddParameter("mode", "history", ParameterType.QueryString);
            Request.AddParameter("limit", "10", ParameterType.QueryString);
           
            return Api.Execute<SabNzbdHistory>(Request, new Uri(url));
        }

        /// <summary>
        /// Gets the queue.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="apiKey">The API key.</param>
        /// <returns>
        /// <see cref="SabNzbdQueue" /></returns>
        public SabNzbdQueue GetQueue(string url, string apiKey)
        {
            // api?mode=qstatus&output=json&apikey=
            Request.Method = Method.GET;

            AddRequiredParameters(apiKey);
            Request.AddParameter("mode", "qstatus", ParameterType.QueryString);
            
            return Api.Execute<SabNzbdQueue>(Request, new Uri(url));
        }

        /// <summary>
        /// Adds the required parameters.
        /// </summary>
        /// <param name="apiKey">The API key.</param>
        private void AddRequiredParameters(string apiKey)
        {
            Request.AddParameter(OutputParam, OutputType, ParameterType.QueryString);
            Request.AddParameter(ApiKeyParam, apiKey, ParameterType.QueryString);
        }
    }
}
