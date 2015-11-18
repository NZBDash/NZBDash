using System;
using System.Web.Mvc;

using NZBDash.Common;
using NZBDash.Common.Interfaces;

namespace NZBDash.UI.Controllers
{
    public class BaseController : Controller
    {
        public ILogger Logger { get; set; }

        public BaseController()
        {
            
        }
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

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var actionName = filterContext.ActionDescriptor.ActionName;
            var controller = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            Logger.Trace(string.Format("Executing action: {0}, Controller: {1}", actionName, controller));
            base.OnActionExecuting(filterContext);
        }
    }
}