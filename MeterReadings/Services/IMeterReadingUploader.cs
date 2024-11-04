using MeterReadings.Models;

namespace MeterReadings.Services
{
    public interface IMeterReadingUploader
    {
        public Task<PostReadingResponse> UploadMeterReading(IFormFile file);
    }
}
