using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JollySamurai.UnrealEngine4.T3D.Exception;

namespace JollySamurai.UnrealEngine4.T3D.Parser
{
    public class ParsedNode
    {
        public string SectionType { get; }
        public ParsedNodeBag Children { get; }
        public ParsedPropertyBag AttributeBag { get; }
        public ParsedPropertyBag PropertyBag { get; }

        public ParsedNode(string sectionType, ParsedNodeBag children, ParsedPropertyBag attributeBag, ParsedPropertyBag propertyBag)
        {
            SectionType = sectionType;
            Children = children;
            AttributeBag = attributeBag;
            PropertyBag = ConvertArrays(propertyBag);
        }

        public ParsedProperty FindAttribute(string name)
        {
            return AttributeBag.FindProperty(name);
        }

        public string FindAttributeValue(string name)
        {
            return AttributeBag.FindPropertyValue(name);
        }

        public ParsedProperty FindProperty(string name)
        {
            return PropertyBag.FindProperty(name);
        }

        public string FindPropertyValue(string name)
        {
            return PropertyBag.FindPropertyValue(name);
        }

        public bool HasAttribute(string name)
        {
            return AttributeBag.HasProperty(name);
        }

        public bool HasProperty(string name)
        {
            return PropertyBag.HasProperty(name);
        }

        private ParsedPropertyBag ConvertArrays(ParsedPropertyBag propertyBag)
        {
            Dictionary<string, List<ParsedProperty>> arrayElements = new Dictionary<string, List<ParsedProperty>>();
            Dictionary<string, ParsedProperty> properties = new Dictionary<string, ParsedProperty>();

            foreach (ParsedProperty property in propertyBag.Properties) {
                Match arrayCheckResult = ParsedProperty.ArrayRegex.Match(property.Name);

                if (arrayCheckResult.Success) {
                    string arrayName = arrayCheckResult.Groups[1].Value;
                    string elementName = arrayCheckResult.Groups[2].Value;

                    if (properties.ContainsKey(arrayName)) {
                        throw new ParserException("Found an array element but there is a non-array version of the property registered", -1, -1);
                    }

                    if (! arrayElements.ContainsKey(arrayName)) {
                        arrayElements.Add(arrayName, new List<ParsedProperty>());
                    }

                    arrayElements[arrayName].Add(new ParsedProperty(elementName, property.Value));
                } else {
                    if (arrayElements.ContainsKey(property.Name)) {
                        throw new ParserException("Found a normal element but there is an array version of the property registered", -1, -1);
                    }

                    properties.Add(property.Name, property);
                }
            }

            foreach (var keyValuePair in arrayElements) {
                properties.Add(keyValuePair.Key, new ParsedProperty(keyValuePair.Key, keyValuePair.Value.ToArray()));
            }

            return new ParsedPropertyBag(properties.Values.ToArray());
        }
    }
}
