using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using NZBDash.DataAccess.Interfaces;
using NZBDash.DataAccess.Models;

namespace NZBDash.DataAccess.Repository
{
    public class ApplicationsRepository : IRepository<Applications>
    {
        private NZBDashContext Db { get; set; }

        public ApplicationsRepository()
        {
            Db = new NZBDashContext();
        }

        public Applications Find(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Applications> FindAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Applications> GetAll()
        {
            return Db.Applications.ToList();
        }

        public Task<IEnumerable<Applications>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Applications Insert(Applications entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Applications> Insert(IEnumerable<Applications> entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Applications>> InsertAsync(IEnumerable<Applications> entity)
        {
            throw new NotImplementedException();
        }

        public Task<Applications> InsertAsync(Applications entity)
        {
            throw new NotImplementedException();
        }

        public int Remove(Applications entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveAsync(Applications entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> ModifyAsync(Applications entity)
        {
            throw new NotImplementedException();
        }

        public int Modify(Applications entity)
        {
            throw new NotImplementedException();
        }
    }
}
