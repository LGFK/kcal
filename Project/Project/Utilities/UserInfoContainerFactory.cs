using Microsoft.AspNetCore.Identity;
using NuGet.Configuration;
using Project.Data;
using Project.Models;
using Settings = Project.Models.Settings;

namespace Project.Utilities
{
    public static class UserInfoContainerFactory
    {

        public static async Task< UserInfoContainer> GetUserInfoContainer(IdentityUser user, Settings settings, UserDescription uD)
        {
            try
            {

                var uContainer = new UserInfoContainer()
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

