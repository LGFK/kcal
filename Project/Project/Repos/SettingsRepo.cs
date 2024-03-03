using Project.Data;
using Project.Models;

namespace Project.Repos
{
    public class SettingsRepo
    {
        private readonly KcalContext _context;

        public SettingsRepo(KcalContext context)
        {
            _context = context;
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

        public async Task AddSettings(Settings settings,UserDescription uD)
        {
            try
            {
                _context.Settings.Add(settings);
                _context.UserDescriptions.Add(uD);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {

            }
           
        }
    }
}
