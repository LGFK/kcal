using Project.Data;
using Project.Models;

namespace Project.Repos
{
    public class SettingsRepo: IKcalRepo<SettingsRepo>
    {
        private readonly KcalContext _context;

        public SettingsRepo(KcalContext context)
        {
            _context = context;
        }

        public Task AddEntities(List<SettingsRepo> entities)
        {
            throw new NotImplementedException();
        }

        public Task AddEntity(SettingsRepo entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteEntity(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<SettingsRepo>> GetEntitiesList()
        {
            throw new NotImplementedException();
        }

        public Task<SettingsRepo> GetEntity(int id)
        {
            throw new NotImplementedException();
        }

        public async Task< Settings> GetSettings(string userId)
        {
            try
            {
                return _context.Settings.Where(s => s.UserId == userId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
           
        }

        public async Task<UserDescription> GetUserDescription(string userId)
        {
            try
            {
                return _context.UserDescriptions.Where(s => s.UserId == userId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Task UpdateEntity(SettingsRepo entity)
        {
            throw new NotImplementedException();
        }
    }
}
