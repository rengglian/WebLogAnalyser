
using System.Collections.Generic;
using WebLogAnalyser.Models;

namespace WebLogAnalyser.Interfaces
{
    public interface ILogRepository
    {
        IEnumerable<LogEntry> GetLogEntries();
        void SetLoglines(IEnumerable<string> loglines);
    }
}
