using MeterReadings.Models;
using Microsoft.EntityFrameworkCore;

namespace MeterReadings.DataAccessLayer
{
    public class ReadingsDbContext : DbContext
    {
        public ReadingsDbContext(DbContextOptions<ReadingsDbContext> options): base(options) { }

        public DbSet<MeterReading> MeterReadings { get; set; }
        public DbSet<Account>  Accounts { get; set; }
    }
}
