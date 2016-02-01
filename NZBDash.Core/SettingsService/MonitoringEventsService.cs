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

using NZBDash.Core.Interfaces;
using NZBDash.Core.Models;
using NZBDash.DataAccessLayer.Interfaces;
using NZBDash.DataAccessLayer.Models;

using Omu.ValueInjecter;

namespace NZBDash.Core.SettingsService
{
    public class MonitoringEventsService : IEventService
    {
        public MonitoringEventsService(ISqlRepository<MonitoringEvents> repo)
        {
            Repo = repo;
        }

        public ISqlRepository<MonitoringEvents> Repo { get; set; }

        public long RecordEvent(MonitoringEventsDto dto)
        {
            var entity = new MonitoringEvents();
            entity.InjectFrom(dto);

            if (entity.EventEnd != DateTime.MinValue)
            {
                var startNotification = Repo.Find(
                    () =>
                    {
                        var all = Repo.GetAll();
                        return all.FirstOrDefault(x => x.EventStart == entity.EventStart);
                    });

                Repo.Delete(startNotification);
            }

            return Repo.Insert(entity);
        }
    }
}