using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Web.Http;
using System.Xml.Serialization;

using Microsoft.VisualBasic.Devices;

using Newtonsoft.Json;

using NZBDash.Api.Models;
using NZBDash.Core.Model;

namespace NZBDash.Api.Controllers
{
    [RoutePrefix("api/{controller}/{action}")]
    public class StatusApiController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<DriveInfoObj> Get()
        {
            return GetPhysicalDrives();
        }

        [HttpGet]
        [ActionName("DriveInfo")]
        public IEnumerable<DriveInfoObj> GetDriveInfo()
        {
            return GetPhysicalDrives();
        }

        [HttpGet]
        [ActionName("RamInfo")]
        public RamInfoObj GetRamInfo()
        {
            return new RamInfoObj(new ComputerInfo());
        }

        [HttpGet]
        [ActionName("UpTime")]
        public TimeSpan UpTime()
        {            
            using (var uptime = new PerformanceCounter("System", "System Up Time"))
            {
                // Call this an extra time before reading its value
                uptime.NextValue();       
                return TimeSpan.FromSeconds(uptime.NextValue());
            }
        }

        [HttpGet]
        [ActionName("NetworkInfo")]
        public NetworkInfo GetNetworkInfo()
        {
            return GetNetworkingDetails();
        }

        [HttpGet]
        [ActionName("Processes")]
        public List<ProcessObj> GetProcesses()
        {
            var list = new List<ProcessObj>();
            Process.GetProcesses().ToList().ForEach(o => list.Add(MakeProcessObj(o)));
            return list;
        }
        
        [HttpGet]
        [ActionName("GetCouchPotatoMovies")]
        public void GetCouchPotatoMovies(string uri, string api)
        {
            throw new NotImplementedException("TODO");
        }

        [HttpGet]
        [ActionName("GetPlexServers")]
        public PlexServers GetPlexServers(string uri)
        {
            return SerializeXmlData<PlexServers>(uri + "servers");
        }

        [HttpGet]
        [ActionName("GetSonarrSystemStatus")]
        public SonarrSystemStatus GetSonarrSystemStatus(string uri, string api)
        {
            return SerializedJsonData<SonarrSystemStatus>(uri + "api/system/status?apikey=" + api);
        }

        [HttpGet]
        [ActionName("GetCouchPotatoStatus")]
        public CouchPotatoStatus GetCouchPotatoStatus(string uri, string api)
        {
            return SerializedJsonData<CouchPotatoStatus>(uri + "api/" + api + "/app.available/");
        }

        [HttpGet]
        [ActionName("GetNzbGetHistory")]
        public NzbGetHistory GetNzbGetHistory(string url, string username, string password)
        {
            return SerializedJsonData<NzbGetHistory>(string.Format("{0}{1}:{2}/jsonrpc/history", url, username, password));
        }

        [HttpGet]
        [ActionName("GetNzbGetHistory")]
        public NzbGetList GetNzbGetList(string url, string username, string password)
        {
            return SerializedJsonData<NzbGetList>(string.Format("{0}{1}:{2}/jsonrpc/listgroups", url, username, password));
        }

        [HttpGet]
        [ActionName("GetNzbGetStatus")]
        public NzbGetStatus GetNzbGetStatus(string url, string username, string password)
        {
            return SerializedJsonData<NzbGetStatus>(string.Format("{0}{1}:{2}/jsonrpc/status", url, username, password));
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
        [ActionName("Proxy")]
        public dynamic Proxy([FromUri]string url)
        {
            return SerializedJsonData<object>(url);
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

        private static T SerializeXmlData<T>(string uri) where T : new()
        {
            using (var w = new WebClient())
            {
                var data = w.DownloadString(uri);

                var serializer = new XmlSerializer(typeof(T));
                var rdr = new StringReader(data);
                return (T)serializer.Deserialize(rdr);
            }
        }

        private IEnumerable<DriveInfoObj> GetPhysicalDrives()
        {
            var list = new List<DriveInfoObj>();
            foreach (var drive in DriveInfo.GetDrives())
            {
                if (drive.IsReady)
                {
                    list.Add(new DriveInfoObj(drive));
                }
            }

            return list;
        }
        
        private ProcessObj MakeProcessObj(Process proc)
        {
            ProcessObj obj = new ProcessObj
            {
                BasePriority = proc.BasePriority,
                EnableRaisingEvents = proc.EnableRaisingEvents,
                HandleCount = proc.HandleCount,
                Id = proc.Id,
                MachineName = proc.MachineName,
                MainWindowHandle = proc.MainWindowHandle,
                MainWindowTitle = proc.MainWindowTitle,
                NonpagedSystemMemorySize64 = proc.NonpagedSystemMemorySize64,
                PagedMemorySize64 = proc.PagedMemorySize64,
                PagedSystemMemorySize64 = proc.PagedSystemMemorySize64,
                PeakPagedMemorySize64 = proc.PeakPagedMemorySize64,
                PeakVirtualMemorySize64 = proc.PeakVirtualMemorySize64,
                PeakWorkingSet64 = proc.PeakWorkingSet64,
                PrivateMemorySize64 = proc.PrivateMemorySize64,
                ProcessName = proc.ProcessName,
                Responding = proc.Responding,
                SessionId = proc.SessionId,
                StartInfo = proc.StartInfo,
                VirtualMemorySize64 = proc.VirtualMemorySize64,
                WorkingSet64 = proc.WorkingSet64
            };
           
            return obj;
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