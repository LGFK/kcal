using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project.Data;
using Project.Models;
using Project.Repos;
using Project.Utilities;
using Project.ViewModels;
using System.Security.Claims;

namespace Project.Controllers
{
    public class UserController : Controller
    {
        
        UserManager<IdentityUser> _userManager;
        private readonly SettingsRepo _sR;
        UserViewModel _userView;


        public UserController(UserManager<IdentityUser> userManager, SettingsRepo Sr)
        {
            _userManager = userManager;
            _sR = Sr;
            
            _userView = new UserViewModel();
        }

        private async Task<UserViewModel>  CreateUserViewModel()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var settings = await _sR.GetSettings(user.Id);
            if (settings == null)
            {
                var settingsToAdd = new Settings()
                {
                    EmailNotifications = false,
                    GoalId = 1,
                    Theme = true,
                    UserId = user.Id
                };

                var uDescriptionToAdd = new UserDescription()
                {
                    Age = 18,
                    WeightKG = 70,
                    GenderId = 1,
                    HeightCM = 170,
                    UserId = user.Id
                };
                await _sR.AddSettings(settingsToAdd, uDescriptionToAdd);
                settings = await _sR.GetSettings(user.Id) ?? new Settings();
            }
            var uD = await _sR.GetUserDescription(user.Id) ?? new UserDescription();
            _userView = await UserInfoContainerFactory.GetUserInfoContainer(user, settings, uD);
            return _userView;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
           
            var vm = await CreateUserViewModel();
            return View("AdditionalInfo",vm);
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserViewModel userSettingsFromForm)
        {
            var a = userSettingsFromForm.UserDescription.GenderId;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            _sR.UpdateDescription(user.Id, userSettingsFromForm.UserDescription);
            _sR.UpdateSettings(user.Id, userSettingsFromForm.Settings);
            return RedirectToAction("Index","DailyRatios");
        }
    }
}
