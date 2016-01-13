#region Copyright
// ************************************************************************
//   Copyright (c) 2016 
//   File: CouchPotatoController.cs
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
// ************************************************************************
#endregion

using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using NZBDash.Common.Interfaces;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.ThirdParty.Api.Interfaces;
using NZBDash.UI.Models.ViewModels.CouchPotato;

namespace NZBDash.UI.Controllers.Application
{
    public class CouchPotatoController : BaseController
    {
        public CouchPotatoController(ISettingsService<CouchPotatoSettingsDto> settingsService, IThirdPartyService api, ILogger logger, ICacheProvider cache)
        {
            SettingsService = settingsService;
            Api = api;
            Logger = logger;
            Settings = SettingsService.GetSettings();
            Cache = cache;
        }

        private ISettingsService<CouchPotatoSettingsDto> SettingsService { get; set; }
        private CouchPotatoSettingsDto Settings { get; set; }
        private IThirdPartyService Api { get; set; }
        private ICacheProvider Cache { get; set; }
        private string MoviesCacheKey = "CPMovies";


        // GET: CouchPotato
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Movies()
        {
            var movies = Cache.GetOrSet(MoviesCacheKey,() => Api.GetCouchPotatoMovies(Settings.Uri.ToString(), Settings.ApiKey));
            var vm = movies.movies.Select(mov => new CouchPotatoMoviesViewModel
            {
                MovieName = mov.title
            }).ToList();

            return PartialView(vm);
        }
    }
}