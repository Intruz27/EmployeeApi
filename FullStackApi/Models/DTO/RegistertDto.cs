using System.ComponentModel.DataAnnotations;

namespace FullStackApi.Models.DTO
{
    public class RegistertDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]

        public string Username { get; set; }   
        
        [Required]
        [DataType(DataType.Password)]

        public string Password { get; set; }

        public string[] Roles { get; set; }

    }
}
