using Microsoft.EntityFrameworkCore;

namespace Dojo_Connect.Models {

    public class MyContext : DbContext {

        public MyContext(DbContextOptions options) : base(options) {}
        public DbSet<User> Users {get;set;}
        public DbSet<Message> ChatLog {get;set;}
    }
}