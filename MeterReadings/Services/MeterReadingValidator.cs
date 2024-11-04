using MeterReadings.DataAccessLayer;
using MeterReadings.Models;
using Microsoft.Extensions.Caching.Memory;

namespace MeterReadings.Services
{
    public class MeterReadingValidator : IMeterReadingValidator
    {
        private readonly ReadingsDbContext _context;
        private readonly IEnumerable<Account> _accounts;
        private readonly IMemoryCache _memoryCache;

        public MeterReadingValidator(ReadingsDbContext readingsDbContext, IMemoryCache memoryCache) {
            _context = readingsDbContext;
            _memoryCache = memoryCache;
            _memoryCache.TryGetValue("Accounts", out List<Account>? cachedAccounts);

            // Try to use cached accounts first
            if (cachedAccounts?.Any() == true)
            {
                _accounts = cachedAccounts;
            }
            else
            {
                _accounts = _context.Accounts;
            }
        }

        public ReadingReport ValidateReport(MeterReading reading, int counter)
        {
            // Implement all validation criteria 

            if (!ValidateAccount(reading.AccountId) == true)
            {
                return new ReadingReport 
                { 
                    ReportMessage = "Entry" + counter + " has no valid account", 
                    IsValid = false 
                };
            }

            if(ValidateDuplicate(reading))
            {
                return new ReadingReport
                {
                    ReportMessage = "Entry" + counter + " is duplicated",
                    IsValid = false
                };
            }

            if (!ValidateReadingValue(reading.ReadignValie))
            {
                return new ReadingReport
                {
                    ReportMessage = "Entry" + counter + " has no valid reading value",
                    IsValid = false
                };
            }

            if (ValidateLatest(reading))
            {
                return new ReadingReport
                {
                    ReportMessage = "Entry" + counter + " is not the latest reading",
                    IsValid = false
                };
            }

            return new ReadingReport { ReportMessage = "Entry " + counter + " is valid", IsValid = true };
        }

        private bool? ValidateAccount(string accountId)
        {
            if (accountId != null)
            {
                return _accounts.Where(x => x.AccountId == accountId).Any();
            }

            return false;
        }

        private bool ValidateDuplicate(MeterReading reading)
        {
            return _context.MeterReadings
                .Where(x => x.AccountId == reading.AccountId && x.ReadignValie == reading.ReadignValie).Any();
        }

        private bool ValidateLatest(MeterReading reading)
        {
            return _context.MeterReadings.Where(x => x.AccountId == reading.AccountId 
                    && x.ReadingTime > reading.ReadingTime).Any();
        }

        private bool ValidateReadingValue(int readingValue) 
        {
            if (readingValue > 99999 ) {
                return false;
            }
            return true;
        }
    }
}
