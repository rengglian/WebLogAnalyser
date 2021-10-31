using System;

namespace WebLogAnalyser.Models
{
    public class LogEntry
    {
        public string Date { get; set; }
        public string Type { get; set; }
        public string Payload { get; set; }
    }
}
