using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        private readonly ILogRepository _repository;
        private readonly ILogger<UploadController> _logger;

        public UploadController(ILogRepository repository, ILogger<UploadController> logger)
        {
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
                _logger.LogInformation($"UploadFile First: {file.FileName}");
                _logLines = ReadLogLines(file);
                await _repository.SetLoglinesAsync(file.FileName, _logLines);
                _logger.LogInformation($"UploadFile Second: {file.FileName}");
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
            List<string> lines = new();
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                lines.Add(line);
            }
            return lines;
        } 
    }
}