
using System.Collections.Generic;
using System.Threading.Tasks;
using WebLogAnalyser.Models;

namespace WebLogAnalyser.Interfaces
{
    public interface ILogRepository
    {
        LogFile GetLogFileInformation();
        Task SetLoglinesAsync(string filename, IEnumerable<string> loglines);
    }
}
