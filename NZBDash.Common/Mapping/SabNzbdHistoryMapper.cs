#region Copyright
//  ***********************************************************************
//  Copyright (c) 2016 Jamie Rees
//  File: SabNzbdHistoryMapper.cs
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
using System.Globalization;
using System.Reflection;

using NZBDash.Common.Helpers;
using NZBDash.ThirdParty.Api.Models.Api.SabNzbd;

using Omu.ValueInjecter.Injections;

namespace NZBDash.Common.Mapping
{
    public class SabNzbdHistoryMapper : KnownSourceInjection<Slot>
    {
        protected override void Inject(Slot source, object target)
        {
            MappingHelper.MapMatchingProperties(target, source);

            var fileSize = target.GetType().GetProperty("FileSize", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);
            var nzbName = target.GetType().GetProperty("NzbName", BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (fileSize == null) return;
            if (nzbName == null) return;

            var fileSizeVal = MemorySizeConverter.ConvertToMb(source.size);
            if (fileSizeVal == default(double)) return;


            fileSize.SetValue(target, fileSizeVal.ToString(CultureInfo.CurrentUICulture));
            nzbName.SetValue(target, source.nzb_name);
        }
    }
}
