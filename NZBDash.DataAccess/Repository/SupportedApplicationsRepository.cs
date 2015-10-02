using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using NZBDash.DataAccess.Interfaces;
using NZBDash.DataAccess.Models;

namespace NZBDash.DataAccess.Repository
{
    public class SupportedApplicationsRepository : IRepository<SupportedApplications>
    {
        public SupportedApplicationsRepository()
        {
            Db = new NZBDashContext();
        }

        public NZBDashContext Db { get; set; }

        public SupportedApplications Find(int id)
        {
            return Db.SupportedApplications.Find(id);
        }

        public async Task<SupportedApplications> FindAsync(int id)
        {
            return await Db.SupportedApplications.FirstOrDefaultAsync(x => x.Id == id);
        }

        public IEnumerable<SupportedApplications> GetAll()
        {
            return Db.SupportedApplications.ToList();
        }

        public async Task<IEnumerable<SupportedApplications>> GetAllAsync()
        {
            return await Db.SupportedApplications.ToListAsync();
        }

        public SupportedApplications Insert(SupportedApplications entity)
        {
            Db.SupportedApplications.Add(entity);
            Db.SaveChanges();
            return entity;
        }

        public IEnumerable<SupportedApplications> Insert(IEnumerable<SupportedApplications> entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<SupportedApplications>> InsertAsync(IEnumerable<SupportedApplications> entity)
        {
            throw new System.NotImplementedException();
        }

        public async Task<SupportedApplications> InsertAsync(SupportedApplications entity)
        {
            Db.SupportedApplications.Add(entity);
            await Db.SaveChangesAsync();
            return entity;
        }

        public int Remove(SupportedApplications entity)
        {
            Db.SupportedApplications.Remove(entity);
            return Db.SaveChanges();
        }

        public async Task<int> RemoveAsync(SupportedApplications entity)
        {
            Db.SupportedApplications.Remove(entity);
            return await Db.SaveChangesAsync();
        }

        public async Task<int> ModifyAsync(SupportedApplications entity)
        {
            Db.SupportedApplications.Attach(entity);

            var entry = Db.Entry(entity);
            entry.State = EntityState.Modified;

            return await Db.SaveChangesAsync();
        }

        public int Modify(SupportedApplications entity)
        {
            Db.SupportedApplications.Attach(entity);

            var entry = Db.Entry(entity);
            entry.State = EntityState.Modified;

            return Db.SaveChanges();
        }
    }
}