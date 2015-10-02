using System;

using Microsoft.AspNet.SignalR;

using NZBDash.Core.Configuration;
using NZBDash.Core.Model.DTO;

namespace NZBDash.UI.Hubs
{
    public class LinksConfigurationHub : Hub
    {
        public void AddLink(string linkName, string linkEndpoint)
        {
            Uri uri = null;
            try
            {
                uri = new Uri(linkEndpoint);
            }
            catch (Exception e)
            {
                Clients.All.error(e.Message);
                return;
            }

            var config = new LinksConfiguration();
            var dto = new LinksConfigurationDto { LinkEndpoint = linkEndpoint, LinkName = linkName };
            var result = config.AddLink(dto);

            if (result != null)
            {
                Clients.All.success(string.Format("You have added {0}!", dto.LinkName));
                Clients.All.addLink(result.Id);
            }
        }

        public void RemoveLink(int modelId)
        {
            var config = new LinksConfiguration();
            var result = config.RemoveLink(modelId);

            if (result)
            {
                Clients.All.success(string.Format("You have removed the link!"));
                Clients.All.remove(modelId);
                return;
            }
            Clients.All.error(string.Format("We could not remove the link!"));

        }
    }
}