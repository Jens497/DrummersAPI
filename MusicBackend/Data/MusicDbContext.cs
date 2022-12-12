using Microsoft.EntityFrameworkCore;
using System;

namespace MusicBackend.Data
{
    public class MusicDbContext : DbContext
    {
        public MusicDbContext(DbContextOptions<MusicDbContext> options) : base(options)
        {

        }

        //The "method" name in this case "User" is the name of the table in the database.
        public DbSet<tblUser> User { get; set; }
        //The "method" name in this case i Excercise because its the name of the table in the database so to use the context you do....
        //dbcontext.Excercise.functionwanted
        public DbSet<tblExcercise> Excercise { get; set; }
    }
}
