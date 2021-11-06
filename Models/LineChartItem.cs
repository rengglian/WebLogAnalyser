using System.Collections.Generic;
using System.Drawing;

namespace WebLogAnalyser.Models
{
    public class LineChartItem
    {
        public string Title { get; set; }
        public List<Point> Values { get; set; }
    }
}
