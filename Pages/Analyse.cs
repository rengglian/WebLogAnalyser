using Microsoft.AspNetCore.Components;
using Radzen;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebLogAnalyser.Models;
using WebLogAnalyser.Services;

namespace WebLogAnalyser.Pages
{
    public partial class Analyse : ComponentBase
    {
        [Inject]
        public virtual DialogService DialogService { get; set; }
        
        [Inject]
        public virtual LogAnalyseService LogAnalyseService { get; set; }

        private LogFile logFile;
        IList<LogEntry> selectedEntries;
        int progress;
        string info;

        public void Dispose()
        {
            // The DialogService is a singleton so it is advisable to unsubscribe.
            //DialogService.OnOpen -= Open;
            //DialogService.OnClose -= Close;
        }

        void OnProgress(UploadProgressArgs args, string name)
        {
            this.info = $"% '{name}' / {args.Loaded} of {args.Total} bytes.";
            this.progress = args.Progress;
        }

        async Task OnComplete(UploadCompleteEventArgs args)
        {
            _ = ShowBusyDialog();
            logFile = await LogAnalyseService.GetLogFileInfoAsync();
            // Close the dialog
            DialogService.Close();
        }

        private async Task DoubleClickCallBack(DataGridRowMouseEventArgs<LogEntry> args)
        {
            var data = new List<LogEntry>
            {
                args.Data
            };
            await OpenEfbValueDialog(data);
        }

        private async Task OnAnalyse()
        {
            await OpenEfbValueDialog(selectedEntries);
        }

        public async Task OpenEfbValueDialog(IEnumerable<LogEntry> selectedEntries)
        {
            await DialogService.OpenAsync<DialogLineChartPage>($"{logFile.FileName}",
                new Dictionary<string, object>() { { "SelectedEntries", selectedEntries } },
                new DialogOptions() { Width = "1200px", Height = "600px", Resizable = true, Draggable = true });
        }

        async Task ShowBusyDialog()
        {
            await BusyDialog("Parsing File ...");
        }

        // Busy dialog from string
        async Task BusyDialog(string message)
        {
            await DialogService.OpenAsync("", ds =>
            {
                void content(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder b)
                {
                    b.OpenElement(0, "div");
                    b.AddAttribute(1, "class", "row");

                    b.OpenElement(2, "div");
                    b.AddAttribute(3, "class", "col-md-12");

                    b.AddContent(4, message);

                    b.CloseElement();
                    b.CloseElement();
                }
                return content;
            }, new DialogOptions() { ShowTitle = false, Style = "min-height:auto;min-width:auto;width:auto" });
        }

        static void RowRender(RowRenderEventArgs<LogEntry> args)
        {

            switch (args.Data.Type)
            {

                case "E":
                    args.Attributes.Add("style", $"background-color: red;");
                    break;
                case "I":
                    args.Attributes.Add("style", $"background-color: green;");
                    break;
                default:

                    break;

            }
        }
    }
}
