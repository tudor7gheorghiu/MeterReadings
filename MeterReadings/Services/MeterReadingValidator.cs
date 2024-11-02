using MeterReadings.Models;

namespace MeterReadings.Services
{
    public class MeterReadingValidator : IMeterReadingValidator
    {
        public ReadingReport ValidateReport(MeterReading reading)
        {
            return new ReadingReport { ReportMessage = "This is Valid", IsValid = true };
        }
    }
}
