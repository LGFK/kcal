using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;
using Project.Repos;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.ComponentModel;

namespace Project.Controllers
{
    public class DailyRatiosController : Controller
    {
        private readonly FoodRepo _fRepo;
        private readonly RatioRepo _ratioRepo;
        private readonly SettingsRepo _settingsRepo;
        private string userId;
        
        

        public DailyRatiosController(FoodRepo fRepo, RatioRepo ratioRepo,SettingsRepo settingsRepo)
        {
           
            this._fRepo = fRepo;
            this._ratioRepo = ratioRepo;
            this._settingsRepo = settingsRepo;
            

            
            
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                this.userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var dailyR = (await _ratioRepo.GetEntitiesList()).Where(e => e.UserId == userId).FirstOrDefault();
                var dailyKcalGoal = GetNormOfCalories();
                if (dailyR != null)
                {
                    
                    if (DateTime.Now.Date != dailyR.Date)
                    {
                        dailyR = new DailyRatio()
                        {
                            DailyKcalGoal =await dailyKcalGoal,
                            Date = DateTime.Now.Date,
                            CcalAlreadyUsed = 0,
                            UserId = userId
                        };
                        await _ratioRepo.AddEntity(dailyR);
                    }
                   
                }
                else
                {
                    dailyR = new DailyRatio()
                    {
                        DailyKcalGoal = await dailyKcalGoal,
                        Date = DateTime.Now.Date,
                        CcalAlreadyUsed = 0,
                        UserId = userId
                    };
                    await _ratioRepo.AddEntity(dailyR);
                }
                return View(dailyR);
            }
            catch (Exception ex)
            {
                return View(new DailyRatio()
                {
                    DailyKcalGoal = 0,
                    Date = DateTime.Now.Date,
                    CcalAlreadyUsed = 0,
                    UserId = "ERROR/"+ex.Message
                });

            }

        }

        public async Task<IActionResult> SearchForProduct(string productName = "")
        {
            try
            {
                if (!string.IsNullOrEmpty(productName))
                {
                    this.userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                    var openFoodDbURL = $"https://world.openfoodfacts.org/cgi/search.pl?search_terms={productName}&search_simple=1&action=process&json=1&fields=product_name,carbohydrates_100g,fat_100g,proteins_100g,energy-kcal_100g";
                    var food = (await _fRepo.GetEntitiesList()).Where(f => f.Name.ToLower().Contains(productName.ToLower())).ToList();
                    if (food.Count() < 5 || !food.Any())
                    {
                        HttpClient httpClient = new HttpClient();

                        var resp = await httpClient.GetStringAsync(openFoodDbURL);

                        var jObject = JObject.Parse(resp);
                        var listJson = jObject["products"].ToObject<List<JsonProduct>>();

                        if (listJson.Any())
                        {
                            List<Food> foodToAddToLocalDb = new List<Food>();
                            foreach (var prod in listJson)
                            {
                                if (!String.IsNullOrEmpty(prod.product_name))
                                {
                                    var prodToAdd = new Food()
                                    {
                                    KcalPer100g = prod.energykcal_100g,
                                    Carbohydrates = prod.carbohydrates_100g,
                                    Fats = prod.fat_100g,
                                    Name = prod.product_name,
                                    Proteins = prod.proteins_100g
                                    };
                                
                                    foodToAddToLocalDb.Add(prodToAdd);
                                }
                            }
                            if (foodToAddToLocalDb.Any())
                            {
                                await _fRepo.AddEntities(foodToAddToLocalDb);
                            }
                            food = (await _fRepo.GetEntitiesList()).Where(f => f.Name.ToLower().Contains(productName.ToLower())).ToList();

                        }

                    }
                   
                    return View("SearchForProduct", food); 
                }
                return View();

            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
        public async Task<IActionResult> EatDishesInCart()
        {
            try
            {
                this.userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var currDailyRatio = await _ratioRepo.GetCurrentDailyRatio(userId);
                var dishesInCartStr = HttpContext.Session.GetString("EatenFoodCart");
                List<EatenFood> dishesInCart = null;
                if (!string.IsNullOrEmpty(dishesInCartStr))
                {
                    dishesInCart = JsonConvert.DeserializeObject<List<EatenFood>>(dishesInCartStr);
                }
               
                if (dishesInCart == null)
                {
                    throw new Exception("No Dishes In cart");
                }
                foreach (var dish in dishesInCart)
                {
                    dish.DailyRatioId = currDailyRatio.Id;
                    await _fRepo.AddEatenFood(dish);
                    var food =  (await _fRepo.GetEntity(dish.DishId));
                    currDailyRatio.CcalAlreadyUsed += food.KcalPer100g / 100 * dish.Weight;
                }
                _ratioRepo.UpdateEntity(currDailyRatio);
                HttpContext.Session.SetString("EatenFoodCart", JsonConvert.SerializeObject(new List<EatenFood>()));
                return View("Index",currDailyRatio);
            }
            catch(Exception ex)
            {
                return null;
            }
        }

        [HttpPost]
      public async Task<IActionResult> AddToEatenFoodCart(int productId,string weight)
      {

            Double.TryParse(weight, out var w);
            try
            {
                var foodToEat = (await _fRepo.GetEntitiesList()).Where(f => f.Id == productId).FirstOrDefault();
                List<EatenFood> cart = null;
                var cartStr = HttpContext.Session.GetString("EatenFoodCart");
                if (!string.IsNullOrEmpty(cartStr))
                {
                    cart = JsonConvert.DeserializeObject<List<EatenFood>>(cartStr);
                }

                else
                {
                    cart = new List<EatenFood>();
                }


                var eatenFood = new EatenFood()
                {
                    DailyRatioId = 0,
                    DishId = productId,
                    Weight = w

                };

                cart?.Add(eatenFood);


                HttpContext.Session.SetString("EatenFoodCart", JsonConvert.SerializeObject(cart));
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return null; //придумать механизм с exceptions 
            }

            
        }

        public async Task<double> GetNormOfCalories()
        {
            var settings = await _settingsRepo.GetSettings(userId);
            var uD//userDescription
                = await _settingsRepo.GetUserDescription(userId);
            var correctorIndx = 0;
            switch (settings.GoalId)
            {
                case 1:
                    correctorIndx = -500;
                    break;
                case 2:
                    correctorIndx = 500;
                    break;
                case 3:
                    break;
                default:
                    break;
            }
            double regularAmountOfKcal = (10 * uD.WeightKG) + (6.25 * uD.HeightCM) - (5 * uD.Age);
            if (uD.GenderId == 1)
            {
                regularAmountOfKcal = (regularAmountOfKcal + 5) * 1.2;
            }
            else
            {
                regularAmountOfKcal = (regularAmountOfKcal - 161) * 1.2;
            }
            return regularAmountOfKcal - correctorIndx;
        }
    }
}