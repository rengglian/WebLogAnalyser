using Microsoft.AspNetCore.Components;
using Radzen;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using WebLogAnalyser.Models;

namespace WebLogAnalyser.Pages
{
    public partial class DialogLineChartPage : ComponentBase
    {
        [Inject]
        public virtual DialogService DialogService { get; set; }

        [Parameter] 
        public IEnumerable<LogEntry> SelectedEntries { get; set; }

        string Title = "Line Chart";     
        int MaxX = 0;
        readonly List<LineChartItem> lineCharts = new();
        bool showLegend = false;

        protected override void OnInitialized()
        {
            Title = $"{SelectedEntries.First().Date}";
            foreach (var entry in SelectedEntries)
            {
                var values = new List<Point>();
                string[] splits = entry.Payload.Split("\t");
                int i = 0;
                if (splits.Length > 1)
                {
                    foreach (var strValue in splits)
                    {
                        if (int.TryParse(strValue, out int val))
                        {
                            values.Add(new Point { Y = val, X = i });
                            i++;
                            if (i > MaxX) MaxX = i;
                        }
                    }
                }
                else
                {
                    values.Add(new Point { Y = 0, X = i });
                }
                lineCharts.Add(new LineChartItem { Title = splits[0], Values = values });
            }
        }
    }
}
