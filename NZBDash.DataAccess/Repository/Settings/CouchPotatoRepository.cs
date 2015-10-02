using System.Collections.Generic;
using System.Threading.Tasks;

using NZBDash.DataAccess.Interfaces;

namespace NZBDash.DataAccess.Repository.Settings
{
    public class CouchPotatoRepository : IRepository<CouchPotatoRepository>
    {
        public CouchPotatoRepository()
        {
            Db = new NZBDashContext();
        }

        public NZBDashContext Db { get; set; }

        public CouchPotatoRepository Find(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<CouchPotatoRepository> FindAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CouchPotatoRepository> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<CouchPotatoRepository>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public CouchPotatoRepository Insert(CouchPotatoRepository entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CouchPotatoRepository> Insert(IEnumerable<CouchPotatoRepository> entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<CouchPotatoRepository>> InsertAsync(IEnumerable<CouchPotatoRepository> entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<CouchPotatoRepository> InsertAsync(CouchPotatoRepository entity)
        {
            throw new System.NotImplementedException();
        }

        public int Remove(CouchPotatoRepository entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> RemoveAsync(CouchPotatoRepository entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> ModifyAsync(CouchPotatoRepository entity)
        {
            throw new System.NotImplementedException();
        }

        public int Modify(CouchPotatoRepository entity)
        {
            throw new System.NotImplementedException();
        }
    }
}