using MeterReadings.Models;
using MeterReadings.Services;
using Microsoft.AspNetCore.Mvc;

namespace MeterReadings.Controllers
{
    [ApiController]
    [Route("api")]
    public class MeterReadingController : ControllerBase
    {
        private readonly ILogger<MeterReadingController> _logger;
        private readonly IMeterReadingUploader _uploader;
        private readonly IAccountsUploader _accountsUploader;

        public MeterReadingController(ILogger<MeterReadingController> logger, IMeterReadingUploader uploader, 
                                        IAccountsUploader accountsUploader)
        {
            _logger = logger;
            _uploader = uploader;
            _accountsUploader = accountsUploader;
        }

        [HttpPost]
        [Route("meter-reading-uploads")]
        public async Task<IEnumerable<ReadingReport>> MeterReadingUploads(IFormFile file)
        {
            if (file == null)
            {
                _logger.LogError("Please upload a file");
                return Enumerable.Empty<ReadingReport>();
            }

            var reports = await _uploader.UploadMeterReading(file);

            return reports;
        }

        [HttpPost]
        [Route("accounts")]
        public async Task AccountsUploads(IFormFile file)
        {
            if (file == null)
            {
                _logger.LogError("Please upload a file");
                return;
            }

            await _accountsUploader.UploadAccounts(file);

        }
    }
}
