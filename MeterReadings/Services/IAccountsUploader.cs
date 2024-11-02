namespace MeterReadings.Services
{
    public interface IAccountsUploader
    {
        public Task UploadAccounts(IFormFile file);
    }
}
