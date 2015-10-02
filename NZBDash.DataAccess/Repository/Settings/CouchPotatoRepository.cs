using System.Collections.Generic;
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
            throw new System.NotImplementedException();
        }

        public Task<CouchPotatoSettings> FindAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CouchPotatoSettings> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<CouchPotatoSettings>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public CouchPotatoSettings Insert(CouchPotatoSettings entity)
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }
    }
}