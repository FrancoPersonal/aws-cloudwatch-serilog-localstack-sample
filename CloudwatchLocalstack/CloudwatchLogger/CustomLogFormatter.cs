﻿using Newtonsoft.Json;
using Serilog.Events;
using Serilog.Formatting;
using System.IO;

namespace CloudwatchLogger
{
    public class CustomLogFormatter : ITextFormatter
    {
        public void Format(LogEvent logEvent, TextWriter output)
        {
            output.Write("Timestamps - {0} | Level - {1} | Message {2} {3} {4}", logEvent.Timestamp, logEvent.Level, logEvent.MessageTemplate, JsonConvert.SerializeObject(logEvent.Properties), output.NewLine);
            if (logEvent.Exception != null)
            {
                output.Write("Exception - {0}", logEvent.Exception);
            }
        }
    }
}
