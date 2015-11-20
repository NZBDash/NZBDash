using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.DTO;
using NZBDash.UI.Models;

namespace NZBDash.UI.Controllers
{
    public class LinksConfigurationController : BaseController
    {
        private ILinksConfiguration Service { get; set; }

        public LinksConfigurationController(ILinksConfiguration service)
            : base(typeof(LinksConfigurationController))
        {
            Service = service;
        }

        // GET: LinksConfiguration
        public async Task<ActionResult> Index()
        {
            var result = await Service.GetLinksAsync();
            if (result == null) return View(new List<LinksViewModel>());

            var model = result.Select(item => new LinksViewModel
            {
                Id = item.Id,
                LinkEndpoint = new Uri(item.LinkEndpoint),
                LinkName = item.LinkName
            }).ToList();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateLink(LinksViewModel config)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index");
            }
            var dto = new LinksConfigurationDto { Id = config.Id, LinkName = config.LinkName, LinkEndpoint = config.LinkEndpoint.ToString() };

            var result = await Service.UpdateLinkAsync(dto);
            if (result)
            {
                return RedirectToAction("Index");
            }

            return View("Error");
        }

        public ActionResult GetLink(int id)
        {
            var links = Service.GetLinks();
            var link = links.FirstOrDefault(x => x.Id == id);
            var model = new LinksViewModel { Id = link.Id, LinkName = link.LinkName, LinkEndpoint = new Uri(link.LinkEndpoint) };
            return PartialView("_Link", model);
        }
    }
}