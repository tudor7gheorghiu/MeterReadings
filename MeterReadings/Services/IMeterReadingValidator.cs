using MeterReadings.Models;

namespace MeterReadings.Services
{
    public interface IMeterReadingValidator
    {
        public ReadingReport ValidateReport(MeterReading reading);
    }
}
