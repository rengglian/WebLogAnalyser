using System.Collections.Generic;
using WebLogAnalyser.Interfaces;
using WebLogAnalyser.Models;

namespace WebLogAnalyser.Repositories
{
    public class InMemoryLogRepository : ILogRepository
    {
        private LogFile _logFile;

        public void SetLoglines(string filename, IEnumerable<string> loglines)
        {
            var logEntries = new List<LogEntry>();
            foreach (var logline in loglines)
            {
                string[] splits = logline.Split("\t");
                LogEntry entry = new()
                {
                    Date = splits[0],
                    Type = splits[1],
                    Payload = string.Join("\t", splits, 2, splits.Length - 2)
                };
                logEntries.Add(entry);
            }
            _logFile = new();
            _logFile.LogEntries = logEntries;
            _logFile.FileName = filename;
        }

        public LogFile GetLogFileInformation()
        {
            return _logFile;
        }
    }
}
