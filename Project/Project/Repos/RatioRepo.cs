using Project.Data;
using Project.Models;
using System.Security.Claims;

namespace Project.Repos
{
    public class RatioRepo : IKcalRepo<DailyRatio>
    {
        private readonly KcalContext _context;

        public RatioRepo(KcalContext context)
        {
            _context = context;
        }
        public async Task DeleteEntity(int id)
        {
            try
            {
                var ratioToDel = _context.DailyRatio.Where(r => r.Id == id).FirstOrDefault();
                if (ratioToDel != null)
                {
                    _context.DailyRatio.Remove(ratioToDel);
                    _context.SaveChanges();
                }
                
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<List<DailyRatio>> GetEntitiesList()
        {
            try
            {
                if (_context != null)
                {
                        return _context.DailyRatio.ToList();
                }
                else
                {
                    return new List<DailyRatio>();
                }
            }
            catch(Exception e)
            {
                return new List<DailyRatio>();
            }
        }

        public async Task<DailyRatio> GetEntity(int id)
        {
            try
            {
                if(_context != null)
                {
                    return _context.DailyRatio.Where(dr => dr.Id == id).FirstOrDefault() ?? null;
                }
                else
                {
                    return null;
                }
                
            }
            catch(Exception ex)
            {
                return null;
            }
            
        }

        public async Task UpdateEntity(DailyRatio entity)
        {
            
            try
            {
                _context.DailyRatio.Update(entity);
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {

            }
        }

        public async Task AddEntity(DailyRatio entity)
        {
            try
            {
                _context.DailyRatio.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task<DailyRatio> GetCurrentDailyRatio(string userId)
        {
            try
            {
                var dr = _context.DailyRatio.Where(r => r.UserId == userId&& r.Date.Date == DateTime.Now.Date).FirstOrDefault();
                return dr;
            }
            catch(Exception ex)
            {
                return null;
            }
           
          
        }
        public Task AddEntities(List<DailyRatio> entities)
        {
            throw new NotImplementedException();
        }
    }
}
