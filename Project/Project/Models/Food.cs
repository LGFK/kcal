using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Food
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public double KcalPer100g { get; set; }
        public double Proteins { get; set; }
        public double Fats { get; set; }
        public double Carbohydrates { get; set; }
    }
}
