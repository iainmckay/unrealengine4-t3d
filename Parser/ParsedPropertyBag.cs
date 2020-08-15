using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace JollySamurai.UnrealEngine4.T3D.Parser
{
    public class ParsedPropertyBag
    {
        public ParsedProperty[] Properties { get; }

        public static ParsedPropertyBag Empty {
            get {
                return new ParsedPropertyBag(new ParsedProperty[] {
                });
            }
        }

        public ParsedPropertyBag(ParsedProperty[] properties)
        {
            Trace.Assert(properties != null);
            Properties = properties;
        }

        public ParsedProperty FindProperty(string name)
        {
            foreach (ParsedProperty parsedProperty in Properties) {
                if (parsedProperty.Name == name) {
                    return parsedProperty;
                }
            }

            return null;
        }

        public string FindPropertyValue(string name)
        {
            return FindProperty(name)?.Value;
        }

        public bool HasProperty(string name)
        {
            return FindProperty(name) != null;
        }

        public bool HasPropertyWithValue(string name, string value)
        {
            ParsedProperty property = FindProperty(name);

            return null != property && property.Value == value;
        }

        public override string ToString()
        {
            var b = new List<string>();

            foreach (var parsedProperty in Properties) {
                b.Add($"{parsedProperty.Name}={parsedProperty.Value}");
            }

            return string.Join(", ", b.ToArray());
        }
    }
}
