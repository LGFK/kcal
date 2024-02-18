using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class UserDescription
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("FK_USER_ID")]
        public int UserId { get; set; }
        public int HeightCM { get; set; }
        public int WeightKG { get; set; }
        public int Age { get; set; }
        [ForeignKey("FK_GENDER_ID")]
        public int GenderId { get; set; }
    }
}

