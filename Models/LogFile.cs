using System.Collections.Generic;

namespace WebLogAnalyser.Models
{
    public class LogFile
    {
        public string FileName { get; set; }
        public IEnumerable<LogEntry> LogEntries { get; set; }
        public IEnumerable<IEnumerable<LogEntry>> Runs { get; set; }
}
}
