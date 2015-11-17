using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NZBDash.UI.Controllers.Application
{
    public class SonarrController : BaseController
    {
        public SonarrController()
            : base(typeof(SonarrController))
        {
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}