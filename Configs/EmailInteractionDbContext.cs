using Email_data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;
using System.Text.Json;

namespace Email_data.Configs
{
    public class EmailInteractionDbContext : DbContext
    {
        private IConfiguration _configuration { get; set; }
        public EmailInteractionDbContext(DbContextOptions<EmailInteractionDbContext> options, IConfiguration configuration) : base(options)
        {
            _configuration = configuration;
        }
        public virtual DbSet<EmailInteraction> EmailInteractions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<EmailInteraction>()
            //    .HasOne(e => e.EmailInteractionDetails)
            //    .WithOne()
            //    .OnDelete(DeleteBehavior.NoAction);

            //modelBuilder.Entity<EmailInteractionDetails>()
            //    .HasMany(e => e.BccRecipients)
            //    .WithOne()
            //    .OnDelete(DeleteBehavior.NoAction);

            //var recipientConverter = new ValueConverter<List<Recipient>, string>(
            //    v => JsonConvert.SerializeObject(v),
            //    v => JsonConvert.DeserializeObject<List<Recipient>>(v) ?? new List<Recipient>()
            //);

            modelBuilder.Entity<EmailInteraction>()
                .HasOne(e => e.EmailInteractionDetails)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseSqlServer(connectionString);
        }
    }
}
