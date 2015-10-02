using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NZBDash.UI.Controllers
{
    public class SettingsController : Controller
    {
        // GET: Settings
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpdateNzbGetSettings()
        {
            return View();
        }

        public ActionResult UpdateSabNzbSettings()
        {
            return View();
        }

        public ActionResult UpdateSonarrSettings()
        {
            return View();
        }

        public ActionResult UpdateCouchPotatoSettings()
        {
            return View();
        }

        public ActionResult UpdatePlexSettings()
        {
            return View();
        }
    }
}