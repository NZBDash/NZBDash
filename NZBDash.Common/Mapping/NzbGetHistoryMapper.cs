using System;
using System.Linq;
using System.Reflection;

using NZBDash.Api.Models;

using Omu.ValueInjecter.Injections;
using Omu.ValueInjecter.Utils;

namespace NZBDash.Common.Mapping
{
    public class NzbGetHistoryMapper : KnownSourceInjection<NzbGetHistoryResult>
    {
        protected override void Inject(NzbGetHistoryResult source, object target)
        {
            MapMatchingProperties(target,source);

            var id = target.GetType().GetProperty("Id", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (id == null) return;

            var fileSize = target.GetType().GetProperty("FileSize", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (fileSize == null) return;

            var nzbName = target.GetType().GetProperty("NzbName", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (nzbName == null) return;

            var idVal = source.ID;
            if (idVal == default(int)) return;

            var fileSizeVal = source.FileSizeMB;
            if (fileSizeVal == default(int)) return;

            var nzbNameVal = source.NZBName;
            if (nzbNameVal ==null) return;

            id.SetValue(target, idVal);
            fileSize.SetValue(target, fileSizeVal);
            nzbName.SetValue(target, nzbNameVal);
        }

        private void MapMatchingProperties(object target, object source)
        {
            var sourceProps = source.GetType().GetProperties();
            var targetProps = target.GetType().GetProperties();

            for (var i = 0; i < targetProps.Count(); i++)
            {
                if (targetProps[i].Name.ToLowerInvariant() == sourceProps[i].Name.ToLowerInvariant())
                {
                    var objVal = GetPropValue(source, sourceProps[i].Name);

                    var fullProperty = target.GetType().GetProperty(targetProps[i].Name);
                    var newType = fullProperty.PropertyType;
                    object castType = null;
                    if (newType == typeof(int))
                    {
                        castType = (int)objVal;
                    }
                    if (newType == typeof(bool))
                    {
                        castType = (bool)objVal;
                    }
                    if (newType == typeof(string))
                    {
                        castType = (string)objVal;
                    }
                    if (newType == typeof(long))
                    {
                        castType = (long)objVal;
                    }

                    if(castType == null) continue;

                    var val = castType;
                    SetPropValue(targetProps[i], val, targetProps[i].Name);
                }
            }
        }

        public static object GetPropValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName).GetValue(src, null);
        }

        public static void SetPropValue(object target, object value, string propName)
        {
            var prop = target.GetType().GetProperty(propName);
            prop.SetValue(target, value);
        }
    }
}
