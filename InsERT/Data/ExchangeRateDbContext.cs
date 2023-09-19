using InsERT.Models;
using Microsoft.EntityFrameworkCore;

namespace InsERT.Data
{
    public class ExchangeRateDbContext : DbContext
    {
        public ExchangeRateDbContext(DbContextOptions<ExchangeRateDbContext> options) : base(options) { }

        public DbSet<Rate> Rate { get; set; }
        public DbSet<ExchangeRate> ExchangeRate { get; set; }
    }
}
