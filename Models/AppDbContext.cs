using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventRegSystem.Models.Domain
{
    public class AppDbContext : DbContext
    {   
        public DbSet<Client> Clients { get; set; }
        public DbSet<TimeTable> TimeTables { get; set; }
        public DbSet<TimeTableClient> TimeTableClients { get; set; }
        public AppDbContext()
        {
            //Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=EventRegDB;Username=AppUser;Password=111");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*modelBuilder.Entity<Client>()
                .HasMany(tt => tt.timeTables)
                .WithMany(c => c.clients)
                ;//.Map(m => { m.ToTable("TimeTableClients"); }); ;*/
            modelBuilder.Entity<TimeTableClient>()
            .HasKey(bc => new { bc.ClientId, bc.TimeTableId });
            modelBuilder.Entity<TimeTableClient>()
                .HasOne(bc => bc.Client)
                .WithMany(b => b.TimeTableClients)
                .HasForeignKey(bc => bc.ClientId);
            modelBuilder.Entity<TimeTableClient>()
                .HasOne(bc => bc.TimeTableSpec)
                .WithMany(c => c.TimeTableClients)
                .HasForeignKey(bc => bc.TimeTableId);
        }
    }
}
