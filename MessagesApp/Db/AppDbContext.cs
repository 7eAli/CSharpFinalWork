using Microsoft.EntityFrameworkCore;

namespace MessagesApp.Db
{
    public class AppDbContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

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
            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("messages_pkey");
                entity.HasIndex(e => e.Id).IsUnique();

                entity.ToTable("messages");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Content).HasMaxLength(255).HasColumnName("content");
                entity.Property(e => e.IsRead).HasColumnName("read");
                entity.Property(e => e.SenderId).HasColumnName("sender");
                entity.Property(e => e.ReceiverId).HasColumnName("receiver");
            });
        }
    }
}
