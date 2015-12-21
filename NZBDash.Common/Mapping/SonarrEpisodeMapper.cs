#region Copyright
//  ***********************************************************************
//  Copyright (c) 2015 Jamie Rees
//  File: SonarrEpisodeMapper.cs
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
using System;
using System.Globalization;
using System.Reflection;

using NZBDash.ThirdParty.Api.Models.Api.Sonarr;

using Omu.ValueInjecter.Injections;

namespace NZBDash.Common.Mapping
{
    public class SonarrEpisodeMapper : KnownSourceInjection<SonarrEpisode>
    {
        protected override void Inject(SonarrEpisode source, object target)
        {
            MappingHelper.MapMatchingProperties(target, source);

            var airDate = target.GetType().GetProperty("AirDate", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (airDate == null) return;

            var airDateUtc = target.GetType().GetProperty("AirDateUtc", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            if (airDateUtc == null) return;

            DateTime convertedAirDate;
            DateTime.TryParse(source.airDate, CultureInfo.InvariantCulture, DateTimeStyles.None, out convertedAirDate);
            if (convertedAirDate == default(DateTime)) return;

            DateTime convertedAirDateUtc;
            DateTime.TryParse(source.airDateUtc, CultureInfo.InvariantCulture, DateTimeStyles.None, out convertedAirDateUtc);
            if (convertedAirDateUtc == default(DateTime)) return;

            airDate.SetValue(target, convertedAirDate);
            airDateUtc.SetValue(target, convertedAirDateUtc);
        }
    }
}
