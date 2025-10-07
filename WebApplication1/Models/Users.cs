using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    [Table("Users")]
    public class Users
    {
        [Key]
        public int user_id { get; set; }

        [Required]
        public string full_name { get; set; }

        [Required]
        [EmailAddress]
        public string email { get; set; }

        [Required]
        [MaxLength(100)]
        public string password { get; set; }

        [Required]
        public string phone_number { get; set; }

        [Required]
        public string address { get; set; }

    }
}

