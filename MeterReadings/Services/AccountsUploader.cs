using CsvHelper;
using MeterReadings.DataAccessLayer;
using MeterReadings.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Globalization;

namespace MeterReadings.Services
{
    public class AccountsUploader : IAccountsUploader
    {
        private readonly ReadingsDbContext _context;
        private readonly IMemoryCache _memoryCache;

        public AccountsUploader(ReadingsDbContext context, IMemoryCache memoryCache)
        {
            _context = context;
            _memoryCache = memoryCache;
        }

        public async Task UploadAccounts (IFormFile file)
        {
            // Read csv and convert to reaquired model
            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                using (var csv = new CsvReader(stream, CultureInfo.InvariantCulture))
                {
                    var accounts = csv.GetRecords<Account>();

                    // Cache accounts on each modification
                    var cachedAccounts = new List<Account>();

                    foreach (var account in accounts)
                    {
                        _context.Add(account);
                        cachedAccounts.Add(account);
                    }

                    var cacheEntryOptions = new MemoryCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromDays(20));
                    _memoryCache.Set("Accounts", cachedAccounts, cacheEntryOptions);

                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
