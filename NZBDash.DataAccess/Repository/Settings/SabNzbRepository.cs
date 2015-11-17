using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

using NZBDash.DataAccess.Interfaces;
using NZBDash.DataAccess.Models.Settings;

namespace NZBDash.DataAccess.Repository.Settings
{
    [ExcludeFromCodeCoverage]
    public class SabNzbRepository : IRepository<SabNzbSettings>
    {
        public SabNzbRepository()
        {
            Db = new NZBDashContext();
        }

        public NZBDashContext Db { get; set; }

        public SabNzbSettings Find(int id)
        {
            return Db.SabNzbSettings.Find(id);
        }

        public async Task<SabNzbSettings> FindAsync(int id)
        {
            return await Db.SabNzbSettings.FindAsync(id);
        }

        public IEnumerable<SabNzbSettings> GetAll()
        {
            return Db.SabNzbSettings.ToList();
        }

        public async Task<IEnumerable<SabNzbSettings>> GetAllAsync()
        {
            return await Db.SabNzbSettings.ToListAsync();
        }

        public SabNzbSettings Insert(SabNzbSettings entity)
        {
            Db.SabNzbSettings.Add(entity);
            var result = Db.SaveChanges();
            return result == 1 ? entity : new SabNzbSettings();
        }

        public IEnumerable<SabNzbSettings> Insert(IEnumerable<SabNzbSettings> entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<SabNzbSettings>> InsertAsync(IEnumerable<SabNzbSettings> entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<SabNzbSettings> InsertAsync(SabNzbSettings entity)
        {
            throw new System.NotImplementedException();
        }

        public int Remove(SabNzbSettings entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> RemoveAsync(SabNzbSettings entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> ModifyAsync(SabNzbSettings entity)
        {
            throw new System.NotImplementedException();
        }

        public int Modify(SabNzbSettings entity)
        {
            Db.SabNzbSettings.Attach(entity);

            var entry = Db.Entry(entity);
            entry.State = EntityState.Modified;

            return Db.SaveChanges();
        }
    }
}