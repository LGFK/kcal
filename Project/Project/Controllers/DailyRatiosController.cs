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

namespace Project.Controllers
{
    public class DailyRatiosController : Controller
    {
        private readonly FoodRepo fRepo;
        private readonly RatioRepo ratioRepo;
        
        private string userId;
        

        public DailyRatiosController(FoodRepo fRepo, RatioRepo ratioRepo)
        {
            this.fRepo = fRepo;
            this.ratioRepo = ratioRepo;
            
            
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var dailyR = (await ratioRepo.GetEntitiesList()).Where(e => e.UserId == userId).FirstOrDefault();
                if (dailyR != null)
                {
                    if (DateTime.Now.Date != dailyR.Date)
                    {
                        dailyR = new DailyRatio()
                        {
                            DailyKcalGoal = 0,
                            Date = DateTime.Now.Date,
                            CcalAlreadyUsed = 0,
                            UserId = userId
                        };
                        await ratioRepo.AddEntity(dailyR);
                    }
                   
                }
                else
                {
                    dailyR = new DailyRatio()
                    {
                        DailyKcalGoal = 0,
                        Date = DateTime.Now.Date,
                        CcalAlreadyUsed = 0,
                        UserId = userId
                    };
                    await ratioRepo.AddEntity(dailyR);
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

        public async Task<IActionResult> SearchForProduct(string productName = "nutella")
        {
            try
            {
                this.userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var openFoodDbURL = $"https://world.openfoodfacts.org/cgi/search.pl?search_terms={productName}&search_simple=1&action=process&json=1&fields=product_name,carbohydrates_100g,fat_100g,proteins_100g,energy-kcal_100g";
                var food = (await fRepo.GetEntitiesList()).Where(f=>f.Name.ToLower().Contains(productName.ToLower())).ToList();
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
                        if (foodToAddToLocalDb.Any())
                        {
                            await fRepo.AddEntities(foodToAddToLocalDb);
                        }
                        food = (await fRepo.GetEntitiesList()).Where(f => f.Name == productName).ToList();

                    }

                }
                else
                {
                    //var prodToAdd = new Food()
                    //{
                    //    KcalPer100g = prod.energykcal_100g,
                    //    Carbohydrates = prod.carbohydrates_100g,
                    //    Fats = prod.fat_100g,
                    //    Name = prod.product_name,
                    //    Proteins = prod.proteins_100g
                    //};
                }
                return View("SearchForProduct", food); //надо дописать что возвращает view View(food)


            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<IActionResult> EatDishesInCart(int id, double weight)
        {
            try
            {
                var currDailyRatio = await ratioRepo.GetCurrentDailyRatio(userId);

                var eatenFoodToAdd = new EatenFood()
                {
                    DailyRatioId = currDailyRatio.Id,
                    DishId = id,
                    Weight = weight
                };
            }
            catch(Exception ex)
            {

            }
            

            return View("Index");
        }

      public async Task<IActionResult> AddToEatenFoodCart(int productId,double weight)
      {
            try
            {
                var foodToEat = (await fRepo.GetEntitiesList()).Where(f => f.Id == productId).FirstOrDefault();
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
                    Weight = weight

                };

                cart?.Add(eatenFood);


                HttpContext.Session.SetString("Cart", JsonConvert.SerializeObject(cart));
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return null; //придумать механизм с exceptions 
            }

            
        }
    }
}