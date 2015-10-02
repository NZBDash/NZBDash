using System.Collections.Generic;
using System.Threading.Tasks;

using NZBDash.DataAccess.Interfaces;
using NZBDash.DataAccess.Models.Settings;

namespace NZBDash.DataAccess.Repository.Settings
{
    public class SabNzbRepository : IRepository<SabNzbSettings>
    {
        public SabNzbRepository()
        {
            Db = new NZBDashContext();
        }

        public NZBDashContext Db { get; set; }

        public SabNzbSettings Find(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<SabNzbSettings> FindAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<SabNzbSettings> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<SabNzbSettings>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public SabNzbSettings Insert(SabNzbSettings entity)
        {
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }
    }
}