using MeterReadings.Models;

namespace MeterReadings.Services
{
    public interface IMeterReadingUploader
    {
        public Task<IEnumerable<ReadingReport>> UploadMeterReading(IFormFile file);
    }
}
