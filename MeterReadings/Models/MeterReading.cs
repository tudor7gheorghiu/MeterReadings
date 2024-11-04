using System.ComponentModel.DataAnnotations;

namespace MeterReadings.Models
{
    public class MeterReading
    {
        [Key]
        public string MeterReadingId { get; set; } = string.Empty;
        public string AccountId { get; set; } = string.Empty;
        public DateTime ReadingTime { get; set; }
        public int ReadignValie { get; set; }
    }
}
