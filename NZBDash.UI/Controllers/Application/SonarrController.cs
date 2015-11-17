using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using NZBDash.Api.Controllers;

namespace NZBDash.UI.Controllers.Application
{
    public class SonarrController : BaseController
    {
        private IThirdPartyService ApiService { get; set; }
        public SonarrController(IThirdPartyService apiService)
            : base(typeof(SonarrController))
        {
            ApiService = apiService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}