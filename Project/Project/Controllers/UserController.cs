using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Project.Data;
using Project.Models;
using Project.Repos;
using Project.Utilities;
using System.Security.Claims;

namespace Project.Controllers
{
    public class UserController : Controller
    {
        UserInfoContainer _userInfoContainer;
        UserManager<IdentityUser> _userManager;
        private readonly SettingsRepo _sR;
        

        public UserController(UserManager<IdentityUser> userManager, SettingsRepo Sr)
        {
            _userManager = userManager;
            _sR = Sr;
        }
        public async Task<IActionResult> Index()
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
                _sR.AddSettings(settingsToAdd,uDescriptionToAdd);
            }
            var uD =await _sR.GetUserDescription(user.Id);
            _userInfoContainer = await UserInfoContainerFactory.GetUserInfoContainer(user, settings, uD);//это модель которую надо использовать во view
            return View();
        }
    }
}
