using System.Collections.Generic;
using System.Linq;
using JollySamurai.UnrealEngine4.T3D.Parser;

namespace JollySamurai.UnrealEngine4.T3D.Processor
{
    public abstract class NodeProcessor
    {
        public abstract string Class { get; }

        public PropertyDefinition[] AttributeDefinitions {
            get => _attributeDefinitions.Values.ToArray();
        }

        public PropertyDefinition[] PropertyDefinitions {
            get => _propertyDefinitions.Values.ToArray();
        }

        public string[] IgnoredAttributeNames {
            get => _ignoredAttributes.ToArray();
        }

        public string[] IgnoredPropertyNames {
            get => _ignoredProperties.ToArray();
        }

        private Dictionary<string, PropertyDefinition> _attributeDefinitions;
        private Dictionary<string, PropertyDefinition> _propertyDefinitions;

        private List<string> _ignoredAttributes;
        private List<string> _ignoredProperties;

        public NodeProcessor()
        {
            _attributeDefinitions = new Dictionary<string, PropertyDefinition>();
            _propertyDefinitions = new Dictionary<string, PropertyDefinition>();

            _ignoredAttributes = new List<string>();
            _ignoredProperties = new List<string>();

            _ignoredAttributes.Add("Class");
        }

        public abstract Node Convert(ParsedNode node, Node[] children);

        protected void AddIgnoredAttribute(string name)
        {
            _ignoredAttributes.Add(name);
        }

        protected void AddIgnoredProperty(string name)
        {
            _ignoredProperties.Add(name);
        }

        protected void AddRequiredAttribute(string name, PropertyDataType propertyDataType)
        {
            _attributeDefinitions.Add(name, new PropertyDefinition(name, propertyDataType, true));
        }

        protected void AddRequiredProperty(string name, PropertyDataType propertyDataType)
        {
            _propertyDefinitions.Add(name, new PropertyDefinition(name, propertyDataType, true));
        }

        protected void AddOptionalProperty(string name, PropertyDataType propertyDataType)
        {
            _propertyDefinitions.Add(name, new PropertyDefinition(name, propertyDataType, false));
        }
    }
}
