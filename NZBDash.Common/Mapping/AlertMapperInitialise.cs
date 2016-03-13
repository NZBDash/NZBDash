#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: AlertMapperInitialise.cs
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

using System.Collections.Generic;
using System.Linq;
using NZBDash.Core.Models.Settings;
using NZBDash.DataAccessLayer.Models.Settings;
using NZBDash.UI.Models.ViewModels.Settings;
using Omu.ValueInjecter;
using AlertRules = NZBDash.UI.Models.ViewModels.Settings.AlertRules;
using AlertType = NZBDash.UI.Models.ViewModels.Settings.AlertType;

namespace NZBDash.Common.Mapping
{
    public class AlertMapperInitialise
    {
        static AlertMapperInitialise()
        {
            // View to DTO
            Mapper.AddMap<AlertSettingsViewModel, AlertSettingsDto>(x =>
            {
                //var settings = new AlertSettingsDto
                //{
                //    AlertRules = x.AlertRules
                //        .Select(c => new AlertRulesDto().InjectFrom(c)).Cast<AlertRulesDto>()
                //        .ToList()
                //};

                var settings = new AlertSettingsDto();
                settings.InjectFrom(x);
                foreach (var a in x.AlertRules)
                {
                    var newA = new AlertRulesDto();
                    newA.InjectFrom(a);
                    newA.AlertType = (AlertTypeDto)(int)a.AlertType;
                    settings.AlertRules.Add(newA);
                }

                return settings;
            });

            // DTO to Entity
            Mapper.AddMap<AlertSettingsDto, AlertSettings>(x =>
            {
                //var settings = new AlertSettings
                //{
                //    AlertRules = x.AlertRules
                //        .Select(c => new DataAccessLayer.Models.Settings.AlertRules().InjectFrom(c)).Cast<DataAccessLayer.Models.Settings.AlertRules> ()
                //        .ToList()
                //};

                var settings = new AlertSettings { AlertRules = new List<DataAccessLayer.Models.Settings.AlertRules>()};
                settings.InjectFrom(x);
                foreach (var a in x.AlertRules)
                {
                    var newA = new DataAccessLayer.Models.Settings.AlertRules();
                    newA.InjectFrom(a);
                    newA.AlertType = (DataAccessLayer.Models.Settings.AlertType)(int)a.AlertType;
                    settings.AlertRules.Add(newA);
                }

                return settings;
            });

            // Entity to DTO
            Mapper.AddMap<AlertSettings, AlertSettingsDto>(x =>
            {
                var settings = new AlertSettingsDto { AlertRules = new List<AlertRulesDto>() };
                settings.InjectFrom(x);
                //foreach (var entityAr in x.AlertRules)
                //{
                //    settings.AlertRules.Add((AlertRulesDto)new AlertRulesDto().InjectFrom(entityAr));
                //}
                foreach (var entityAr in x.AlertRules)
                {
                    var newA = new AlertRulesDto();
                    newA.InjectFrom(entityAr);
                    newA.AlertType = (AlertTypeDto)(int)entityAr.AlertType;
                    settings.AlertRules.Add(newA);
                }

                return settings;
            });


            // DTO to View
            Mapper.AddMap<AlertSettingsDto, AlertSettingsViewModel>(x =>
            {
                //var settings = new AlertSettingsViewModel
                //{
                //    AlertRules = x.AlertRules
                //        .Select(c => new AlertRules().InjectFrom(c)).Cast<AlertRules>()
                //        .ToList()
                //};

                var settings = new AlertSettingsViewModel();
                settings.InjectFrom(x);
                foreach (var entityAr in x.AlertRules)
                {
                    var newA = new AlertRules();
                    newA.InjectFrom(entityAr);
                    newA.AlertType = (AlertType)(int)entityAr.AlertType;
                    settings.AlertRules.Add(newA);
                }

                return settings;
            });


            // View to DTO
            Mapper.AddMap<AlertRules, AlertRulesDto>(x =>
            {
                var dto = new AlertRulesDto();
                dto.InjectFrom(x);
                dto.AlertType = (AlertTypeDto)(int)x.AlertType;
                return dto;
            });

            // DTO to Entity
            Mapper.AddMap<AlertRulesDto, DataAccessLayer.Models.Settings.AlertRules>(x =>
            {
                var dto = new DataAccessLayer.Models.Settings.AlertRules();
                dto.InjectFrom(x);
                dto.AlertType = (DataAccessLayer.Models.Settings.AlertType)(int)x.AlertType;
                return dto;
            });


            // Entity to DTO
            Mapper.AddMap<DataAccessLayer.Models.Settings.AlertRules, AlertRulesDto>(x =>
            {
                var dto = new AlertRulesDto();
                dto.InjectFrom(x);
                dto.AlertType = (AlertTypeDto)(int)x.AlertType;
                return dto;
            });

            // DTO to View
            Mapper.AddMap<AlertRulesDto, AlertRules>(x =>
            {
                var dto = new AlertRules();
                dto.InjectFrom(x);
                dto.AlertType = (AlertType)(int)x.AlertType;
                return dto;
            });
        }
    }
}
