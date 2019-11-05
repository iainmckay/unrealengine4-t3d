using System.Diagnostics;
using System.Text.RegularExpressions;

namespace JollySamurai.UnrealEngine4.T3D.Parser
{
    public class ParsedProperty
    {
        public static readonly Regex ArrayRegex = new Regex(@"(\w+)\(([0-9]+)\)", RegexOptions.Compiled);

        public string Name { get; }
        public string Value { get; }
        public ParsedProperty[] Elements { get; }
        public bool IsArray { get; }

        public ParsedProperty(string name, string value)
        {
            Trace.Assert(! string.IsNullOrEmpty(name));

            Name = name;
            Value = value;
            IsArray = false;
        }

        public ParsedProperty(string name, ParsedProperty[] arrayElements)
        {
            Trace.Assert(! string.IsNullOrEmpty(name));

            Name = name;
            IsArray = true;
            Elements = arrayElements;
        }
    }
}
