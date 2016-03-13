#region Copyright
//  ***********************************************************************
//  Copyright (c) 2016 Jamie Rees
//  File: CustomDependencyResolverTest.cs
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
using System.Linq;

using Ninject;
using Ninject.Modules;

using NUnit.Framework;

using NZBDash.Common.Interfaces;
using NZBDash.Core.Interfaces;
using NZBDash.DependencyResolver.Modules;

using Octokit;

namespace NZBDash.DependencyResolver.Test
{
    [TestFixture]
    public class CustomDependencyResolverTest
    {
        [Test]
        public void ResolveServiceDependency()
        {
            var module = new ServiceModule();
            var kernal = new StandardKernel(module);

            var service = kernal.Get<IHardwareService>();
            Assert.That(service,Is.Not.Null);
        }

        [Test]
        public void ResolveSerializerDependency()
        {
            var module = new SerializerModule();
            var webClientModule = new ServiceModule();
            var kernal = new StandardKernel(module, webClientModule);

            var service = kernal.Get<ISerializer>();
            Assert.That(service, Is.Not.Null);
        }

        [Test]
        public void ResolveLoggerDependency()
        {
            var module = new LoggerModule();
            var kernal = new StandardKernel(module);
            var service = kernal.Get<TestClass>();
            Assert.That(service, Is.Not.Null);
        }

        [Test]
        public void ResolveCacheDependency()
        {
            var module = new CacheModule();
            var kernal = new StandardKernel(module);

            var service = kernal.Get<ICacheProvider>();
            Assert.That(service, Is.Not.Null);
        }

        [Test]
        public void ResolveWrapperDependency()
        {
            var module = new WrapperModule();
            var kernal = new StandardKernel(module);

            var service = kernal.Get<ISmtpClient>();
            Assert.That(service, Is.Not.Null);
        }

        [Test]
        public void ResolveUpdaterDependency()
        {
            var module = new UpdaterModule();
            var kernal = new StandardKernel(module);

            var service = kernal.Get<IGitHubClient>();
            Assert.That(service, Is.Not.Null);
        }

        [Test]
        public void GetCustomModules()
        {
            IDependencyResolver<NinjectModule> customResolver = new CustomDependencyResolver();

            var modules = customResolver.GetModules();

            Assert.That(modules.Count, Is.GreaterThan(0));
            Assert.That(modules, Is.TypeOf(typeof(NinjectModule[])));

            foreach (var m in modules)
            {
                Assert.That(m.GetType().BaseType, Is.EqualTo(typeof(NinjectModule)));
            }
        }
    }
    public class TestClass
    {
        private readonly ILogger m_Logger;

        public TestClass(ILogger logger){}
    }
}
