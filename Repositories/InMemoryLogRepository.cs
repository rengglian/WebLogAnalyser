using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WebLogAnalyser.Interfaces;
using WebLogAnalyser.Models;

namespace WebLogAnalyser.Repositories
{
    public class InMemoryLogRepository : ILogRepository
    {
        private LogFile _logFile;

        public async Task SetLoglinesAsync(string filename, IEnumerable<string> loglines)
        {
            await Task.Run(() =>
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

                var result = logEntries.Select((x, i) => new { x, i })
                  .Where(x => x.x.Payload.Contains("Enter energy calibration [Run"))
                  .Select(x => x.i).ToList();

                _logFile = new();
                _logFile.LogEntries = logEntries;
                _logFile.FileName = filename;
            });
        }

        public LogFile GetLogFileInformation()
        {
            return _logFile;
        }
    }
}
