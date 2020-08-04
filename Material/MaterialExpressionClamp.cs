using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionClamp : Node
    {
        public ParsedPropertyBag Input { get; }
        public ParsedPropertyBag Min { get; }
        public ParsedPropertyBag Max { get; }

        public MaterialExpressionClamp(string name, ParsedPropertyBag input, ParsedPropertyBag min, ParsedPropertyBag max, int editorX, int editorY) : base(name, editorX, editorY)
        {
            Input = input;
            Min = min;
            Max = max;
        }
    }

    public class MaterialExpressionClampProcessor : NodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionClamp";

        public MaterialExpressionClampProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddRequiredProperty("Input", PropertyDataType.AttributeList);

            AddOptionalProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddOptionalProperty("MaterialExpressionEditorY", PropertyDataType.Integer);
            AddOptionalProperty("Max", PropertyDataType.AttributeList);
            AddOptionalProperty("Min", PropertyDataType.AttributeList);

            AddIgnoredProperty("Material");
            AddIgnoredProperty("MaterialExpressionGuid");
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionClamp(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Input")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Min") ?? null),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Max") ?? null),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX") ?? "0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY") ?? "0")
            );
        }
    }
}
