using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

using NZBDash.Common.Models.Data.Models.Settings;
using NZBDash.DataAccess.Interfaces;

namespace NZBDash.DataAccess.Repository.Settings
{
    [ExcludeFromCodeCoverage]
    public class NzbGetRepository : IRepository<NzbGetSettings>
    {
        public NzbGetRepository()
        {
            Db = new NZBDashContext();
        }

        public NZBDashContext Db { get; set; }

        public NzbGetSettings Find(int id)
        {
            return Db.NzbGetSettings.Find(id);
        }

        public async Task<NzbGetSettings> FindAsync(int id)
        {
            return await Db.NzbGetSettings.FindAsync(id);
        }

        public IEnumerable<NzbGetSettings> GetAll()
        {
            return Db.NzbGetSettings.ToList();
        }

        public async Task<IEnumerable<NzbGetSettings>> GetAllAsync()
        {
            return await Db.NzbGetSettings.ToListAsync();
        }

        public NzbGetSettings Insert(NzbGetSettings entity)
        {
            Db.NzbGetSettings.Add(entity);
            var result = Db.SaveChanges();
            return result == 1 ? entity : new NzbGetSettings();
        }

        public IEnumerable<NzbGetSettings> Insert(IEnumerable<NzbGetSettings> entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<NzbGetSettings>> InsertAsync(IEnumerable<NzbGetSettings> entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<NzbGetSettings> InsertAsync(NzbGetSettings entity)
        {
            throw new System.NotImplementedException();
        }

        public int Remove(NzbGetSettings entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> RemoveAsync(NzbGetSettings entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> ModifyAsync(NzbGetSettings entity)
        {
            throw new System.NotImplementedException();
        }

        public int Modify(NzbGetSettings entity)
        {
            Db.NzbGetSettings.Attach(entity);
         
            var entry = Db.Entry(entity);
            entry.State = EntityState.Modified;

            return Db.SaveChanges();
        }
    }
}