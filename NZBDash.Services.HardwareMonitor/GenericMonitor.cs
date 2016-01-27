using System;
using System.Web.Hosting;

using FluentScheduler;

using NZBDash.Services.HardwareMonitor.Interfaces;
using NZBDash.Services.HardwareMonitor.Observers;

namespace NZBDash.Services.HardwareMonitor
{
    class GenericMonitor<T,U> : ITask where T : IMonitorObservable, ITask, IRegisteredObject, new() where U : IAlertingObserver, new()
    {
        public void Execute()
        {
            var monitor = new T();
            var alertObserver = new U();
            monitor.StartAlert += alertObserver.HandleStartAlert;
            monitor.EndAlert += alertObserver.HandleEndAlert;


            monitor.StartMonitoring();
        }



        public void Stop(bool immediate)
        {
            throw new NotImplementedException();
        }
    }
}
