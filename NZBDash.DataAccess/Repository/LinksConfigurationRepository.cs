using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

using NZBDash.Common.Models.Data.Models;
using NZBDash.DataAccess.Interfaces;

namespace NZBDash.DataAccess.Repository
{
    [ExcludeFromCodeCoverage]
    public class LinksConfigurationRepository : IRepository<LinksConfiguration>
    {
        public LinksConfigurationRepository()
        {
            Db = new NZBDashContext();
        }

        public NZBDashContext Db { get; set; }

        public LinksConfiguration Find(int id)
        {
            return Db.LinksConfiguration.Find(id);
        }

        public async Task<LinksConfiguration> FindAsync(int id)
        {
            return await Db.LinksConfiguration.FindAsync(id);
        }

        public IEnumerable<LinksConfiguration> GetAll()
        {
            return Db.LinksConfiguration.ToList();
        }

        public async Task<IEnumerable<LinksConfiguration>> GetAllAsync()
        {
            return await Db.LinksConfiguration.ToListAsync();
        }

        public LinksConfiguration Insert(LinksConfiguration entity)
        {
            Db.LinksConfiguration.Add(entity);
            Db.SaveChanges();
            return entity;
        }

        public IEnumerable<LinksConfiguration> Insert(IEnumerable<LinksConfiguration> entity)
        {
            Db.LinksConfiguration.AddRange(entity);
            Db.SaveChanges();
            return entity;
        }

        public async Task<IEnumerable<LinksConfiguration>> InsertAsync(IEnumerable<LinksConfiguration> entity)
        {
            Db.LinksConfiguration.AddRange(entity);
            await Db.SaveChangesAsync();
            return entity;
        }

        public async Task<LinksConfiguration> InsertAsync(LinksConfiguration entity)
        {
            Db.LinksConfiguration.Add(entity);
            await Db.SaveChangesAsync();
            return entity;
        }

        public int Remove(LinksConfiguration entity)
        {
           Db.LinksConfiguration.Remove(entity);
            return Db.SaveChanges();
        }

        public Task<int> RemoveAsync(LinksConfiguration entity)
        {
            throw new NotImplementedException();
        }

        public async Task<int> ModifyAsync(LinksConfiguration entity)
        {
            Db.LinksConfiguration.Attach(entity);

            var entry = Db.Entry(entity);
            entry.State = EntityState.Modified;

            return await Db.SaveChangesAsync();
        }

        public int Modify(LinksConfiguration entity)
        {
            Db.LinksConfiguration.Attach(entity);

            var entry = Db.Entry(entity);
            entry.State = EntityState.Modified;

            return Db.SaveChanges();
        }
    }
}
