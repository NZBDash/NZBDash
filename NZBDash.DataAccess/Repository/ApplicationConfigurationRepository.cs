using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using NZBDash.DataAccess.Interfaces;
using NZBDash.DataAccess.Models;

namespace NZBDash.DataAccess.Repository
{
    public class ApplicationConfigurationRepository : IRepository<ApplicationConfiguration>
    {
        public ApplicationConfigurationRepository()
        {
            Db = new NZBDashContext();
        }

        public NZBDashContext Db { get; set; }

        public ApplicationConfiguration Find(int id)
        {
            return Db.ApplicationConfiguration.Find(id);
        }

        public async Task<ApplicationConfiguration> FindAsync(int id)
        {
            return await Db.ApplicationConfiguration.FindAsync(id);
        }

        public IEnumerable<ApplicationConfiguration> GetAll()
        {
            return Db.ApplicationConfiguration.ToList();
        }

        public async Task<IEnumerable<ApplicationConfiguration>> GetAllAsync()
        {
            return await Db.ApplicationConfiguration.ToListAsync();
        }

        public ApplicationConfiguration Insert(ApplicationConfiguration entity)
        {
            Db.ApplicationConfiguration.Add(entity);
            Db.SaveChanges();
            return entity;
        }

        public IEnumerable<ApplicationConfiguration> Insert(IEnumerable<ApplicationConfiguration> entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<ApplicationConfiguration>> InsertAsync(IEnumerable<ApplicationConfiguration> entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ApplicationConfiguration> InsertAsync(ApplicationConfiguration entity)
        {
            Db.ApplicationConfiguration.Add(entity);
            await Db.SaveChangesAsync();
            return entity;
        }

        public int Remove(ApplicationConfiguration entity)
        {
            Db.ApplicationConfiguration.Remove(entity);
            return Db.SaveChanges();
        }

        public async Task<int> RemoveAsync(ApplicationConfiguration entity)
        {
            Db.ApplicationConfiguration.Remove(entity);
            return await Db.SaveChangesAsync();
        }

        public async Task<int> ModifyAsync(ApplicationConfiguration entity)
        {
            Db.ApplicationConfiguration.Attach(entity);

            var entry = Db.Entry(entity);
            entry.State = EntityState.Modified;

            return await Db.SaveChangesAsync();
        }

        public int Modify(ApplicationConfiguration entity)
        {
            Db.ApplicationConfiguration.Attach(entity);

            var entry = Db.Entry(entity);
            entry.State = EntityState.Modified;

            return Db.SaveChanges();
        }
    }
}
