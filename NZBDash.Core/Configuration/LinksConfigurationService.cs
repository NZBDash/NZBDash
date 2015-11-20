using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using NZBDash.Common.Models.Data.Models;
using NZBDash.Core.Interfaces;
using NZBDash.Core.Model.DTO;
using NZBDash.DataAccess.Interfaces;

namespace NZBDash.Core.Configuration
{
    public class LinksConfigurationService : ILinksConfiguration
    {
        public LinksConfigurationService(IRepository<LinksConfiguration> repo)
        {
            Repo = repo;
        }

        private IRepository<LinksConfiguration> Repo { get; set; }
        public async Task<IEnumerable<LinksConfigurationDto>> GetLinksAsync()
        {
            var result = await Repo.GetAllAsync();
            var dto = result.Select(r => new LinksConfigurationDto
            {
                LinkEndpoint = r.LinkEndpoint,
                Id = r.Id,
                LinkName = r.LinkName
            }).ToList();

            return dto;
        }

        public IEnumerable<LinksConfigurationDto> GetLinks()
        {
            var reuslt = Repo.GetAll();
            var dto = reuslt.Select(r => new LinksConfigurationDto
            {
                LinkEndpoint = r.LinkEndpoint,
                Id = r.Id,
                LinkName = r.LinkName
            }).ToList();

            return dto;
        }

        public bool UpdateLink(LinksConfigurationDto dto)
        {
            var entity = Repo.Find(dto.Id);
            entity.LinkEndpoint = dto.LinkEndpoint;
            entity.LinkName = dto.LinkName;

            var result = Repo.Modify(entity);
            return result == 1;
        }

        public async Task<bool> UpdateLinkAsync(LinksConfigurationDto dto)
        {
            var entity = await Repo.FindAsync(dto.Id);
            entity.LinkEndpoint = dto.LinkEndpoint;
            entity.LinkName = dto.LinkName;

            var result = await Repo.ModifyAsync(entity);
            return result == 1;
        }

        public LinksConfigurationDto AddLink(LinksConfigurationDto link)
        {
            var entity = new LinksConfiguration { LinkEndpoint = link.LinkEndpoint, LinkName = link.LinkName };

            var result = Repo.Insert(entity);

            return new LinksConfigurationDto{ Id = result.Id, LinkName = result.LinkName, LinkEndpoint = result.LinkEndpoint };
        }

        public bool RemoveLink(int id)
        {
            var entity = Repo.Find(id);
            if (entity != null)
            {
                var result = Repo.Remove(entity);
                return result == 1;
            }
            return false;
        }
    }
}
