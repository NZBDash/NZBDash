#region Copyright
// /************************************************************************
//   Copyright (c) 2016 NZBDash
//   File: ThirdPartySerializer.cs
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
using System.IO;
using System.Xml.Serialization;

using Newtonsoft.Json;

using NZBDash.Common.Interfaces;

namespace NZBDash.Common.Helpers
{
    public class ThirdPartySerializer : ISerializer
    {
        private IHttpClient HttpClient { get; set; }

        public ThirdPartySerializer(IHttpClient client)
        {
            HttpClient = client;
        }
        public T SerializedJsonData<T>(string url) where T : new()
        {
            var jsonData = string.Empty;
            using (HttpClient)
            {
                try
                {
                    jsonData = HttpClient.DownloadString(url);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e);
                }

                return !string.IsNullOrEmpty(jsonData) ? JsonConvert.DeserializeObject<T>(jsonData) : new T();
            }
        }

        public T SerializedJsonData<T,U>(string url, string method, Func<U> body) where T : new()
        {
            var item = body();

            using (HttpClient)
            {
                try
                {
                    var result = HttpClient.UploadString(url, method, item.ToString());
                    return !string.IsNullOrEmpty(result) ? JsonConvert.DeserializeObject<T>(result) : new T();
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e);
                }
            }
        }

        public T SerializeXmlData<T>(string uri) where T : new()
        {
            using (HttpClient)
            {
                var data = HttpClient.DownloadString(uri);

                var serializer = new XmlSerializer(typeof(T));
                var rdr = new StringReader(data);
                return (T)serializer.Deserialize(rdr);
            }
        }
    }
}
