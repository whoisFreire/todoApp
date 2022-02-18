using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TodoApp.Models;

namespace TodoApp.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }
        public DbSet<User> Users { get; set; }

        private IConfiguration _configuration;

        public AppDbContext(IConfiguration iconfig)
        {
            _configuration = iconfig;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            var dataSource = this._configuration.GetValue<string>("Connection:dataSource");
            var cache = this._configuration.GetValue<string>("Connection:cache");
            
            optionsBuilder.UseSqlite("DataSource={dataSource}.db;cache={cache}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Todo>()
                .HasOne(todo => todo.User)
                .WithMany(user => user.Todos)
                .HasForeignKey(todo => todo.UserForeignKey)
                .HasPrincipalKey(user => user.Id);

        }
    }
}