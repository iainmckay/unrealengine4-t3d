using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JollySamurai.UnrealEngine4.T3D.Parser;

namespace JollySamurai.UnrealEngine4.T3D.Processor
{
    public abstract class DocumentProcessor<T> where T : Node
    {
        public static readonly Regex TypeNameRegex = new Regex(@"(\w+)[0-9]{1,}", RegexOptions.Compiled);

        private Dictionary<string, NodeProcessor> _nodeProcessors;

        public DocumentProcessor()
        {
            _nodeProcessors = new Dictionary<string, NodeProcessor>();
        }

        protected void AddNodeProcessor(NodeProcessor nodeProcessor)
        {
            _nodeProcessors.Add(nodeProcessor.Class, nodeProcessor);
        }

        protected NodeProcessor FindProcessorForNode(ParsedNode parsedNode)
        {
            string className = parsedNode.AttributeBag.FindProperty("Class")?.Value;

            if (null != className) {
                return FindProcessor(className);
            }

            return null;
        }

        protected NodeProcessor FindProcessor(string name)
        {
            if (_nodeProcessors.ContainsKey(name)) {
                return _nodeProcessors[name];
            }

            return null;
        }

        public T Convert(ParsedDocument document)
        {
            ParsedNode rootNode = document.RootNode;

            return ProcessNode(rootNode) as T;
        }

        private Node ProcessNode(ParsedNode node)
        {
            NodeProcessor nodeProcessor = FindProcessorForNode(node);

            if (null == nodeProcessor) {
                // FIXME:
                Console.WriteLine(String.Format("processor not found for node, {0} -> {1}", node.AttributeBag.FindProperty("Class")?.Value, node.AttributeBag.FindProperty("Name")?.Value));

                return null;
            }

            ProcessProperties(node, node.AttributeBag, nodeProcessor.AttributeDefinitions, nodeProcessor.IgnoredAttributeNames);
            ProcessProperties(node, node.PropertyBag, nodeProcessor.PropertyDefinitions, nodeProcessor.IgnoredPropertyNames);

            List<Node> children = new List<Node>();

            foreach (ParsedNode childNode in node.Children.Nodes) {
                Node child = ProcessNode(childNode);

                if (null == child) {
                    // FIXME:
                    Console.WriteLine("Failed to process node");
                    continue;
                }

                children.Add(child);
            }

            return nodeProcessor.Convert(node, children.ToArray());
        }

        private void ProcessProperties(ParsedNode node, ParsedPropertyBag propertyBag, PropertyDefinition[] propertyDefinitions, string[] ignoredPropertyNames)
        {
            bool presenceValidationResult = ValidatePresenceOfProperties(node.FindAttributeValue("Name"), propertyBag, propertyDefinitions, ignoredPropertyNames);
            bool valueValidationResult = ValidatePropertiesValues(propertyBag, propertyDefinitions);

            if (! presenceValidationResult || ! valueValidationResult) {
                // FIXME:
                Console.WriteLine("could not process node it had one or more failures");
            }
        }

        private bool ValidatePresenceOfProperties(string nodeName, ParsedPropertyBag propertyBag, PropertyDefinition[] propertyDefinitions, string[] ignoredPropertyNames)
        {
            IEnumerable<string> providedKeys = propertyBag.Properties.Select(property => property.Name);
            IEnumerable<string> allDefinedKeys = propertyDefinitions.Select(property => property.Name);
            IEnumerable<string> requiredDefinedKeys = propertyDefinitions.Where(attribute => attribute.IsRequired).Select(property => property.Name);

            string[] missingPropertiesInMaterial = providedKeys.Except(allDefinedKeys).ToList().Except(ignoredPropertyNames).ToArray();
            string[] missingRequiredProperties = requiredDefinedKeys.Except(providedKeys).ToArray();

            foreach (string s in missingPropertiesInMaterial) {
                // FIXME:
                Console.WriteLine(String.Format("FIXME: unknown attribute/property defined in material: {0} -> {1}", s, nodeName));
            }

            foreach (string s in missingRequiredProperties) {
                // FIXME:
                Console.WriteLine(String.Format("FIXME: missing required attribute/property: {0} -> {1}", s, nodeName));
            }

            return missingRequiredProperties.Length == 0;
        }

        private bool ValidatePropertiesValues(ParsedPropertyBag propertyBag, PropertyDefinition[] propertyDefinitions)
        {
            bool hasProblems = false;

            foreach (PropertyDefinition propertyDefinition in propertyDefinitions) {
                ParsedProperty parsedProperty = propertyBag.FindProperty(propertyDefinition.Name);

                if (null == parsedProperty) {
                    continue;
                }

                if (! ValidatePropertyValue(parsedProperty, propertyDefinition)) {
                    hasProblems = true;
                }
            }

            return ! hasProblems;
        }

        private bool ValidatePropertyValue(ParsedProperty parsedProperty, PropertyDefinition propertyDefinition, bool testForArray = true)
        {
            bool wasValid = false;

            if (testForArray && propertyDefinition.IsArray) {
                if (! parsedProperty.IsArray) {
                    wasValid = false;

                    // FIXME:
                    Console.WriteLine(String.Format("property {0} was expected to be an array but got something else", propertyDefinition.Name));
                } else {
                    wasValid = true;

                    foreach (var parsedPropertyElement in parsedProperty.Elements) {
                        if (! ValidatePropertyValue(parsedPropertyElement, propertyDefinition, false)) {
                            wasValid = false;
                        }
                    }
                }
            } else if (propertyDefinition.DataType.HasFlag(PropertyDataType.Boolean)) {
                ValueUtil.TryParseBoolean(parsedProperty.Value, out wasValid);
            } else if (propertyDefinition.DataType.HasFlag(PropertyDataType.ExpressionReference)) {
                ValueUtil.TryParseExpressionReference(parsedProperty.Value, out wasValid);
            } else if (propertyDefinition.DataType.HasFlag(PropertyDataType.Float)) {
                ValueUtil.TryParseFloat(parsedProperty.Value, out wasValid);
            } else if (propertyDefinition.DataType.HasFlag(PropertyDataType.FunctionReference)) {
                ValueUtil.TryParseFunctionReference(parsedProperty.Value, out wasValid);
            } else if (propertyDefinition.DataType.HasFlag(PropertyDataType.Integer)) {
                ValueUtil.TryParseInteger(parsedProperty.Value, out wasValid);
            } else if (propertyDefinition.DataType.HasFlag(PropertyDataType.SamplerType)) {
                ValueUtil.TryParseSamplerType(parsedProperty.Value, out wasValid);
            } else if (propertyDefinition.DataType.HasFlag(PropertyDataType.String)) {
                wasValid = true;
            } else if (propertyDefinition.DataType.HasFlag(PropertyDataType.TextureReference)) {
                ValueUtil.TryParseTextureReference(parsedProperty.Value, out wasValid);
            } else if (propertyDefinition.DataType.HasFlag(PropertyDataType.Vector4)) {
                ValueUtil.TryParseVector4(parsedProperty.Value, out wasValid);
            } else {
                // FIXME:
                Console.WriteLine("unhandled data type");
                return false;
            }

            if (! wasValid) {
                // FIXME:
                Console.WriteLine("data is invalid");
            }

            return wasValid;
        }
    }
}
