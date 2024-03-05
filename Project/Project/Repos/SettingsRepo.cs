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
        public async Task<Settings> UpdateSettings(string userId,Settings settingsToUpdate)
        {
            try
            {
                var settingsTmp = _context.Settings.Where(u => userId == u.UserId).FirstOrDefault();
                settingsTmp.EmailNotifications = settingsToUpdate.EmailNotifications;
                settingsTmp.Theme = settingsToUpdate.Theme;
                settingsTmp.GoalId = settingsToUpdate.GoalId;
                _context.SaveChanges();
                return settingsTmp;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public async Task<UserDescription> UpdateDescription(string userId, UserDescription description)
        {
            try
            {
                var descTmp = _context.UserDescriptions.Where(u => userId == u.UserId).FirstOrDefault();
                descTmp.WeightKG = description.WeightKG;
                descTmp.HeightCM = description.HeightCM;
                descTmp.Age = description.Age;
                descTmp.GenderId = description.GenderId;
                
                _context.SaveChanges();
                return descTmp; 
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
