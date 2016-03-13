#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: CouchPotatoRestRequest.cs
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
using NZBDash.DataAccess.Api.CouchPotato;
using NZBDash.ThirdParty.Api.Interfaces;
using NZBDash.ThirdParty.Api.Models.Api.CouchPotato;

using RestSharp;

namespace NZBDash.ThirdParty.Api.Rest
{
    public class CouchPotatoRestRequest : BaseRequest, ICouchPotatoRequest
    {
        public CouchPotatoRestRequest(IApiRequest request, ILogger logger) : base(request, logger)
        {
        }
        
        /// <summary>
        /// Gets the movies currently in CouchPotato.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="apiKey">The API key.</param>
        /// <returns></returns>
        public CouchPotatoMediaList GetMovies(string url, string apiKey)
        {
            Logger.Trace("Getting movies from CP with ApiKey {0}", apiKey);
            var request = new RestRequest
            {
                Resource = "api/{key}/media.list/",
                Method = Method.GET
            };

            request.AddUrlSegment("key", apiKey);

            return Api.Execute<CouchPotatoMediaList>(request, new Uri(url));
        }

        /// <summary>
        /// Gets the status.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="apiKey">The API key.</param>
        /// <returns></returns>
        public CouchPotatoStatus GetStatus(string url, string apiKey)
        {
            Logger.Trace("Getting CP Status, ApiKey = {0}", apiKey);
            var request = new RestRequest
            {
                Resource = "api/{key}/app.available/",
                Method = Method.GET
            };

            request.AddUrlSegment("key", apiKey);

            return Api.Execute<CouchPotatoStatus>(request, new Uri(url));
        }
    }
}
