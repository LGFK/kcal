using System.Configuration;
using Project.Models;

namespace Project.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string? Email { get; set; }



        public UserDescription UserDescription { get; set; }

        public Settings Settings { get; set; }


    }
}
