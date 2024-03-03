using System.Configuration;

namespace Project.Models
{
    public class UserInfoContainer
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string? Email { get; set; }

        

        public UserDescription UserDescription { get; set; }

        public Settings Settings { get; set; }


    }
}
