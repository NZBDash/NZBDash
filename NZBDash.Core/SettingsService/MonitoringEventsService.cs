#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: MonitoringEventsService.cs
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
using System.Linq;

using NZBDash.Common.Interfaces;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Models;
using NZBDash.DataAccessLayer.Interfaces;
using NZBDash.DataAccessLayer.Models;

using Omu.ValueInjecter;

namespace NZBDash.Core.SettingsService
{
    public class MonitoringEventsService : IEventService
    {
        public MonitoringEventsService(ISqlRepository<MonitoringEvents> repo, ILogger logger)
        {
            Repo = repo;
            Logger = logger;
        }

        private ILogger Logger { get; set; }
        private ISqlRepository<MonitoringEvents> Repo { get; set; }

        public long RecordEvent(MonitoringEventsDto dto)
        {
            var entity = new MonitoringEvents();
            entity.InjectFrom(dto);

            if (entity.EventEnd != DateTime.MinValue)
            {
                Logger.Trace("Removing existing Event record");

                var all = Repo.GetAll();
                Logger.Trace("We have all events count: {0}", all.Count());

                var startNotification = all.FirstOrDefault(x => x.EventStart == entity.EventStart);
                if (startNotification != null)
                {
                    Logger.Trace("We have found the old event to delete id: {0}", startNotification.Id);

                   Repo.Delete(startNotification);
                }
            }

            Logger.Trace("Inserting new event: Name: {0} | Type: {1}", entity.EventName, entity.EventType);
            return Repo.Insert(entity);
        }
    }
}