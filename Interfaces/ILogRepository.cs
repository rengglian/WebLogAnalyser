
using System.Collections.Generic;
using WebLogAnalyser.Models;

namespace WebLogAnalyser.Interfaces
{
    public interface ILogRepository
    {
        LogFile GetLogFileInformation();
        void SetLoglines(string filename, IEnumerable<string> loglines);
    }
}
