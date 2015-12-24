﻿#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: SabNzbSettingsService.cs
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

using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.Settings;
using NZBDash.DataAccessLayer.Interfaces;
using NZBDash.DataAccessLayer.Models.Settings;

using Omu.ValueInjecter;

namespace NZBDash.Core.SettingsService
{
    public class SabNzbSettingsService : ISettingsService<SabNzbdSettingsDto>
    {
        private ISqlRepository<SabNzbSettings> Repo { get; set; }
        public SabNzbSettingsService(ISqlRepository<SabNzbSettings> repo)
        {
            Repo = repo;
        }

        public SabNzbdSettingsDto GetSettings()
        {
            var result = Repo.GetAll();
            var setting = result.FirstOrDefault();
            if (setting == null)
            {
                return new SabNzbdSettingsDto();
            }

            var model = new SabNzbdSettingsDto();
            model.InjectFrom(setting);

            return model;
        }

        public bool SaveSettings(SabNzbdSettingsDto model)
        {
            var entity = Repo.Get(model.Id);

            if (entity == null)
            {
                var newEntity = new SabNzbSettings();
                newEntity.InjectFrom(model);
                
                var insertResult = Repo.Insert(newEntity);
                return insertResult != long.MinValue;
            }

            entity.Enabled = model.Enabled;
            entity.IpAddress = model.IpAddress;
            entity.Port = model.Port;
            entity.ApiKey = model.ApiKey;
            entity.Id = model.Id;
            entity.ShowOnDashboard = model.ShowOnDashboard;

            var result = Repo.Update(entity);

            return result;
        }
    }
}
