using System.Collections.Generic;
using WebLogAnalyser.Interfaces;
using WebLogAnalyser.Models;

namespace WebLogAnalyser.Repositories
{
    public class InMemoryLogRepository : ILogRepository
    {
        private List<LogEntry> _logEntries;

        public void SetLoglines(IEnumerable<string> loglines)
        {
            _logEntries = new List<LogEntry>();
            foreach (var logline in loglines)
            {
                string[] splits = logline.Split("\t");
                LogEntry entry = new()
                {
                    Date = splits[0],
                    Type = splits[1],
                    Payload = string.Join("\t", splits, 2, splits.Length - 2)
                };
                _logEntries.Add(entry);
            }
        }

        public IEnumerable<LogEntry> GetLogEntries()
        {
            return _logEntries;
        }
    }
}
