using System.ComponentModel.DataAnnotations;

namespace dotNet_GMZ_backend.Models.ModelsDTO
{
    public class UserLoginDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}