using Serilog.Events;
using Serilog.Formatting;
using System.IO;

namespace CloudwatchLogger
{
    public class CustomLogFormatter : ITextFormatter
    {
        public void Format(LogEvent logEvent, TextWriter output)
        {
            if (logEvent != null && output != null)
            {
                Render(logEvent, output);
            }
        }

        private void Render(LogEvent logEvent, TextWriter output)
        {
            //output.Write("Timestamps - {0} | Level - {1} | Message {2} {3} {4}", logEvent.Timestamp, logEvent.Level, logEvent.MessageTemplate, JsonConvert.SerializeObject(logEvent.Properties), output.NewLine);

            output.FirstProperty("severity", logEvent.Level.Map());
            output.AddProperty("timestamp", logEvent.Timestamp.ToUnixTimeMilliseconds());
            output.AddProperty("message", logEvent.MessageTemplate);
            output.RenderProperties(logEvent.Properties);
            output.FinishLog();

            if (logEvent.Exception != null)
            {
                output.Write("Exception - {0}", logEvent.Exception);
            }
        }



    }
}
