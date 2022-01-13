using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Material
{
    public class MaterialExpressionClamp : MaterialNode
    {
        public ParsedPropertyBag Input { get; }
        public ParsedPropertyBag Min { get; }
        public ParsedPropertyBag Max { get; }

        public float MinDefault { get; }
        public float MaxDefault { get; }

        public MaterialExpressionClamp(string name, int editorX, int editorY, ParsedPropertyBag input, ParsedPropertyBag min, ParsedPropertyBag max, float minDefault, float maxDefault)
            : base(name, editorX, editorY)
        {
            Input = input;
            Min = min;
            MinDefault = minDefault;
            Max = max;
            MaxDefault = maxDefault;
        }
    }

    public class MaterialExpressionClampProcessor : MaterialNodeProcessor
    {
        public override string Class => "/Script/Engine.MaterialExpressionClamp";

        public MaterialExpressionClampProcessor()
        {
            AddRequiredProperty("Input", PropertyDataType.AttributeList);

            AddOptionalProperty("Max", PropertyDataType.AttributeList);
            AddOptionalProperty("MaxDefault", PropertyDataType.Float);
            AddOptionalProperty("Min", PropertyDataType.AttributeList);
            AddOptionalProperty("MinDefault", PropertyDataType.Float);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new MaterialExpressionClamp(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorX")),
                ValueUtil.ParseInteger(node.FindPropertyValue("MaterialExpressionEditorY")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Input")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Min")),
                ValueUtil.ParseAttributeList(node.FindPropertyValue("Max")),
                ValueUtil.ParseFloat(node.FindPropertyValue("MinDefault")),
                ValueUtil.ParseFloat(node.FindPropertyValue("MaxDefault") ?? "1")
            );
        }
    }
}
