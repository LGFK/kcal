using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Models
{
    public class Settings
    {
        [Key]
        public int Id { get; set; }
        [ForeignKey("FK_USER_ID")]
        public string UserId { get; set; }
        [ForeignKey("FK_GOAL_ID")]
        public int GoalId { get; set; }
        public bool? EmailNotifications { get; set; }
        
        public bool Theme { get; set; }
    }
}
