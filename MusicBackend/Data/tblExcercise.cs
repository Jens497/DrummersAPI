using System.ComponentModel.DataAnnotations;


namespace MusicBackend.Data
{
    public class tblExcercise
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Name { get; set; }
        public string Filename { get; set; }

    }
}
