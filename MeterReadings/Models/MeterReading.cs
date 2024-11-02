namespace MeterReadings.Models
{
    public class MeterReading
    {
        public int Id { get; set; }
        public string AccountId { get; set; } = string.Empty;
        public DateTime ReadingTime { get; set; }
        public int ReadignValie { get; set; }
    }
}
