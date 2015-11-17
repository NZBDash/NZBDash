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
    public class PlexRepository : IRepository<PlexSettings>
    {
        public PlexRepository()
        {
            Db = new NZBDashContext();
        }

        public NZBDashContext Db { get; set; }

        public PlexSettings Find(int id)
        {
            return Db.PlexSettings.Find(id);
        }

        public Task<PlexSettings> FindAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<PlexSettings> GetAll()
        {
            return Db.PlexSettings.ToList();
        }

        public Task<IEnumerable<PlexSettings>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public PlexSettings Insert(PlexSettings entity)
        {
            Db.PlexSettings.Add(entity);
            var result = Db.SaveChanges();
            return result == 1 ? entity : new PlexSettings();
        }

        public IEnumerable<PlexSettings> Insert(IEnumerable<PlexSettings> entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<PlexSettings>> InsertAsync(IEnumerable<PlexSettings> entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<PlexSettings> InsertAsync(PlexSettings entity)
        {
            throw new System.NotImplementedException();
        }

        public int Remove(PlexSettings entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> RemoveAsync(PlexSettings entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> ModifyAsync(PlexSettings entity)
        {
            throw new System.NotImplementedException();
        }

        public int Modify(PlexSettings entity)
        {
            Db.PlexSettings.Attach(entity);

            var entry = Db.Entry(entity);
            entry.State = EntityState.Modified;

            return Db.SaveChanges();
        }
    }
}