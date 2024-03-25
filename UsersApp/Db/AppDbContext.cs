using Microsoft.EntityFrameworkCore;

namespace UsersApp.Db
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        private string _connectionString;


        public AppDbContext()
        {

        }

        public AppDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("users_pkey");
                entity.HasIndex(e => e.Email).IsUnique();

                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Email).HasMaxLength(255).HasColumnName("email");

                entity.Property(e => e.Password).HasColumnName("password");                

                entity.Property(e => e.RoleId).HasConversion<int>();
            });

            modelBuilder
                .Entity<Role>()
                .Property(e => e.RoleType)
                .HasConversion<int>();

            modelBuilder
                .Entity<Role>().HasData(
                Enum.GetValues(typeof(RoleType))
                .Cast<RoleType>()
                .Select(e => new Role
                {
                    RoleType = e,
                    Name = e.ToString()
                }));
        }
    }
}
