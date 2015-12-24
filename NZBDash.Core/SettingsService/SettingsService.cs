#region Copyright
// /************************************************************************
//   Copyright (c) 2015 Jamie Rees
//   File: SettingsService.cs
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
using System.Runtime.Serialization.Formatters;

using Newtonsoft.Json;

using NZBDash.Common.Interfaces;
using NZBDash.Common.Models.Settings;
using NZBDash.Core.Interfaces;
using NZBDash.DataAccessLayer.Interfaces;
using NZBDash.DataAccessLayer.Models.Settings;

using Omu.ValueInjecter;

namespace NZBDash.Core.SettingsService
{
    public class SettingsService<T, U> : ISettingsService<U>
        where T : Entity, new()
        where U : Setting, new()
    {
        public SettingsService(ISettingsRepository repo, ILogger logger)
        {
            Logger = logger;
            Repo = repo;
            EntityName = typeof(T).Name;
        }
        private static readonly JsonSerializerSettings SerializerSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            TypeNameHandling = TypeNameHandling.Objects,
            TypeNameAssemblyFormat = FormatterAssemblyStyle.Simple,
            NullValueHandling = NullValueHandling.Ignore
        };
        private ILogger Logger { get; set; }
        private ISettingsRepository Repo { get; set; }
        private string EntityName { get;set; }

        public U GetSettings()
        {
            try
            {
                Logger.Trace("Getting all items from NzbDashSettingsService");
                var result = Repo.Get(EntityName);
                if (result == null)
                {
                    Logger.Trace("There are no items returned from NzbDashRepository. Returning new empty DTO");
                    return new U();
                }

                var obj = result == null || string.IsNullOrEmpty(result.Content) ? null : JsonConvert.DeserializeObject<T>(result.Content, SerializerSettings);

                Logger.Trace("Creating dto from the results from NzbDashRepository");
                var model = new U();
                model.InjectFrom(obj);

                return model;
            }
            catch (Exception e)
            {
                Logger.Fatal(e);
                throw new Exception(e.Message, e);
            }
        }

        public bool SaveSettings(U model)
        {
            Logger.Trace(string.Format("Looking for id {0} in the {1}", model.Id, typeof(U).Name));
            var entity = Repo.Get(EntityName);

            if (entity == null)
            {
                Logger.Trace("Our entity is null so we are going to insert one");
                var newEntity = new T();
                newEntity.InjectFrom(model);

                Logger.Trace("Inserting now");
                var a = new GlobalSettings { SettingsName = EntityName, Content = JsonConvert.SerializeObject(newEntity, SerializerSettings) };

                var insertResult = Repo.Insert(a);

                Logger.Trace(string.Format("Our insert was {0}", insertResult != long.MinValue));
                return insertResult != long.MinValue;
            }

            var modified = new T();
            modified.InjectFrom(model);
            var b = new GlobalSettings { SettingsName = typeof(U).Name, Content = JsonConvert.SerializeObject(modified, SerializerSettings) };

            var result = Repo.Update(b);

            Logger.Trace(string.Format("Our modify was {0}", result));
            return result;
        }
    }
}