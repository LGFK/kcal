using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class EatenFood
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("FK_DISH_ID")]
        public int DishId { get; set; }
        [ForeignKey("FK_DR_ID")]
        public int DailyRatioId { get; set; }
       
        public double Weight { get; set; }
    }
}
