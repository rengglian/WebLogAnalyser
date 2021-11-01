using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using WebLogAnalyser.Interfaces;

namespace WebLogAnalyser.Controllers
{
    [DisableRequestSizeLimit]
    public class UploadController : Controller
    {
        private IEnumerable<string> _logLines;

        private readonly IWebHostEnvironment _environment;
        private readonly ILogRepository _repository;
        private readonly ILogger<UploadController> _logger;


        public UploadController(IWebHostEnvironment environment, ILogRepository repository, ILogger<UploadController> logger)
        {
            _environment = environment;
            _repository = repository;
            _logger = logger;
        }

        [HttpPost("upload/single")]
        public IActionResult Single(IFormFile file)
        {
            try
            {
                _logger.LogInformation($"Single: {file.FileName}");
                 _ = UploadFile(file);
                 return StatusCode(200);
            }
            catch (Exception ex)
            {
                
                return StatusCode(500, ex.Message);
            }
        }

        public async Task UploadFile(IFormFile file)
        {

            if (file != null && file.Length > 0)
            {
                var filePath = @"/tmp";
                var uploadPath = _environment.WebRootPath+filePath;
                if(!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }
                _logger.LogInformation($"UploadFile First: {file.FileName}");
                _logLines = ReadLogLines(file);
                _repository.SetLoglines(file.FileName, _logLines);
                _logger.LogInformation($"UploadFile Second: {file.FileName}");
                var fullPath = Path.Combine(uploadPath, file.FileName);
                using FileStream fileStream = new(fullPath, FileMode.Create, FileAccess.Write);
                await file.CopyToAsync(fileStream);
            }
        }

        public string[] GetLogLines()
        {
            _logger.LogInformation($"GetLines");
            return _logLines.ToArray();
        }
    
        private IEnumerable<string> ReadLogLines(IFormFile file) {
            _logger.LogInformation($"ReadLogLines: {file.FileName}");
            using StreamReader reader = new(file.OpenReadStream());
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                yield return line;
            }
        } 
    }
}