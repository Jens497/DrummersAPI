using System;
using System.ComponentModel.DataAnnotations;

namespace MusicBackend.Data
{
    public class tblUser
    {
        [Key]
        public int Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; }    
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
    }
}
