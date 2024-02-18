using System.ComponentModel.DataAnnotations;

namespace Project.Models
{
    public class Goals
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
