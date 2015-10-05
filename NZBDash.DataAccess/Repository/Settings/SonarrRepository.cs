using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;

using NZBDash.DataAccess.Interfaces;
using NZBDash.DataAccess.Models.Settings;

namespace NZBDash.DataAccess.Repository.Settings
{
    public class SonarrRepository : IRepository<SonarrSettings>
    {
        public SonarrRepository()
        {
            Db = new NZBDashContext();
        }

        public NZBDashContext Db { get; set; }

        public SonarrSettings Find(int id)
        {
            return Db.SonarrSettings.Find(id);
        }

        public Task<SonarrSettings> FindAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<SonarrSettings> GetAll()
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<SonarrSettings>> GetAllAsync()
        {
            throw new System.NotImplementedException();
        }

        public SonarrSettings Insert(SonarrSettings entity)
        {
            Db.SonarrSettings.Add(entity);
            var result = Db.SaveChanges();
            return result == 1 ? entity : new SonarrSettings();
        }

        public IEnumerable<SonarrSettings> Insert(IEnumerable<SonarrSettings> entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<SonarrSettings>> InsertAsync(IEnumerable<SonarrSettings> entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<SonarrSettings> InsertAsync(SonarrSettings entity)
        {
            throw new System.NotImplementedException();
        }

        public int Remove(SonarrSettings entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> RemoveAsync(SonarrSettings entity)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> ModifyAsync(SonarrSettings entity)
        {
            throw new System.NotImplementedException();
        }

        public int Modify(SonarrSettings entity)
        {
            Db.SonarrSettings.Attach(entity);

            var entry = Db.Entry(entity);
            entry.State = EntityState.Modified;

            return Db.SaveChanges();
        }
    }
}