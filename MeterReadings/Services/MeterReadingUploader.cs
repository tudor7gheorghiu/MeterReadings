using CsvHelper;
using MeterReadings.DataAccessLayer;
using MeterReadings.Models;
using System.Globalization;

namespace MeterReadings.Services
{
    internal class MeterReadingUploader : IMeterReadingUploader
    {
        private readonly IMeterReadingValidator _validator;
        private readonly ReadingsDbContext _context;
        
        public MeterReadingUploader(IMeterReadingValidator validator, ReadingsDbContext readingsDbContext) 
        { 
            _validator = validator;
            _context = readingsDbContext;
        }

        public async Task<PostReadingResponse> UploadMeterReading(IFormFile file)
        {
            var reports = new List<ReadingReport>();
            var successful = 0;
            var failed = 0;

            // Process csv file
            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                using (var csv = new CsvReader(stream, CultureInfo.InvariantCulture))
                {
                    var readings = csv.GetRecords<MeterReadingCSV>();
                    
                    // Match csv file line
                    var counter = 2;
                    foreach (var reading in readings)
                    {
                        var readingDb = new MeterReading {
                            MeterReadingId = reading.AccountId + reading.MeterReadValue + counter,
                            AccountId = reading.AccountId,  
                            ReadignValie = reading.MeterReadValue,
                            ReadingTime = DateTime.Parse(reading.MeterReadingDateTime)
                        };
                        var validation = _validator.ValidateReport(readingDb, counter);
                        if (validation.IsValid)
                        {
                            _context.Add(readingDb);
                            successful++;
                        }
                        else 
                        {
                            failed++;
                        }

                        reports.Add(validation);
                        counter++;
                    }

                    await _context.SaveChangesAsync();
                }
            }

            return new PostReadingResponse 
            { Reports = reports, SuccesfulEntries = successful, FailedEntries = failed};
        }
    }
}
