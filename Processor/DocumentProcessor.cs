using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using JollySamurai.UnrealEngine4.T3D.Exception;
using JollySamurai.UnrealEngine4.T3D.Parser;

namespace JollySamurai.UnrealEngine4.T3D.Processor
{
    public abstract class DocumentProcessor<T>
        where T : Node
    {
        public static readonly Regex TypeNameRegex = new Regex(@"(\w+)[0-9]{1,}", RegexOptions.Compiled);

        private Dictionary<string, NodeProcessor> _nodeProcessors;

        private List<string> _ignoredNodes;

        public DocumentProcessor()
        {
            _nodeProcessors = new Dictionary<string, NodeProcessor>();
            _ignoredNodes = new List<string>();
        }

        protected void AddNodeProcessor(NodeProcessor nodeProcessor)
        {
            _nodeProcessors.Add(nodeProcessor.Class, nodeProcessor);
        }
        
        protected void IgnoreNode(string className)
        {
            _ignoredNodes.Add(className);
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

        public DocumentProcessorResult<T> Convert(ParsedDocument document)
        {
            ParsedNode rootNode = document.RootNode;
            DocumentProcessorState state = new DocumentProcessorState();
            T convertedRootNode = ProcessNode(rootNode, state) as T;

            return new DocumentProcessorResult<T>(convertedRootNode, state.Problems);
        }

        private Node ProcessNode(ParsedNode node, DocumentProcessorState state)
        {
            var nodeClass = node.AttributeBag.FindProperty("Class")?.Value;

            if (_ignoredNodes.Contains(nodeClass)) {
                return null;
            }

            NodeProcessor nodeProcessor = FindProcessorForNode(node);

            if (null == nodeProcessor) {
                state.AddWarning("Processor not found (class={0}, node={1})", nodeClass, node.AttributeBag.FindProperty("Name")?.Value);

                return null;
            }

            if (! Validate(node, nodeProcessor, state)) {
                return null;
            }

            List<Node> children = new List<Node>();

            foreach (ParsedNode childNode in node.Children.Nodes) {
                Node child = ProcessNode(childNode, state);

                if (null != child) {
                    children.Add(child);
                }
            }

            return nodeProcessor.Convert(node, children.ToArray());
        }

        private bool Validate(ParsedNode node, NodeProcessor nodeProcessor, DocumentProcessorState state)
        {
            bool validateAttributesResult = ValidateProperties(node, node.AttributeBag, nodeProcessor.AttributeDefinitions, nodeProcessor.IgnoredAttributeNames, state);
            bool validatePropertiesResult = ValidateProperties(node, node.PropertyBag, nodeProcessor.PropertyDefinitions, nodeProcessor.IgnoredPropertyNames, state);

            return validateAttributesResult && validatePropertiesResult;
        }

        private bool ValidateProperties(ParsedNode node, ParsedPropertyBag propertyBag, PropertyDefinition[] propertyDefinitions, string[] ignoredPropertyNames, DocumentProcessorState state)
        {
            bool presenceValidationResult = ValidatePresenceOfProperties(node.FindAttributeValue("Name"), propertyBag, propertyDefinitions, ignoredPropertyNames, state);
            bool valueValidationResult = ValidatePropertiesValues(node.FindAttributeValue("Name"), propertyBag, propertyDefinitions, state);

            return presenceValidationResult && valueValidationResult;
        }

        private bool ValidatePresenceOfProperties(string nodeName, ParsedPropertyBag propertyBag, PropertyDefinition[] propertyDefinitions, string[] ignoredPropertyNames, DocumentProcessorState state)
        {
            IEnumerable<string> providedKeys = propertyBag.Properties.Select(property => property.Name);
            IEnumerable<string> allDefinedKeys = propertyDefinitions.Select(property => property.Name);
            IEnumerable<string> requiredDefinedKeys = propertyDefinitions.Where(attribute => attribute.IsRequired).Select(property => property.Name);

            string[] missingPropertiesInMaterial = providedKeys.Except(allDefinedKeys).ToList().Except(ignoredPropertyNames).ToArray();
            string[] missingRequiredProperties = requiredDefinedKeys.Except(providedKeys).ToArray();

            foreach (string s in missingPropertiesInMaterial) {
                state.AddWarning("Unknown property found (property={0}, node={1})", s, nodeName);
            }

            foreach (string s in missingRequiredProperties) {
                state.AddError("Required property is missing (property={0}, node={1})", s, nodeName);
            }

            return missingRequiredProperties.Length == 0;
        }

        private bool ValidatePropertiesValues(string nodeName, ParsedPropertyBag propertyBag, PropertyDefinition[] propertyDefinitions, DocumentProcessorState state)
        {
            bool hasProblems = false;

            foreach (PropertyDefinition propertyDefinition in propertyDefinitions) {
                ParsedProperty parsedProperty = propertyBag.FindProperty(propertyDefinition.Name);

                if (null == parsedProperty) {
                    continue;
                }

                if (! ValidatePropertyValue(nodeName, parsedProperty, propertyDefinition, state)) {
                    hasProblems = true;
                }
            }

            return ! hasProblems;
        }

        private bool ValidatePropertyValue(string nodeName, ParsedProperty parsedProperty, PropertyDefinition propertyDefinition, DocumentProcessorState state, bool testForArray = true)
        {
            bool wasValid = false;

            if (testForArray && propertyDefinition.IsArray) {
                if (! parsedProperty.IsArray) {
                    wasValid = false;

                    state.AddError($"Property \"{propertyDefinition.Name}\" on \"{nodeName}\" was expected to be an array but got something else");

                    return false;
                } else {
                    wasValid = true;

                    foreach (var parsedPropertyElement in parsedProperty.Elements) {
                        if (! ValidatePropertyValue(nodeName, parsedPropertyElement, propertyDefinition, state, false)) {
                            wasValid = false;
                        }
                    }
                }
            } else if (propertyDefinition.DataType.HasFlag(PropertyDataType.AttributeList)) {
                ValueUtil.TryParseAttributeList(parsedProperty.Value, out wasValid);
            } else if (propertyDefinition.DataType.HasFlag(PropertyDataType.BlendMode)) {
                ValueUtil.TryParseBlendMode(parsedProperty.Value, out wasValid);
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
            } else if (propertyDefinition.DataType.HasFlag(PropertyDataType.ShadingModel)) {
                ValueUtil.TryParseShadingModel(parsedProperty.Value, out wasValid);
            } else if (propertyDefinition.DataType.HasFlag(PropertyDataType.String)) {
                wasValid = true;
            } else if (propertyDefinition.DataType.HasFlag(PropertyDataType.TextureReference)) {
                ValueUtil.TryParseTextureReference(parsedProperty.Value, out wasValid);
            } else if (propertyDefinition.DataType.HasFlag(PropertyDataType.Vector4)) {
                ValueUtil.TryParseVector4(parsedProperty.Value, out wasValid);
            } else {
                state.AddError($"Property \"{parsedProperty.Name} on \"{nodeName}\" had unhandled data type: {propertyDefinition.DataType}");

                return false;
            }

            if (! wasValid) {
                state.AddError($"Property \"{parsedProperty.Name}\" on \"{nodeName}\" value is invalid: {parsedProperty.Value}");
            }

            return wasValid;
        }
    }

    public class DocumentProcessorResult<T>
        where T : Node
    {
        public T RootNode { get; }
        public Problem[] Problems { get; }

        public DocumentProcessorResult(T rootNode, Problem[] problems)
        {
            RootNode = rootNode;
            Problems = problems;
        }
    }

    public class DocumentProcessorState
    {
        private List<Problem> _problems = new List<Problem>();

        public Problem[] Problems {
            get { return _problems.ToArray(); }
        }

        public DocumentProcessorState()
        {
        }

        public void AddWarning(string format, params object[] args)
        {
            AddProblem(ProblemSeverity.Warning, format, args);
        }

        public void AddError(string format, params object[] args)
        {
            AddProblem(ProblemSeverity.Error, format, args);
        }

        private void AddProblem(ProblemSeverity severity, string format, object[] args)
        {
            _problems.Add(new Problem(severity, string.Format(format, args)));
        }
    }
}
