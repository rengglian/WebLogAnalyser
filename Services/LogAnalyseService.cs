using System.Collections.Generic;
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
        public Task<IEnumerable<LogEntry>> GetLogLinesAsync()
        {
            return Task.FromResult(_repository.GetLogEntries());
        }
    }
}
