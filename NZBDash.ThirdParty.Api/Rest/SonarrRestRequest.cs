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

using NZBDash.Common.Interfaces;
using NZBDash.DataAccess.Api.Sonarr;
using NZBDash.ThirdParty.Api.Interfaces;
using NZBDash.ThirdParty.Api.Models.Api.Sonarr;

using RestSharp;

namespace NZBDash.ThirdParty.Api.Rest
{
    public class SonarrRestRequest : BaseRequest, ISonarrRequest
    {
        public SonarrRestRequest(IApiRequest request, ILogger logger) : base(request, logger)
        {
            Api = request;
        }
        
        private const string ApiKeyHeader = " X-Api-Key";

        /// <summary>
        /// Gets the Sonarr Episodes
        /// </summary>
        /// <returns><see cref="SonarrEpisode"/></returns>
        public List<SonarrEpisode> GetEpisodes(string url, string seriesId, string apiKey)
        {
            var request = new RestRequest
            {
                Resource = "api/episode",
                Method = Method.GET
            };

            request.AddHeader(ApiKeyHeader, apiKey);
            request.AddParameter("seriesId", seriesId);

            return Api.Execute<List<SonarrEpisode>>(request, new Uri(url));
        }

        /// <summary>
        /// Gets all series in this Sonarr instance.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="apiKey">The API key.</param>
        /// <returns></returns>
        public List<SonarrSeries> GetSeries(string url, string apiKey)
        {
            var request = new RestRequest
            {
                Resource = "api/series",
                Method = Method.GET
            };

            request.AddHeader(ApiKeyHeader, apiKey);

            return Api.Execute<List<SonarrSeries>>(request, new Uri(url));
        }

        /// <summary>
        /// Add's an Episode and downloads the episode on the local Sonarr instance
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="episodeId">The episode identifier.</param>
        /// <param name="apiKey">The API key.</param>
        /// <returns>bool</returns>
        public bool AddEpisode(string url, string episodeId, string apiKey)
        {
            var request = new RestRequest
            {
                Resource = "api/command",
                Method = Method.POST
            };

            request.AddHeader(ApiKeyHeader, apiKey);
            request.AddBody("{ name: 'EpisodeSearch', episodeIds: [" + episodeId + "]}");

            var result = Api.Execute<SonarrCommand>(request, new Uri(url));
            return !string.IsNullOrEmpty(result.status); // TODO need to actually check what the status is.
        }

        /// <summary>
        /// Gets the sonarr system status.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="apiKey">The API key.</param>
        /// <returns>SonarrSystemStatus</returns>
        public SonarrSystemStatus GetStatus(string url, string apiKey)
        {
            var request = new RestRequest
            {
                Resource = "api/system/status",
                Method = Method.GET
            };

            request.AddHeader(ApiKeyHeader, apiKey);

            return Api.Execute<SonarrSystemStatus>(request, new Uri(url));
        }
    }
}
