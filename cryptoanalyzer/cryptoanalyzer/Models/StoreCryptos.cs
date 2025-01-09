using Microsoft.EntityFrameworkCore;

namespace cryptoanalyzer.Models
{
    public class StoreCryptos : DbContext
    {
        public DbSet<CryptoPrices> CryptoPrices { get; set; }
        public DbSet<Cryptos> Cryptos { get; set; }
        public DbSet<Cucrypto> Cucrypto { get; set; }
        public DbSet<FuturePrices> FuturePrices { get; set; }


        public StoreCryptos(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // تعریف که جدول پرایمری کی ندارد
            modelBuilder.Entity<Cucrypto>()
               .HasNoKey(); 
            modelBuilder.Entity<Cryptos>()
               .HasNoKey(); 
            modelBuilder.Entity<CryptoPrices>()
                .HasNoKey(); 
            modelBuilder.Entity<FuturePrices>()
               .HasNoKey(); 
        }
    }
}
