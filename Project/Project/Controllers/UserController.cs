using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            //CreateUserViewModel();
            _userView = new UserViewModel();
        }

        private async void CreateUserViewModel()
        {
            //var user = await _userManager.FindByNameAsync(User.Identity.Name);
            //var settings = await _sR.GetSettings(user.Id);
            //if (settings == null)
            //{
            //    var settingsToAdd = new Settings()
            //    {
            //        EmailNotifications = false,
            //        GoalId = 1,
            //        Theme = true,
            //        UserId = user.Id
            //    };

            //    var uDescriptionToAdd = new UserDescription()
            //    {
            //        Age = 0,
            //        WeightKG = 0,
            //        GenderId = 1,
            //        HeightCM = 0,
            //        UserId = user.Id
            //    };
            //    await _sR.AddSettings(settingsToAdd, uDescriptionToAdd);
            //    settings = await _sR.GetSettings(user.Id) ?? new Settings();
            //}
            //var uD = await _sR.GetUserDescription(user.Id) ?? new UserDescription();
            //_userView = await UserInfoContainerFactory.GetUserInfoContainer(user, settings, uD);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            //var user = await _userManager.FindByNameAsync(User.Identity.Name);
            //var settings = await _sR.GetSettings(user.Id);
            //if (settings == null)
            //{
            //    var settingsToAdd = new Settings()
            //    {
            //        EmailNotifications = false,
            //        GoalId = 1,
            //        Theme = true,
            //        UserId = user.Id
            //    };

            //    var uDescriptionToAdd = new UserDescription()
            //    {
            //        Age = 0,
            //        WeightKG = 0,
            //        GenderId = 1,
            //        HeightCM = 0,
            //        UserId = user.Id
            //    };
            //    await _sR.AddSettings(settingsToAdd,uDescriptionToAdd);
            //    settings = await _sR.GetSettings(user.Id);
            //}
            //var uD =await _sR.GetUserDescription(user.Id);
            //UserViewModel  _userInfoContainer = await UserInfoContainerFactory.GetUserInfoContainer(user, settings, uD);//это модель которую надо использовать во view;
            
            return View("AdditionalInfo");
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserViewModel newUserView)
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
                    Age = 0,
                    WeightKG = 0,
                    GenderId = 1,
                    HeightCM = 0,
                    UserId = user.Id
                };
                await _sR.AddSettings(settingsToAdd, uDescriptionToAdd);
                settings = await _sR.GetSettings(user.Id) ?? new Settings();
            }
            var uD = await _sR.GetUserDescription(user.Id) ?? new UserDescription();
            _userView = await UserInfoContainerFactory.GetUserInfoContainer(user, settings, uD);
            _userView.Settings.GoalId = newUserView.Settings.GoalId;
            _userView.UserDescription.HeightCM = newUserView.UserDescription.HeightCM;
            _userView.UserDescription.WeightKG = newUserView.UserDescription.WeightKG;
            _userView.UserDescription.Age = newUserView.UserDescription.Age;
            _userView.UserDescription.GenderId = newUserView.UserDescription.GenderId;
            // save in database

            return RedirectToAction();
        }
    }
}
