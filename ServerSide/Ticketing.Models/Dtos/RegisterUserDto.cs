using System.ComponentModel.DataAnnotations;
using Ticketing.Models.DbModels;

namespace Ticketing.Models.Dtos
{
    public class RegisterUserDto : UserDto
    {
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}