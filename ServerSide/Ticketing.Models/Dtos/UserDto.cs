using System.ComponentModel.DataAnnotations;

namespace Ticketing.Models.Dtos
{
    public class UserDto
    {
        public int Id { get; set; }
        [Required]
        public string GivenName { get; set; }
        [Required]
        public string FamilyName { get; set; }
       
    }
}