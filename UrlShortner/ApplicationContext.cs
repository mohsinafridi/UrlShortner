using Microsoft.EntityFrameworkCore;
using UrlShortner.Entities;
using UrlShortner.Services;

namespace UrlShortner
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext(DbContextOptions options)
            :base(options)
        {
        }

        public DbSet<ShortenedUrl> ShortenedUrl { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShortenedUrl>(builder =>
            {
                builder.Property(s => s.Code).HasMaxLength(UrlShorteningService.NumberofCharsInShortLink);

                builder.HasIndex(x => x.Code).IsUnique();
            });
        }
    }

    
}
