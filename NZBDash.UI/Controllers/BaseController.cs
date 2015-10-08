using System.Web.Mvc;

using NZBDash.Common;

namespace NZBDash.UI.Controllers
{
    public class BaseController : Controller
    {
        public readonly ILogger Logger = new NLogLogger();

        protected override void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;
            Logger.Fatal(exception);

            base.OnException(filterContext);
        }
    }
}