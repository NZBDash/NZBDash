using System.Collections.Generic;
using System.Threading.Tasks;

using NZBDash.DataAccess.Interfaces;

namespace NZBDash.DataAccess.Repository.Settings
{
    public class SabNzbRepository : IRepository<SabNzbRepository>
    {
        public SabNzbRepository()
        {
            Db = new NZBDashContext();
        }

        public NZBDashContext Db { get; set; }

        public SabNzbRepository Find(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<SabNzbRepository> FindAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<SabNzbRepository> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<SabNzbRepository>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public SabNzbRepository Insert(SabNzbRepository entity)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<SabNzbRepository> Insert(IEnumerable<SabNzbRepository> entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<SabNzbRepository>> InsertAsync(IEnumerable<SabNzbRepository> entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<SabNzbRepository> InsertAsync(SabNzbRepository entity)
        {
            throw new System.NotImplementedException();
        }

        public int Remove(SabNzbRepository entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> RemoveAsync(SabNzbRepository entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> ModifyAsync(SabNzbRepository entity)
        {
            throw new System.NotImplementedException();
        }

        public int Modify(SabNzbRepository entity)
        {
            throw new System.NotImplementedException();
        }
    }
}