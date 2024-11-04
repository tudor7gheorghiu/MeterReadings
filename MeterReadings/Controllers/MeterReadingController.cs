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
        public async Task<PostReadingResponse> MeterReadingUploads(IFormFile file)
        {
            if (file == null)
            {
                _logger.LogError("Please upload a file");
                return new PostReadingResponse();
            }
            try
            {
                var reports = await _uploader.UploadMeterReading(file);
                return reports;
            }

            catch (Exception ex) 
            {
                _logger.LogError($"{ex.Message}", ex);
                return new PostReadingResponse();
            }
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

            try
            {

                await _accountsUploader.UploadAccounts(file);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{ex.Message}", ex);
            }

        }
    }
}
