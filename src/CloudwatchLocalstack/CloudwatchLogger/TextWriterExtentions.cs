using Serilog.Events;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CloudwatchLogger
{
    public static class TextWriterExtentions
    {
        private const string fullPathern = ",\"{0}\":\"{1}\"";
        private const string firstPathern = "\"{0}\":\"{1}\"";
        private const string eventPropertyPathern = ",\"{0}\":{1}";

        public static void AddProperty<T>(this TextWriter output, string propertyname, T value)
        {
            output.Write(fullPathern, propertyname.ToLowerInvariant(), value);
        }

        public static void RenderProperties(this TextWriter output, IReadOnlyDictionary<string, LogEventPropertyValue> properties)
        {
            properties.ToList().ForEach(p => AddLogEventProperty(output, p.Key, p.Value));
        }

        public static void AddLogEventProperty<T>(this TextWriter output, string propertyname, T value)
        {
            output.Write(eventPropertyPathern, propertyname.ToLowerInvariant(), value);
        }


        public static void FirstProperty<T>(this TextWriter output, string propertyname, T value)
        {
            output.Write("{");
            output.Write(firstPathern, propertyname.ToLowerInvariant(), value);
        }

        public static void FinishLog(this TextWriter output)
        {
            output.Write("}");
            output.WriteLine();
        }


    }
}
