using Project.Data;
using Project.Models;

namespace Project.Repos
{
    public class FoodRepo:IKcalRepo<Food>
    {
        private readonly KcalContext _context;

        public FoodRepo(KcalContext context)
        {
            _context = context;
        }
        public async Task<Food> GetEntity(int id)
        {
            try
            {
                return _context.Foods.Where(f => f.Id == id).FirstOrDefault();
            }
            catch(Exception ex)
            {
                return null;
            }
            
        }

        public async Task<List<Food>> GetEntitiesList()
        {
            try
            {
                return _context.Foods.ToList();
            }
            catch(Exception ex)
            {
                return new List<Food>();
            }
            
        }

        public async Task DeleteEntity(int id)
        {
            try
            {
                var foodToDel = _context.Foods.Where(f => f.Id == id).FirstOrDefault();
                _context.Foods.Remove(foodToDel);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
            
        }

        public async Task UpdateEntity( Food entity)
        {
            try
            {
                if (entity != null)
                {
                    _context.Foods.Update(entity);
                    _context.SaveChanges();
                }
            }
            catch( Exception ex)
            {

            }
            
            

        }

        public async Task AddEatenFood(EatenFood eFood)
        {
            try
            {
                _context.EatenDishes.Add(eFood);
                await _context.SaveChangesAsync();
            }
            catch(Exception ex) 
            { 

            }
        }

        public async Task AddEntity(Food entity)
        {
            try
            {
                _context.Foods.Add(entity);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }
        }

        public async Task AddEntities(List<Food> entities)
        {
            _context.AddRangeAsync(entities);
            _context.SaveChanges();
        }
    }
}
