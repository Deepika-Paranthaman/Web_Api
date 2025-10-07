using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class SignUpDto
    {
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
