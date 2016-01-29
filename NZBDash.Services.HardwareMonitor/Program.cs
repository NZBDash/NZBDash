#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: Program.cs
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
// ************************************************************************/
#endregion
using System;

using Ninject;

using NZBDash.Common.Interfaces;
using NZBDash.Core;

using Topshelf;

namespace NZBDash.Services.HardwareMonitor
{
    internal class Program
    {
        private static ILogger Logger { get; set; }

        private static void Main(string[] args)
        {
            Setup();
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

            HostFactory.Run(
                x =>
                {
                    x.StartAutomatically();
                    x.Service<HardwareMonitor>(
                        s =>
                        {
                            s.ConstructUsing(monitor => new HardwareMonitor());
                            s.WhenStarted(tc => tc.Start());
                            s.WhenStopped(tc => tc.Stop());
                            s.AfterStartingService(() => { Logger.Info("Starting HardwareMonitor service"); });
                            s.AfterStoppingService(() => { Logger.Info("Stopping HardwareMonitor Service"); });
                        });
                    x.RunAsLocalSystem();
                    x.EnableServiceRecovery(r => { r.RestartService(1); });

                    x.SetDescription("NZBDash Monitor");
                    x.SetDisplayName("NZBDash Monitor");
                    x.SetServiceName("NZBDashMonitor");
                    x.UseNLog();
                });
        }

        private static void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Logger = ServiceKernel.GetKernel().Get<ILogger>();
            Logger.Fatal(e.ExceptionObject as Exception);
            if (e.IsTerminating)
            {
                Logger.Info("Application is terminating due to an unhandled exception");
            }
        }

        private static void Setup()
        {
            var k = ServiceKernel.GetKernel();
            Logger = k.Get<ILogger>();
            var setup = k.Get<ISetup>();
            setup.Start();
            setup.SetupMappers();
        }
    }
}