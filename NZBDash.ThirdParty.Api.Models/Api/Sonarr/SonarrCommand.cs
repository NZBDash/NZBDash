using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NZBDash.DataAccess.Api.Sonarr
{

    public class SonarrCommand
    {
        public string name { get; set; }
        public Body body { get; set; }
        public string priority { get; set; }
        public string status { get; set; }
        public DateTime queued { get; set; }
        public string trigger { get; set; }
        public string state { get; set; }
        public bool manual { get; set; }
        public DateTime startedOn { get; set; }
        public bool sendUpdatesToClient { get; set; }
        public bool updateScheduledTask { get; set; }
        public int id { get; set; }
    }

    public class Body
    {
        public int[] episodeIds { get; set; }
        public bool sendUpdatesToClient { get; set; }
        public bool updateScheduledTask { get; set; }
        public string completionMessage { get; set; }
        public string name { get; set; }
        public string trigger { get; set; }
    }

}
