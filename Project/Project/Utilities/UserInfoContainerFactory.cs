using Microsoft.AspNetCore.Identity;
using NuGet.Configuration;
using Project.Data;
using Project.Models;
using Project.Repos;
using Project.ViewModels;
using Settings = Project.Models.Settings;

namespace Project.Utilities
{
    public class UserInfoContainerFactory
    {
       

        public static async Task< UserViewModel> GetUserInfoContainer(IdentityUser user, Settings settings, UserDescription uD)
        {
            try
            {

                var uContainer = new UserViewModel()
                {
                    UserDescription = uD,
                    Settings = settings,
                    Email = user.Email,
                    Id = user.Id,
                    Name = user.UserName
                };


                return uContainer;
            }
            catch (Exception ex)
            {
                return null;//придумать механизм
            }


        }
    }
}

