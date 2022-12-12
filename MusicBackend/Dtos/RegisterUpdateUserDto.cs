using System.ComponentModel.DataAnnotations;

namespace MusicBackend.Dtos
{
    public class RegisterUpdateUserDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; init; }
        [Required]
        public string Password { get; init; }
        public string Email { get; init; }
        public string Firstname { get; init; }
        public string Lastname { get; init; }
    }
}
