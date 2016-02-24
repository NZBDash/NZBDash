
using NZBDash.ThirdParty.Api.Models.Api;
#region Copyright
//  ***********************************************************************
//  Copyright (c) 2016 Jamie Rees
//  File: IThirdPartyService.cs
//  Created By: Jamie Rees
//
//  Permission is hereby granted, free of charge, to any person obtaining
//  a copy of this software and associated documentation files (the
//  "Software"), to deal in the Software without restriction, including
//  without limitation the rights to use, copy, modify, merge, publish,
//  distribute, sublicense, and/or sell copies of the Software, and to
//  permit persons to whom the Software is furnished to do so, subject to
//  the following conditions:
//
//  The above copyright notice and this permission notice shall be
//  included in all copies or substantial portions of the Software.
//
//  THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
//  EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
//  MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
//  NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
//  LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
//  OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
//  WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//  ***********************************************************************
#endregion

using System.Collections.Generic;
using NZBDash.DataAccess.Api.CouchPotato;
using NZBDash.ThirdParty.Api.Models.Api.CouchPotato;
using NZBDash.ThirdParty.Api.Models.Api.SabNzbd;
using NZBDash.ThirdParty.Api.Models.Api.Sonarr;

namespace NZBDash.ThirdParty.Api.Interfaces
{
    public interface IThirdPartyService
    {
        CouchPotatoMediaList GetCouchPotatoMovies(string uri, string api);
        PlexServers GetPlexServers(string uri);
        List<SonarrSeries> GetSonarrSeries(string uri, string api);
        List<SonarrEpisode> GetSonarrEpisodes(string uri, string api, int seriesId);
        bool SonarrEpisodeSearch(string url, string apiKey, int episodeId);
        SonarrSystemStatus GetSonarrSystemStatus(string uri, string api);
        CouchPotatoStatus GetCouchPotatoStatus(string uri, string api);
        NzbGetHistory GetNzbGetHistory(string url, string username, string password);
        NzbGetList GetNzbGetList(string url, string username, string password);
        NzbGetStatus GetNzbGetStatus(string url, string username, string password);
        NzbGetLogs GetNzbGetLogs(string url, string username, string password);
        SabNzbdHistory GetSabNzbdHistory(string url, string apiKey);
        SabNzbdQueue GetSabNzbdQueue(string url, string apiKey);
    }
}