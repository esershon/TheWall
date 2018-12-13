using Microsoft.EntityFrameworkCore;
using TheWall.Models;

namespace TheWall.Models
{
    public class MyContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        //ADD AS MANY CLASSES AS YOU HAVE IN YOUR DB HERE
        public DbSet<User> users { get; set; }
        public DbSet<Message> messages { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<MLike> mlikes { get; set; }
        public DbSet<CLike> clikes { get; set; }

    }
}
