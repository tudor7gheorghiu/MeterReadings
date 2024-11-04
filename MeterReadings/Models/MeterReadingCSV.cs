namespace MeterReadings.Models
{
    public class MeterReadingCSV
    {
        public string AccountId { get; set; } = string.Empty;
        public string MeterReadingDateTime { get; set; } = string.Empty;
        public int MeterReadValue { get; set; }
    }
}
