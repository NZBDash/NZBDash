using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using NZBDash.DataAccess.Interfaces;
using NZBDash.DataAccess.Models;

namespace NZBDash.DataAccess.Repository
{
    public class MockRepository : IRepository<Entity>
    {
        public Entity Find(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Entity> FindAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Entity> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Entity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Entity Insert(Entity entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Entity> Insert(IEnumerable<Entity> entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Entity>> InsertAsync(IEnumerable<Entity> entity)
        {
            throw new NotImplementedException();
        }

        public Task<Entity> InsertAsync(Entity entity)
        {
            throw new NotImplementedException();
        }

        public int Remove(Entity entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> RemoveAsync(Entity entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> ModifyAsync(Entity entity)
        {
            throw new NotImplementedException();
        }

        public int Modify(Entity entity)
        {
            throw new NotImplementedException();
        }
    }
}
