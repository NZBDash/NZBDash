using System.Collections.Generic;
using System.Threading.Tasks;

using NZBDash.DataAccess.Interfaces;

namespace NZBDash.DataAccess.Repository.Settings
{
    public class NzbGetRepository : IRepository<NzbGetRepository>
    {
        public NzbGetRepository()
        {
            Db = new NZBDashContext();
        }

        public NZBDashContext Db { get; set; }

        public NzbGetRepository Find(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<NzbGetRepository> FindAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<NzbGetRepository> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<NzbGetRepository>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public NzbGetRepository Insert(NzbGetRepository entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<NzbGetRepository> Insert(IEnumerable<NzbGetRepository> entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<NzbGetRepository>> InsertAsync(IEnumerable<NzbGetRepository> entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<NzbGetRepository> InsertAsync(NzbGetRepository entity)
        {
            throw new System.NotImplementedException();
        }

        public int Remove(NzbGetRepository entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> RemoveAsync(NzbGetRepository entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> ModifyAsync(NzbGetRepository entity)
        {
            throw new System.NotImplementedException();
        }

        public int Modify(NzbGetRepository entity)
        {
            throw new System.NotImplementedException();
        }
    }
}