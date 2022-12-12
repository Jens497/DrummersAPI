using System.ComponentModel.DataAnnotations;

namespace MusicBackend.Dtos
{
    public class UserDto
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; init; }
        public string Email { get; init; }
        public string Firstname { get; init; }
        public string Lastname { get; init; }
    }
}
