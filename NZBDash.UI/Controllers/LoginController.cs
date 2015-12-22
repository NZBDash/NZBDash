using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

using NZBDash.Core.Interfaces;

namespace NZBDash.UI.Controllers
{
    public class LoginController : Controller
    {
        private IMembershipProvider Provider { get; set; }
        public LoginController(IMembershipProvider mem)
        {
            Provider = mem;
        }

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Register()
        {
            MembershipCreateStatus status;
            var user = Provider.CreateUser("username", "pass", "email", "abv", "fff", true, null, out status);
            if (status.Equals(MembershipCreateStatus.Success))
            {
                return Json("success", JsonRequestBehavior.AllowGet);
            }

            return Json("bad", JsonRequestBehavior.AllowGet);
        }
    }
}