using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using RestSharp;

namespace NZBDash.ThirdParty.Api.Service
{
    public class RestService
    {
        public RestClient Client { get; set; }

        public void Get(string baseUri, string resource)
        {
            Client = new RestClient(baseUri);
            var request = new RestRequest(resource, Method.GET);
            var result = Client.ExecuteAsGet(request, Method.GET.ToString());

            
        }
    }
}
