#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: BaseController.cs
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
using System;
using System.Web.Mvc;

using Ninject;

using NZBDash.Common;
using NZBDash.Common.Interfaces;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.UI.App_Start;
using NZBDash.UI.Helpers;

namespace NZBDash.UI.Controllers
{
    public class BaseController : Controller
    {
        public ILogger Logger { get; set; }

        public BaseController() { }

        [Obsolete("Should not use this ctor, we should be using the IoC container to pass in the ILogger interface")]
        public BaseController(Type classType)
        {
            Logger = new NLogLogger(classType);
        }

        public BaseController(ILogger classType)
        {
            Logger = classType;
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            var exception = filterContext.Exception;
            Logger.Fatal(exception);
            base.OnException(filterContext);
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            // TODO: Swap out the service locator with something else
            var kernel = NinjectWebCommon.GetKernel();
            var authHelper = new AuthenticationHelper(kernel.Get<ISettingsService<NzbDashSettingsDto>>());

            var shouldBeAuth = authHelper.IsAuthenticated();

            if (!User.Identity.IsAuthenticated && shouldBeAuth)
            {
                var redirect = RedirectToAction("Login", "Account");
                var url = Url.RouteUrl(redirect.RouteName, redirect.RouteValues);
                filterContext.Result = new RedirectResult(url);
                return;
            }

            base.OnActionExecuting(filterContext);
        }
    }
}