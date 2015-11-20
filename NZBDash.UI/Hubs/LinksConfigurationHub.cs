using System;

using Microsoft.AspNet.SignalR;

using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.DTO;

namespace NZBDash.UI.Hubs
{
    public class LinksConfigurationHub : Hub
    {
        private ILinksConfiguration LinksService { get; set; }

        public LinksConfigurationHub(ILinksConfiguration linksService)
        {
            LinksService = linksService;
        }

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

            var dto = new LinksConfigurationDto { LinkEndpoint = linkEndpoint, LinkName = linkName };
            var result = LinksService.AddLink(dto);

            if (result != null)
            {
                Clients.All.success(string.Format("You have added {0}!", dto.LinkName));
                Clients.All.addLink(result.Id);
            }
        }

        public void RemoveLink(int modelId)
        {
            var result = LinksService.RemoveLink(modelId);

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