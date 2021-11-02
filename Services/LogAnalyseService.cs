using System.Threading.Tasks;
using WebLogAnalyser.Interfaces;
using WebLogAnalyser.Models;

namespace WebLogAnalyser.Services
{
    public class LogAnalyseService
    {
        private readonly ILogRepository _repository;

        public LogAnalyseService(ILogRepository repository)
        {
            _repository = repository;
        }
        public Task<LogFile> GetLogFileInfoAsync()
        {
            return Task.FromResult(_repository.GetLogFileInformation());
        }
    }
}
