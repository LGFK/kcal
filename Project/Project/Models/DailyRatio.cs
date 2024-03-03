using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class DailyRatio
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double DailyKcalGoal { get; set; }
        public double CcalAlreadyUsed { get; set; }
        [ForeignKey("FK_USER_ID")]
        public string UserId { get; set; }
    }
}
