using CsvHelper;
using MeterReadings.DataAccessLayer;
using MeterReadings.Models;
using System.Globalization;

namespace MeterReadings.Services
{
    public class AccountsUploader : IAccountsUploader
    {
        private readonly ReadingsDbContext _context;

        public AccountsUploader(ReadingsDbContext context)
        {
            _context = context;
        }

        public async Task UploadAccounts (IFormFile file)
        {
            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                using (var csv = new CsvReader(stream, CultureInfo.InvariantCulture))
                {
                    var accounts = csv.GetRecords<Account>();
                    foreach (var account in accounts)
                    {
                        _context.Add(account);
                    }

                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
