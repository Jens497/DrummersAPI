using System;


//Is this class actually nescessary can we just use the DTO object and the tblUser object?

namespace MusicBackend.Models
{
    public class UserRequest
    {
        public int Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }
        public string? Email { get; set; } = default;
        public string? Firstname { get; set; } = default;
        public string? Lastname { get; set; } = default;
    }
}
