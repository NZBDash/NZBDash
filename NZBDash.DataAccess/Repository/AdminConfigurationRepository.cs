using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using NZBDash.DataAccess.Interfaces;
using NZBDash.DataAccess.Models;

namespace NZBDash.DataAccess.Repository
{
    public class AdminConfigurationRepository : IRepository<AdminConfiguration>
    {
        public AdminConfigurationRepository()
        {
            Db = new NZBDashContext();
        }

        public NZBDashContext Db { get; set; }

        public AdminConfiguration Find(int id)
        {
            return Db.AdminConfiguration.Find(id);
        }

        public async Task<AdminConfiguration> FindAsync(int id)
        {
            return await Db.AdminConfiguration.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<AdminConfiguration> GetAll()
        {
            return Db.AdminConfiguration.ToList();
        }

        public async Task<IEnumerable<AdminConfiguration>> GetAllAsync()
        {
            return await Db.AdminConfiguration.ToListAsync();
        }

        public AdminConfiguration Insert(AdminConfiguration entity)
        {
            Db.AdminConfiguration.Add(entity);
            Db.SaveChanges();
            return entity;
        }

        public IEnumerable<AdminConfiguration> Insert(IEnumerable<AdminConfiguration> entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<AdminConfiguration>> InsertAsync(IEnumerable<AdminConfiguration> entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<AdminConfiguration> InsertAsync(AdminConfiguration entity)
        {
            Db.AdminConfiguration.Add(entity);
            await Db.SaveChangesAsync();
            return entity;
        }

        public int Remove(AdminConfiguration entity)
        {
            Db.AdminConfiguration.Remove(entity);
            return Db.SaveChanges();
        }

        public async Task<int> RemoveAsync(AdminConfiguration entity)
        {
            Db.AdminConfiguration.Remove(entity);
            return await Db.SaveChangesAsync();
        }

        public async Task<int> ModifyAsync(AdminConfiguration entity)
        {
            Db.AdminConfiguration.Attach(entity);

            var entry = Db.Entry(entity);
            entry.State = EntityState.Modified;

            return await Db.SaveChangesAsync();
        }

        public int Modify(AdminConfiguration entity)
        {
            Db.AdminConfiguration.Attach(entity);

            var entry = Db.Entry(entity);
            entry.State = EntityState.Modified;

            return Db.SaveChanges();
        }
    }
}