using System;

using NZBDash.Services.HardwareMonitor.Interfaces;

namespace NZBDash.Services.HardwareMonitor.Observers
{
    class AlertingObserver : IAlertingObserver
    {
        private AlertModel AlertModel { get; set; }
        public void HandleStartAlert(object sender, EventArgs args)
        {
            var model = (ThresholdModel)sender;
            AlertModel = new AlertModel { BreachEnd = model.BreachEnd, BreachStart = model.BreachStart, TimeThresholdSec = model.TimeThresholdSec, Percentage = model.Percentage };
            Console.WriteLine("START ALERT!");
            Console.WriteLine("START Breach Time: {0}", AlertModel.BreachStart);
        }

        public void HandleEndAlert(object sender, EventArgs args)
        {
            var model = (ThresholdModel)sender;
            AlertModel.BreachEnd = model.BreachEnd;
            Console.WriteLine("END ALERT!");
            Console.WriteLine("END Breach Time: {0}", AlertModel.BreachEnd);
            Console.WriteLine("END Duration: {0}", AlertModel.Duration.ToString("g"));
        }
    }

    internal interface IAlertingObserver
    {
        void HandleStartAlert(object sender, EventArgs args);
        void HandleEndAlert(object sender, EventArgs args);
    }
}
