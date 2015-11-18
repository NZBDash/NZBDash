using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Web.Http;
using System.Xml.Serialization;

using Newtonsoft.Json;

using NZBDash.Api.Models;
using NZBDash.Core.Model;

namespace NZBDash.Api.Controllers
{
    [RoutePrefix("api/{controller}/{action}")]
    public class StatusApiController : ApiController
    {
        [HttpGet]
        [ActionName("NetworkInfo")]
        public NetworkInfo GetNetworkInfo()
        {
            return GetNetworkingDetails();
        }

        [HttpGet]
        [ActionName("SabNZB")]
        public SabNzbObject GetSabNzb(string url, string api)
        {
            var ret = new SabNzbObject();
            ret.QueueObject = GetSabNzbQueue(url, api);
            ret.SabHistory = GetSabNzbHistory(url, api);
            return ret;
        }

        [HttpGet]
        [ActionName("SabNzbQueue")]
        public SabNzbQueueObject GetSabNzbQueue(string url, string api)
        {
            return SerializedJsonData<SabNzbQueueObject>(url + "api?mode=qstatus&output=json&apikey=" + api);
        }

        [HttpGet]
        [ActionName("SabNzbHistory")]
        public History GetSabNzbHistory(string url, string api)
        {
            return SerializedJsonData<SabNzbHistory>(url + "api?mode=history&start=0&limit=10&output=json&apikey=" + api).History;
        }


        private static T SerializedJsonData<T>(string url) where T : new()
        {
            using (var w = new WebClient())
            {
                string jsonData;

                // attempt to download JSON data as a string
                try
                {
                    jsonData = w.DownloadString(url);
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message, e);
                }

                // If string with JSON data is not empty,
                // deserialize it to class and return its instance
                return !string.IsNullOrEmpty(jsonData) ? JsonConvert.DeserializeObject<T>(jsonData) : new T();
            }
        }

        private NetworkInfo GetNetworkingDetails()
        {
            NetworkInfo info = new NetworkInfo();
            PerformanceCounterCategory performanceCounterCategory = new PerformanceCounterCategory("Network Interface");
            string cn = performanceCounterCategory.GetInstanceNames()[0];
            var networkBytesSent = new PerformanceCounter("Network Interface", "Bytes Sent/sec", cn);
            var networkBytesReceived = new PerformanceCounter("Network Interface", "Bytes Received/sec", cn);
            var networkBytesTotal = new PerformanceCounter("Network Interface", "Bytes Total/sec", cn);

            info.Sent = networkBytesSent.NextValue();
            info.Recieved = networkBytesReceived.NextValue();
            info.Total = networkBytesTotal.NextValue();

            // First counter is empty
            Thread.Sleep(1000);
            info.Sent = networkBytesSent.NextValue();
            info.Recieved = networkBytesReceived.NextValue();
            info.Total = networkBytesTotal.NextValue();
            return info;
        }
    }
}
