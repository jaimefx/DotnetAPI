using DotnetAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace DotnetAPI.Data
{
    public class DataContextEF<T> : DbContext where T:class
    {
        private readonly IConfiguration _config;
        private static readonly string schemaName = "TutorialAppSchema";
        public DataContextEF(IConfiguration config)
        {
            _config = config;
        }

        public virtual DbSet<T> Context { get;set;}
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder){
            if(!optionsBuilder.IsConfigured){
                optionsBuilder
                    .UseSqlServer(_config.GetConnectionString("DefaultConnection"), optionsBuilder => optionsBuilder.EnableRetryOnFailure());
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("Users", schemaName)
                .HasKey(u => u.UserId);

            modelBuilder.Entity<UserSalary>()
                .ToTable("UserSalary", schemaName)
                .HasKey(u => u.UserId);
            
            modelBuilder.Entity<UserJobInfo>()
                .ToTable("UserJobInfo", schemaName)
                .HasKey(u => u.UserId);

            modelBuilder.Entity<Post>()
                .ToTable("Posts", schemaName)
                .HasKey(u => u.PostId);

        }
    }
}