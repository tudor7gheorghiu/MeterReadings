using System.ComponentModel.DataAnnotations;

namespace MeterReadings.Models
{
    public class Account
    {
        [Key]
        public string AccountId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
