namespace MeterReadings.Models
{
    public class PostReadingResponse
    {
        public List<ReadingReport>? Reports { get; set; }
        public int SuccesfulEntries { get; set; }
        public int FailedEntries { get; set; }
    }
}
