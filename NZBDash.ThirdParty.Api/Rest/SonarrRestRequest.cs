#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: SonarrRestRequest.cs
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
using System.Collections.Generic;

using NZBDash.ThirdParty.Api.Interfaces;
using NZBDash.ThirdParty.Api.Models.Api.Sonarr;

using RestSharp;

namespace NZBDash.ThirdParty.Api.Rest
{
    public class SonarrRestRequest
    {
        public SonarrRestRequest(IApiRequest request)
        {
            Api = request;
        }

        private IApiRequest Api { get; set; }
        private const string ApiKeyHeaderKey = " X-Api-Key";

        /// <summary>
        /// Gets the Sonarr Episodes
        /// </summary>
        /// <returns><see cref="SonarrEpisode"/></returns>
        public List<SonarrEpisode> GetSonarrEpisodes(string url, string seriesId, string apiKey)
        {
            var request = new RestRequest
            {
                Resource = "api/episode",
            };

            request.AddHeader(ApiKeyHeaderKey, apiKey);

            request.AddParameter("seriesId", seriesId);

            return Api.Execute<List<SonarrEpisode>>(request, new Uri(url));
        }

    }
}
