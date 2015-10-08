using System;
using System.Web.Mvc;

using NZBDash.Common;

namespace NZBDash.UI.Controllers
{
    public class BaseController : Controller
    {
        public ILogger Logger { get; set; }

        public BaseController(Type classType)
        {
            Logger = new NLogLogger(classType);
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;
            Logger.Fatal(exception);

            base.OnException(filterContext);
        }
    }
}