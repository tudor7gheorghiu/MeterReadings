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

        public async Task<IEnumerable<ReadingReport>> UploadMeterReading(IFormFile file)
        {
            var reports = new List<ReadingReport>();

            using (var stream = new StreamReader(file.OpenReadStream()))
            {
                using (var csv = new CsvReader(stream, CultureInfo.InvariantCulture))
                {
                    var readings = csv.GetRecords<MeterReading>();
                    foreach (var reading in readings)
                    {
                        var validation = _validator.ValidateReport(reading);
                        if (validation.IsValid)
                        {
                            _context.Add(reading);
                        }

                        reports.Add(validation);
                    }

                    await _context.SaveChangesAsync();
                }
            }

            return reports;
        }
    }
}
