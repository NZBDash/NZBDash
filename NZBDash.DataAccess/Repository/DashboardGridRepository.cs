using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

using NZBDash.Common.Models.Data.Models;
using NZBDash.DataAccess.Interfaces;

namespace NZBDash.DataAccess.Repository
{
    [ExcludeFromCodeCoverage]
    public class DashboardGridRepository : IRepository<DashboardGrid>
    {
        public DashboardGridRepository()
        {
            Db = new NZBDashContext();
        }

        private NZBDashContext Db { get; set; }
        public DashboardGrid Find(int id)
        {
            throw new NotImplementedException();
        }

        public Task<DashboardGrid> FindAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DashboardGrid> GetAll()
        {
            return Db.DashboardGrid.ToList();
        }

        public Task<IEnumerable<DashboardGrid>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public DashboardGrid Insert(DashboardGrid entity)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<DashboardGrid> Insert(IEnumerable<DashboardGrid> entity)
        {
            Db.DashboardGrid.AddRange(entity);
            Db.SaveChanges();

            return entity;
        }

        public async Task<IEnumerable<DashboardGrid>> InsertAsync(IEnumerable<DashboardGrid> entity)
        {
            Db.DashboardGrid.AddRange(entity);
            await Db.SaveChangesAsync();

            return entity;
        }

        public Task<DashboardGrid> InsertAsync(DashboardGrid entity)
        {
            throw new NotImplementedException();
        }

        public int Remove(DashboardGrid entity)
        {
            throw new NotImplementedException();
        }

        public int RemoveAll()
        {
            Db.DashboardGrid.RemoveRange(Db.DashboardGrid.ToList());
            return Db.SaveChanges();
        }

        public Task<int> RemoveAsync(DashboardGrid entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> ModifyAsync(DashboardGrid entity)
        {
            throw new NotImplementedException();
        }

        public int Modify(DashboardGrid entity)
        {
            throw new NotImplementedException();
        }
    }
}
