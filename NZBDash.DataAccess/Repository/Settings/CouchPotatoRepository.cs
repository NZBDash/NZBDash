using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

using NZBDash.DataAccess.Interfaces;
using NZBDash.DataAccess.Models.Settings;

namespace NZBDash.DataAccess.Repository.Settings
{
    public class CouchPotatoRepository : IRepository<CouchPotatoSettings>
    {
        public CouchPotatoRepository()
        {
            Db = new NZBDashContext();
        }

        public NZBDashContext Db { get; set; }

        public CouchPotatoSettings Find(int id)
        {
            return Db.CouchPotatoSettings.Find(id);
        }

        public Task<CouchPotatoSettings> FindAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CouchPotatoSettings> GetAll()
        {
            return Db.CouchPotatoSettings.ToList();
        }

        public Task<IEnumerable<CouchPotatoSettings>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public CouchPotatoSettings Insert(CouchPotatoSettings entity)
        {
            Db.CouchPotatoSettings.Add(entity);
            var result = Db.SaveChanges();
            return result == 1 ? entity : new CouchPotatoSettings();
        }

        public IEnumerable<CouchPotatoSettings> Insert(IEnumerable<CouchPotatoSettings> entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<CouchPotatoSettings>> InsertAsync(IEnumerable<CouchPotatoSettings> entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<CouchPotatoSettings> InsertAsync(CouchPotatoSettings entity)
        {
            throw new System.NotImplementedException();
        }

        public int Remove(CouchPotatoSettings entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> RemoveAsync(CouchPotatoSettings entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> ModifyAsync(CouchPotatoSettings entity)
        {
            throw new System.NotImplementedException();
        }

        public int Modify(CouchPotatoSettings entity)
        {
            Db.CouchPotatoSettings.Attach(entity);

            var entry = Db.Entry(entity);
            entry.State = EntityState.Modified;

            return Db.SaveChanges();
        }
    }
}