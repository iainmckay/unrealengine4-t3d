using JollySamurai.UnrealEngine4.T3D.Common;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionClamp : Node
    {
        public ParsedPropertyBag Input { get; }
        public ParsedPropertyBag Min { get; }
        public ParsedPropertyBag Max { get; }

        public float MinDefault { get; }
        public float MaxDefault { get; }

        public MaterialExpressionClamp(string name, ParsedPropertyBag input, ParsedPropertyBag min, ParsedPropertyBag max, float minDefault, float maxDefault, int editorX, int editorY) : base(name, editorX, editorY)
        {
            Input = input;
            Min = min;
            MinDefault = minDefault;
            Max = max;
            MaxDefault = maxDefault;
        }
    }

    public class MaterialExpressionClampProcessor : ObjectNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionClamp";

        public MaterialExpressionClampProcessor()
        {
            AddRequiredAttribute("Name", PropertyDataType.String);

            AddRequiredProperty("Input", PropertyDataType.AttributeList);

            AddOptionalProperty("MaterialExpressionEditorX", PropertyDataType.Integer);
            AddOptionalProperty("MaterialExpressionEditorY", PropertyDataType.Integer);
            AddOptionalProperty("Max", PropertyDataType.AttributeList);
            AddOptionalProperty("MaxDefault", PropertyDataType.Float);
            AddOptionalProperty("Min", PropertyDataType.AttributeList);
            AddOptionalProperty("MinDefault", PropertyDataType.Float);

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
                ValueUtil.ParseFloat(node.FindPropertyValue("MinDefault") ?? "0"),
                ValueUtil.ParseFloat(node.FindPropertyValue("MaxDefault") ?? "1"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX") ?? "0"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY") ?? "0")
            );
        }
    }
}
