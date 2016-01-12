using System;
using System.IO;
using System.Net;
using System.Xml.Serialization;

using Newtonsoft.Json;

using NZBDash.Common.Interfaces;

namespace NZBDash.Common.Helpers
{
    public class ThirdPartySerializer : ISerializer
    {
        private IWebClient WebClient { get; set; }

        public ThirdPartySerializer(IWebClient client)
        {
            WebClient = client;
        }
        public T SerializedJsonData<T>(string url) where T : new()
        {
            var jsonData = string.Empty;
            using (WebClient)
            {
                try
                {
                    jsonData = WebClient.DownloadString(url);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e);
                }

                return !string.IsNullOrEmpty(jsonData) ? JsonConvert.DeserializeObject<T>(jsonData) : new T();
            }
        }

        public string SerializedJsonData<T>(string url, string method, Func<T> func)
        {
            var item = func();

            using (WebClient)
            {
                try
                {
                    return WebClient.UploadString(url, method, item.ToString());
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e);
                }
            }
        }

        public T SerializeXmlData<T>(string uri) where T : new()
        {
            using (WebClient)
            {
                var data = WebClient.DownloadString(uri);

                var serializer = new XmlSerializer(typeof(T));
                var rdr = new StringReader(data);
                return (T)serializer.Deserialize(rdr);
            }
        }
    }
}
