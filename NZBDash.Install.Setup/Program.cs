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
using System.Diagnostics;

using Microsoft.Web.Administration;

namespace NZBDash.Install.Setup
{
    class Program
    {
        static void Main(string[] args)
        {

            // Args expected:
            // 1. Install Location
            // 2. Install or Uninstall
            // 3. Port number

            //if (args.Length != 3)
            //{
            //    Console.WriteLine("Number of arguments is incorrect, required 3:");
            //    Console.WriteLine("1. Install Location");
            //    Console.WriteLine("2. Install or Uninstall");
            //    Console.WriteLine("3. Port number");
            //    Console.ReadLine();
            //    Environment.Exit(1);
            //}

            Trace.TraceInformation("test");
            try
            {
                // https://blogs.msdn.microsoft.com/carlosag/2006/04/17/microsoft-web-administration-in-iis-7/
                var iisManager = new ServerManager();
                //iisManager.Sites.Add("AA", "http", "*:8080:", @"C:\Users\Jamie.Rees\Source\Repos\NZBDash\NZBDash.UI"); // TODO: Do all of this.
                //iisManager.CommitChanges();

                iisManager.ApplicationPools.Add("AATest");
                
                iisManager.Sites["AA"].Applications.Add("/AA", @"C:\Users\Jamie.Rees\Source\Repos\NZBDash\NZBDash.UI");
                iisManager.Sites["AA"].ApplicationDefaults.ApplicationPoolName = "AATest";

                iisManager.CommitChanges();
                Console.ReadLine();
                //Nothing seems to be working

            }
            catch (Exception e)
            {
                Trace.TraceError(e.Message);
                throw;
            }


        }
    }
}
